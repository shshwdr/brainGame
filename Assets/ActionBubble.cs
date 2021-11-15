using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBubble : Bubble
{
    public ActionBubbleInfo info;
    float currentValue;


    public EmotionRequirementCell[] emotionRequirementCells;


    public void consumeIngredient(EmotionBubble ing, ActionSlot slot)
    {
        emotionRequirementCells[ing.emotionType].updateEmotion(ing);
        //if (currentValue >= 1)
        //{
        //    succeed();
        //    slot.removeAction();
        //}
    }

    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        emotionRequirementCells = GetComponentsInChildren<EmotionRequirementCell>();
        info = (ActionBubbleInfo)inf;
        isActionBubble = true;
        int i = 0;
        var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[inf.name];
        foreach(var emotionRequirementCell in emotionRequirementCells)
        {
            emotionRequirementCell.init(expressionRanges[i],i);
            i++;
        }

    }
    public void getReward(List<string> rewards)
    {
        foreach(string rew in rewards)
        {
            BubbleManager.Instance.specificBubbleGeneration(rew,Vector3.positiveInfinity);
        }
    }
    public void succeed()
    {
        //var pickedResult = (ActionResult)BubbleManager.pickInfoWithProbability(info.successResults);
        //var successLogs = pickedResult.logs;
        //LogController.Instance.addLog(successLogs[Random.Range(0, successLogs.Length)], Color.green);
        //getReward(pickedResult.rewards);
        //Destroy(gameObject);
    }

    public void failed()
    {
        //var pickedResult =(ActionResult) BubbleManager.pickInfoWithProbability(info.failedResults);
        //var failedLogs = pickedResult.logs;
        //LogController.Instance.addLog(failedLogs[Random.Range(0, failedLogs.Length)],Color.red);
        //getReward(pickedResult.rewards);
        //Destroy(gameObject);
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
