using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBubble : Bubble
{
    public ActionBubbleInfo info;
    float currentValue;
    
    public void consumeIngredient(IngredientBubble ing, ActionSlot slot)
    {
        currentValue += info.ingredients[0].addValue;
        if (currentValue >= 1)
        {
            succeed();
            slot.removeAction();
        }
    }
    
    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        info = (ActionBubbleInfo)inf;
        isActionBubble = true;
    }

    public void succeed()
    {
        var successLogs = info.successResults[0].logs;
        LogController.Instance.addLog(successLogs[Random.Range(0, successLogs.Length)]);
        Destroy(gameObject);
    }

    public void failed()
    {

        var failedLogs = info.failedResults[0].logs;
        LogController.Instance.addLog(failedLogs[Random.Range(0, failedLogs.Length)]);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
