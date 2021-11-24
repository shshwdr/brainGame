using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPlanController : MonoBehaviour
{
    public TMP_Text dayLabel;
    public TMP_Text eventLabel;
    AttributeRequirementCell[] attributeRequirementCells;

    public Button finishButton;
    public Button cancelButton;
    // Start is called before the first frame update
    void Start()
    {
        CollectionManager.Instance.transform.position = transform.position;
        attributeRequirementCells = GetComponentsInChildren<AttributeRequirementCell>();
        onDayUpdate();
        EventPool.OptIn("gameStageUpdate", onDayUpdate);
        EventPool.OptIn("attributeUpdate", onDayUpdate);
    }

    public void onDayUpdate()
    {
        dayLabel.text = "第" + EventPlanManager.Instance.currentDay.ToString() + "/30 天";
        var currentEvent = EventPlanManager.Instance.currentEvent;
        //eventLabel.text = string.Format( currentEvent.displayName, currentEvent.date, currentEvent.requiredString) + " （还剩"+(currentEvent.date - EventPlanManager.Instance.currentDay)+"天）";
        var eventLeftTime = (currentEvent.date - EventPlanManager.Instance.currentDay);
        eventLabel.text = currentEvent.name+ "还有" + eventLeftTime.ToString()+"天";
        int i = 0;
        foreach (var pair in currentEvent.requirement)
        {
            attributeRequirementCells[i].gameObject.SetActive(true);
            attributeRequirementCells[i].init(pair.Key,(int) pair.Value);
            i++;
        }
        for (; i < attributeRequirementCells.Length; i++)
        {
            attributeRequirementCells[i].gameObject.SetActive(false);
        }

        if (EventPlanManager.Instance.isLastEvent())
        {

            finishButton.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
        else
        {
            if (EventPlanManager.Instance.canFinishEvent())
            {
                finishButton.gameObject.SetActive(true);
                cancelButton.gameObject.SetActive(false);
            }
            else
            {

                finishButton.gameObject.SetActive(false);
                cancelButton.gameObject.SetActive(true);
            }

        }


    }

    public void OnButtonClick()
    {
        EventPlanManager.Instance.checkAndUpdateEvent();
        onDayUpdate();
    }

    public void finish()
    {

    }

    public void cancel()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
