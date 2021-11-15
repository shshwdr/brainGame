using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubble : ActionSlot
{
    //EmotionRequirementCell[] emotionRequirementCells;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    init();
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
    //public void init()
    //{
    //    emotionRequirementCells = GetComponentsInChildren<EmotionRequirementCell>();
    //    int i = 0;
    //    foreach (var emotionRequirementCell in emotionRequirementCells)
    //    {
            
    //        emotionRequirementCell.init(new CompareExpressionRange(), i);
    //        i++;
    //    }

    //}

    //public int isSuccess(ActionBubble bubble)
    //{
    //    bool res = true;
    //    for(int i = 0; i < 2; i++)
    //    {
    //        var currentExpression = emotionRequirementCells[i].expression;
    //        var compareExpression = bubble.emotionRequirementCells[i].expression;
    //        switch (compareExpression.symbol) {
    //            case '>':
    //                res = currentExpression.value >= compareExpression.value;
    //                break;
    //            case '<':
    //                res = currentExpression.value <= compareExpression.value;
    //                break;
    //            default:
    //                Debug.LogError("wrong symbol in player bubble " + compareExpression.symbol);
    //                break;
    //        }
    //        if(res == false)
    //        {
    //            break;
    //        }
    //    }
    //    return res?1:0;
    //}
    //public override void consumeEmotion(EmotionBubble bubble)
    //{

    //    // var value = BubbleCalculator.Instance.calculateAddScore(actionBubble.info.name, bubble.info.name);
    //    //score += value*baseScore;
    //    Destroy(bubble.gameObject);

    //    //var actionBubbleLogs = actionBubble.info.emotionDict[bubble.info.name].logs;
    //    //var addValue = actionBubble.info.ingredientDict[bubble.info.name].addValue;
    //    //LogController.Instance.addLog(actionBubbleLogs[Random.Range(0, actionBubbleLogs.Length)], addValue > 0 ? Color.white : Color.grey);

    //    emotionRequirementCells[bubble.emotionType].updateEmotion(bubble);
    //}
    //public override bool canTakeBubble(Bubble bubble)
    //{
    //    return !bubble.isActionBubble;
    //    //if(bubble.isActionBubble && actionBubble == null)
    //    //{
    //    //    return true;
    //    //}
    //    //if (!bubble.isActionBubble && actionBubble != null) {
    //    //    return true;
    //    //}
    //    //return false;
    //}
}
