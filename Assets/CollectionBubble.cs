using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionBubble : ActionBubble
{
    public CollectionBubbleInfo info;
    // Start is called before the first frame update



    protected override void unlockOthers()
    {

    }

    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        info = (CollectionBubbleInfo)inf;

        outputRender.sprite =info.icon;
        isActionBubble = true;


        initRequirements2();
    }

    protected void initRequirements2()
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

    public override void succeed()
    {
        var prefab = Resources.Load<GameObject>("collection");
        
        if (BubbleManager.Instance.collectionPositions[info.name].Count > 0)
        {

            var position = BubbleManager.Instance.collectionPositions[info.name][0].position;
            BubbleManager.Instance.collectionPositions[info.name].RemoveAt(0);

            var go = Instantiate(prefab, position, Quaternion.identity);
            go.GetComponent<Collection>().init(info);
        }
        string finalLog = Utils.randomFromList( info.log);// + LogController.Instance.getActionLog(info.name, 1);
        LogController.Instance.addLog(finalLog,Color.yellow);
        if (info.gameProcess > 0)
        {
            // DevelopGameStageManager.Instance.addProcess(3);
        }
        //getReward(pickedResult.rewards);
        Destroy(gameObject);
    }
}