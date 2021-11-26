using LitJson;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionRequirementCell:MonoBehaviour
{
    public TMP_Text label;
    public Image image;
    public SpriteRenderer render;
    public void init( string  emotionType, int requirement)
    {
        var info = BubbleManager.Instance.emotionBubbleInfoDict[emotionType];
        //image.color = BubbleManager.Instance.emotionIdToColor[emotionType];
        label.text = requirement.ToString();
        //image.color = info.color;
        render.sprite = info.icon;

    }

    //public void updateText()
    //{
    //    label.text = expression.ToString();
    //}
    //public void updateEmotion(EmotionBubble emotionBubble)
    //{
    //    switch (emotionBubble.symbol)
    //    {
    //        case '+':
    //            expression.value += emotionBubble.value;
    //            break;
    //        case '-':
    //            expression.value -= emotionBubble.value;
    //            break;
    //        default:
    //            Debug.LogError("wrong symbol " + emotionBubble.symbol);
    //            break;
    //    }
    //    updateText();
    //}
}