using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    ActionBubble actionBubble;
    public Image overlayImage;
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
        DOTween.KillAll();
        StopAllCoroutines();
    }

    public void attachAction(ActionBubble bubble)
    {
        actionBubble = bubble;
        LogController.Instance.addLog(bubble.info.logs[Random.Range(0, bubble.info.logs.Length)]);
        overlayImage.gameObject.SetActive(true);
        overlayImage.fillAmount = 0;
        DOTween.To(() => overlayImage.fillAmount, x => overlayImage.fillAmount = x, 1, actionBubble.info.duration);

        StartCoroutine(finishAction(actionBubble.info.duration));
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
        var actionBubbleLogs = actionBubble.info.ingredients[0].logs;
        LogController.Instance.addLog(actionBubbleLogs[Random.Range(0, actionBubbleLogs.Length)]);
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
