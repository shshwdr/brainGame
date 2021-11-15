using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDayManager : Singleton<DefaultDayManager>
{
    public List<List<string>> dayInfoList;
    public List<string> dayTime;
    float dayTimeInterval = 2;
    float currentDayTimeInterval = 0;
    List<string> selectedDayInfo;
    int currentDayTimeId = 0;
    // Start is called before the first frame update
    void Awake()
    {

        dayInfoList = CsvUtil.LoadList("DayPlan",true);
        dayTime = dayInfoList[0];
        dayInfoList.RemoveAt(0);
        selectedDayInfo = Utils.randomFromList(dayInfoList);
    }
    private void Start()
    {

        dayTimeInterval = (float)GameManager.Instance.data["dayIdeaBubbleGenerationTime"];
    }
    public bool isResting()
    {
        return currentDayTimeInterval < 0;
    }
    // Update is called once per frame
    void Update()
    {
        currentDayTimeInterval += Time.deltaTime;
        if (currentDayTimeInterval >= dayTimeInterval)
        {
            currentDayTimeInterval = 0;
            BubbleManager.Instance.specificBubbleGeneration(selectedDayInfo[currentDayTimeId], Vector3.positiveInfinity);
            currentDayTimeId++;
            if (currentDayTimeId >= selectedDayInfo.Count)
            {

                selectedDayInfo = Utils.randomFromList(dayInfoList);
                currentDayTimeId = 0;
                currentDayTimeInterval = -(float)GameManager.Instance.data["dayIntervalTime"];
                AttributeManager.Instance.reduceAllAttributes((int)GameManager.Instance.data["attributeReduceADay"]);
                LogController.Instance.addLog("一天过去了，各项属性都减少了",Color.yellow);
            }
        }
    }
}
