using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBubble : Bubble
{
    public ActionBubbleInfo info;
    float currentValue;


    public EmotionRequirementCell[] emotionRequirementCells;

    void OnMouseDown()
    {
        if (canFinish())
        {
            consume();
            succeed();
        }
    }

    void updateColor()
    {
        if (canFinish())
        {
            rend.color = Color.green;
        }
        else
        {
            rend.color = Color.gray;
        }
    }
    bool canFinish()
    {
        var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[info.name];
        Dictionary<string, int> res = new Dictionary<string, int>();
        for (int i = 0; i < expressionRanges.Count; i++)
        {
            if (expressionRanges[i] > 0)
            {
                res[BubbleCalculator.Instance.IdToEmotion[i]] = (int)expressionRanges[i];
            }
        }
        if (Inventory.Instance.canConsumeItems(res))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void consume()
    {
        var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[info.name];
        Dictionary<string, int> res = new Dictionary<string, int>();
        for (int i = 0; i < expressionRanges.Count; i++)
        {
            if (expressionRanges[i] > 0)
            {
                res[BubbleCalculator.Instance.IdToEmotion[i]] = (int)expressionRanges[i];
            }
        }
        Inventory.Instance.consumeItems(res);
    }

    //public void consumeIngredient(EmotionBubble ing, ActionSlot slot)
    //{
    //    emotionRequirementCells[ing.emotionType].updateEmotion(ing);
    //    //if (currentValue >= 1)
    //    //{
    //    //    succeed();
    //    //    slot.removeAction();
    //    //}
    //}

    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        emotionRequirementCells = GetComponentsInChildren<EmotionRequirementCell>();
        info = (ActionBubbleInfo)inf;
        isActionBubble = true;
        int i = 0;
        var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[inf.name];

        foreach(var pair in BubbleCalculator.Instance.emotionToId)
        {
            if (expressionRanges[pair.Value] > 0)
            {
                emotionRequirementCells[i].init(pair.Key, (int)expressionRanges[pair.Value]);
                i++;
            }
        }

        //foreach(var emotionRequirementCell in emotionRequirementCells)
        //{
        //    emotionRequirementCell.init(expressionRanges[i],i);
        //    i++;
        //}

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
        AttributeManager.Instance.addAttributes(info.successAttribute);
        //Inventory.Instance.consumeItems(info.failedAttribute);
        //var pickedResult = (ActionResult)BubbleManager.pickInfoWithProbability(info.successAttribute);
        string finalLog = info.log[0] + LogController.Instance.getActionLog(info.name, 1);
        LogController.Instance.addLog(finalLog, Color.green);
        if (info.gameProcess > 0)
        {
            DevelopGameStageManager.Instance.addProcess(3);
        }
        //getReward(pickedResult.rewards);
        Destroy(gameObject);
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
        updateColor();
        EventPool.OptIn("inventoryChanged", updateColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
