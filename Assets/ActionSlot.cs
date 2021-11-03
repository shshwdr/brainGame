using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    ActionBubble actionBubble;
    public Image overlayImage;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeAction()
    {
        actionBubble = null;
        overlayImage.gameObject.SetActive(false);
        DOTween.Kill(overlayImage.fillAmount);
        StopCoroutine(coroutine);
    }

    public void attachAction(ActionBubble bubble)
    {
        actionBubble = bubble;
        LogController.Instance.addLog(bubble.info.logs[Random.Range(0, bubble.info.logs.Length)], Color.yellow);
        overlayImage.gameObject.SetActive(true);
        overlayImage.fillAmount = 0;
        Debug.Log("attach action " + actionBubble.info.name + " " + actionBubble.info.duration);
        DOTween.To(() => overlayImage.fillAmount, x => overlayImage.fillAmount = x, 1, actionBubble.info.duration);

        coroutine = StartCoroutine(finishAction(actionBubble.info.duration));
    }


    public IEnumerator finishAction(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (actionBubble)
        {
            actionBubble.failed();
            removeAction();
        }
    }

    void consumeIngredient(IngredientBubble bubble)
    {
        Destroy(bubble.gameObject);
        var actionBubbleLogs = actionBubble.info.ingredientDict[bubble.info.name].logs;
        var addValue = actionBubble.info.ingredientDict[bubble.info.name].addValue;
        LogController.Instance.addLog(actionBubbleLogs[Random.Range(0, actionBubbleLogs.Length)], addValue>0 ?Color.white:Color.grey);
        actionBubble.consumeIngredient(bubble,this);
    }

    public void takeBubble(Bubble bubble)
    {
        if (bubble.isActionBubble)
        {
            attachAction((ActionBubble)bubble);
        }
        else
        {
            consumeIngredient((IngredientBubble)bubble);
        }
    }

    public bool canTakeBubble(Bubble bubble)
    {
        if(bubble.isActionBubble && actionBubble == null)
        {
            return true;
        }
        if (!bubble.isActionBubble && actionBubble != null) {
            return true;
        }
        return false;
    }
}
