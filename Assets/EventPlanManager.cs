using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventPlanInfo : BaseInfo
{
    public int date;

    public Dictionary<string, float> addRequirement;
    public Dictionary<string, float> successReward;
    public Dictionary<string, float> failedReward;
    public string successLog;
    public string failedLog;

    public Dictionary<string, float> requirement = new Dictionary<string, float>();
    public string requiredString
    {
        get
        {
            string res = "";
            foreach (var pair in requirement)
            {
                res += pair.Value.ToString();
                res += pair.Key;
                res += " ";
            }
            return res;
        }
    }
}
public class EventPlanManager : Singleton<EventPlanManager>
{
    public int currentDay = 1;
    public Dictionary<string, EventPlanInfo> eventPlanInfoDict = new Dictionary<string, EventPlanInfo>();
    public List<EventPlanInfo> eventPlanInfoList;
    int currentEventId = 0;
    int totalDay = 30;

    public bool isLastDay()
    {
        return currentDay >= totalDay;
    }
    public EventPlanInfo currentEvent
    {
        get { return eventPlanInfoList[Mathf.Min( currentEventId, eventPlanInfoList.Count-1)]; }
    }


    void Awake()
    {

        eventPlanInfoList = CsvUtil.LoadObjects<EventPlanInfo>("EventPlan");
        //foreach (var info in eventPlanInfoList)
        //{
        //    eventPlanInfoDict[info.name] = info;
        //}
    }

    public void gotoTheLastDay()
    {
        currentDay = totalDay ;

        if (currentEvent.date == currentDay)
        {
            checkAndUpdateEvent();
        }
        EventPool.Trigger("gameStageUpdate");
    }
    public void addDay()
    {
        currentDay += 1;

        if (currentEvent.date == currentDay)
        {
            checkAndUpdateEvent();
        }
        EventPool.Trigger("gameStageUpdate");

        EventPool.Trigger("checkGameOver");

    }


    

    public bool isLastEvent()
    {
        return currentEventId >= eventPlanInfoList.Count;
    }

    public bool canFinishEvent()
    {
        bool succeed = true;
        foreach (var pair in currentEvent.requirement)
        {
            if (AttributeManager.Instance.getAttributeValue(pair.Key) < pair.Value)
            {
                succeed = false;
                break;
            }
        }
        return succeed;
    }

    public void checkAndUpdateEvent()
    {

            finishCurrentEvent();

            currentEventId++;
            updateEvent();
    }

    void finishCurrentEvent()
    {
        
        if (canFinishEvent())
        {
            LogController.Instance.addLog(currentEvent.successLog,Color.green);
            CollectionManager.Instance.AddCoins(transform.position, currentEvent.successReward);
            //foreach (var pair in currentEvent.successReward)
            //{
            //    AttributeManager.Instance.addAttribute(pair.Key, pair.Value);
            //}
        }
        else
        {
            LogController.Instance.addLog(currentEvent.failedLog,Color.red); 
            //foreach (var pair in currentEvent.failedReward)
            //{
            //    AttributeManager.Instance.addAttribute(pair.Key, pair.Value);
            //}
        }
    }

    void updateEvent()
    {

        foreach (var pair in currentEvent.addRequirement)
        {
            currentEvent.requirement[pair.Key] = Mathf.Min(90, pair.Value + AttributeManager.Instance.getAttributeValue(pair.Key));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        updateEvent();
        EventPool.Trigger("gameStageUpdate");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
