using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventPlanController : MonoBehaviour
{
    public TMP_Text dayLabel;
    public TMP_Text eventLabel;
    // Start is called before the first frame update
    void Start()
    {
        onDayUpdate();
        EventPool.OptIn("gameStageUpdate", onDayUpdate);
    }

    public void onDayUpdate()
    {
        dayLabel.text = "第" + EventPlanManager.Instance.currentDay.ToString() + "/30 天";
        var currentEvent = EventPlanManager.Instance.currentEvent;
        eventLabel.text = string.Format( currentEvent.displayName, currentEvent.date, currentEvent.requiredString) + " （还剩"+(currentEvent.date - EventPlanManager.Instance.currentDay)+"天）";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
