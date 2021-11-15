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
    public CompareExpression expression;
    public CompareExpression init(CompareExpressionRange range, int emotionType)
    {
        if(range == null)
        {
            gameObject.SetActive(false);
            return null;
        }
        else
        {
            gameObject.SetActive(true);
            expression = new CompareExpression(range);
            image.color = BubbleManager.Instance.emotionIdToColor[emotionType];
            label.text = expression.ToString();

            return expression;
        }
    }

    public void updateText()
    {
        label.text = expression.ToString();
    }
    public void updateEmotion(EmotionBubble emotionBubble)
    {
        switch (emotionBubble.symbol)
        {
            case '+':
                expression.value += emotionBubble.value;
                break;
            case '-':
                expression.value -= emotionBubble.value;
                break;
            default:
                Debug.LogError("wrong symbol " + emotionBubble.symbol);
                break;
        }
        updateText();
    }
}