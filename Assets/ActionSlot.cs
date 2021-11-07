using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    ActionBubble actionBubble;
    public Image overlayImage;
    Coroutine coroutine;
    public TMP_Text timeLabel;
    float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "actionBar")
        {
            doAction();
        }
    }

    void doAction()
    {

        //Debug.Log("do action " + actionBubble.info.name);
        string logString = "在" + timeLabel.text + actionBubble.info.log[0];



        //detect if success or not
        int rand = Random.Range(0, 100);
        int success = rand > score ? 0 : 1;

        string finalLog = LogController.Instance.getActionLog(actionBubble.info.name, success);
        logString += finalLog;

        
        LogController.Instance.addLog(logString, Color.white);

        //give reward
        var attribute = success == 1 ? actionBubble.info.successAttribute : actionBubble.info.failedAttribute;

        AttributeManager.Instance.addAttributes(attribute);
    }
    public void init(string idea,string time,Vector3 position)
    {
        timeLabel.text = time;

        Bubble bubble = BubbleManager.Instance.specificBubbleGeneration(idea, position);
        attachAction((ActionBubble)bubble);
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
        //LogController.Instance.addLog(bubble.info.logs[Random.Range(0, bubble.info.logs.Length)], Color.yellow);
        overlayImage.gameObject.SetActive(true);
        overlayImage.fillAmount = 0;

        bubble.attachToSlot(this);
       // StartCoroutine(delayAttach(bubble));
        //Debug.Log("attach action " + actionBubble.info.name + " " + actionBubble.info.duration);
        //DOTween.To(() => overlayImage.fillAmount, x => overlayImage.fillAmount = x, 1, actionBubble.info.duration);

        //coroutine = StartCoroutine(finishAction(actionBubble.info.duration));
    }

    IEnumerator delayAttach(ActionBubble bubble)
    {
        yield return new WaitForSeconds(0.1f);

        bubble.attachToSlot(this);
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
        //Destroy(bubble.gameObject);
        //var actionBubbleLogs = actionBubble.info.ingredientDict[bubble.info.name].logs;
        //var addValue = actionBubble.info.ingredientDict[bubble.info.name].addValue;
        //LogController.Instance.addLog(actionBubbleLogs[Random.Range(0, actionBubbleLogs.Length)], addValue>0 ?Color.white:Color.grey);
        //actionBubble.consumeIngredient(bubble,this);
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
