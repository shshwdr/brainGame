using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionBubble : Bubble
{
    public ActionBubbleInfo info;
    float currentValue;
    public SpriteRenderer outputRender;

    public EmotionRequirementCell[] emotionRequirementCells;

    void OnMouseDown()
    {
        if (PauseButtonController.Instance.isPaused)
        {
            return;
        }
        if (canFinish())
        {
            MusicManager.Instance.playOneShot(audioClip);
            consume();
            succeed();
            unlockOthers();
        }
    }

    protected virtual void unlockOthers()
    {
        info.finishedTime++;
        var bubbleInfoDict = BubbleManager.Instance.actionBubbleInfoDict;
        foreach (var value in bubbleInfoDict.Values)
        {
            if (!value.isUnlocked)
            {
                bool canUnlock = true;
                foreach (var pair in value.isLockedBy)
                {
                    if (bubbleInfoDict[pair.Key].finishedTime < pair.Value)
                    {
                        canUnlock = false;
                        break;
                    }
                }
                if (canUnlock)
                {
                    value.isUnlocked = true;
                    LogController.Instance.addLog("新想法解锁：" + value.displayName,Color.yellow);
                    if (!BubbleManager.Instance.categoryToIds.ContainsKey(value.category))
                    {
                        BubbleManager.Instance.categoryToIds[value.category] = new List<string>();
                    }
                    BubbleManager.Instance.categoryToIds[value.category].Add(value.name);
                    if (info.gameProcess == 1)
                    {
                        BubbleManager.Instance.gameIdeas.Add(info.name);
                    }
                }
            }
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
            rend.color = Color.white;
        }
    }
    bool canFinish()
    {
        //var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[info.name];
        //Dictionary<string, int> res = new Dictionary<string, int>();
        //for (int i = 0; i < expressionRanges.Count; i++)
        //{
        //    if (expressionRanges[i] > 0)
        //    {
        //        res[BubbleCalculator.Instance.IdToEmotion[i]] = (int)expressionRanges[i];
        //    }
        //}
        if (Inventory.Instance.canConsumeItems(info))
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
        //var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[info.name];
        //Dictionary<string, int> res = new Dictionary<string, int>();
        //for (int i = 0; i < expressionRanges.Count; i++)
        //{
        //    if (expressionRanges[i] > 0)
        //    {
        //        res[BubbleCalculator.Instance.IdToEmotion[i]] = (int)expressionRanges[i];
        //    }
        //}
        Inventory.Instance.consumeItems(info);
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
        info = (ActionBubbleInfo)inf;

        if (info.successAttribute!=null&&info.successAttribute.Keys.ToList().Count > 0)
        {

            var firstAward = info.successAttribute.Keys.ToList()[0];
            outputRender.sprite = AttributeManager.Instance.attributeDict[firstAward].icon;
            isActionBubble = true;
        }


        initRequirements();

    }

    protected void initRequirements()
    {
        emotionRequirementCells = GetComponentsInChildren<EmotionRequirementCell>();
        int i = 0;
        //var expressionRanges = BubbleCalculator.Instance.ideaEmotionRelationship[inf.name];

        foreach (var key in BubbleManager.Instance.emotionBubbleInfoDict.Keys)
        {
            if (info.getEmotionRequirement(key) > 0)
            {

                emotionRequirementCells[i].gameObject.SetActive(true);
                emotionRequirementCells[i].init(key, info.getEmotionRequirement(key));
                i++;
            }
        }
        for (; i < emotionRequirementCells.Length; i++)
        {
            emotionRequirementCells[i].gameObject.SetActive(false);
        }
    }
    public void getReward(List<string> rewards)
    {
        foreach(string rew in rewards)
        {
            BubbleManager.Instance.specificBubbleGeneration(rew,Vector3.positiveInfinity);
        }
    }
    public virtual void succeed()
    {
        CollectionManager.Instance.AddCoins(transform.position, info.successAttribute);
        // AttributeManager.Instance.addAttributes(info.successAttribute);
        //Inventory.Instance.consumeItems(info.failedAttribute);
        //var pickedResult = (ActionResult)BubbleManager.pickInfoWithProbability(info.successAttribute);
        string finalLog = Utils.randomFromList(info.log);// + LogController.Instance.getActionLog(info.name, 1);
        LogController.Instance.addLog(finalLog);
        if (info.gameProcess > 0)
        {
            // DevelopGameStageManager.Instance.addProcess(3);
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
