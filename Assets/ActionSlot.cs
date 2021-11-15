using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
    //ActionBubble actionBubble;
    //public Image overlayImage;
    //Coroutine coroutine;
    //public TMP_Text timeLabel;
    //float score = 0;
    //public float baseScore = 10;
    //bool isFinished = false;
    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (transform.position.x < -7)
    //    {

    //        Destroy(gameObject, 30);
    //    }
    //}

    ////private void OnTriggerEnter2D(Collider2D collision)
    ////{
    ////    if(collision.GetComponent<PlayerBubble>() && !isFinished)
    ////    {
    ////        doAction(collision.GetComponent<PlayerBubble>());
    ////    }
    ////}

    ////void doAction(PlayerBubble playerBubble)
    ////{
    ////    isFinished = true;

    ////    //Debug.Log("do action " + actionBubble.info.name);
    ////    string logString = "在" + timeLabel.text + actionBubble.info.log[0];



    ////    //detect if success or not
    ////    //int rand = Random.Range(0, 100);
    ////    //int success = rand > score ? 0 : 1;

    ////    int success = playerBubble.isSuccess(actionBubble);

    ////    string finalLog = LogController.Instance.getActionLog(actionBubble.info.name, success);
    ////    logString += finalLog;


    ////    LogController.Instance.addLog(logString, Color.white);

    ////    //give reward
    ////    var attribute = success == 1 ? actionBubble.info.successAttribute : actionBubble.info.failedAttribute;

    ////    AttributeManager.Instance.addAttributes(attribute);


    ////    if (success == 1 && actionBubble.info.gameProcess > 0)
    ////    {
    ////        DevelopGameStageManager.Instance.addProcess(AttributeManager.Instance.gameAttributesValue());
    ////    }
    ////    if (success == 1)
    ////    {
    ////        actionBubble.rend.color = Color.green;
    ////    }
    ////    else
    ////    {

    ////        actionBubble.rend.color = Color.grey;
    ////    }
    ////}
    ////public void init(string idea,string time,Vector3 position)
    ////{
    ////    timeLabel.text = time;

    ////    Bubble bubble = BubbleManager.Instance.specificBubbleGeneration(idea, position);
    ////    attachAction((ActionBubble)bubble);
    ////}

    ////public void removeAction()
    ////{
    ////    actionBubble = null;
    ////    overlayImage.gameObject.SetActive(false);
    ////    DOTween.Kill(overlayImage.fillAmount);
    ////    StopCoroutine(coroutine);
    ////}

    //void updateOverlay()
    //{
    //    //overlayImage.fillAmount = score/100f;
    //}

    ////public void attachAction(ActionBubble bubble)
    ////{
    ////    if (actionBubble)
    ////    {
    ////        Destroy(actionBubble.gameObject);

    ////    }
    ////    actionBubble = bubble;
    ////    //LogController.Instance.addLog(bubble.info.logs[Random.Range(0, bubble.info.logs.Length)], Color.yellow);
    ////    // overlayImage.gameObject.SetActive(true);

    ////    bubble.transform.position = transform.position;
    ////    bubble.transform.rotation = Quaternion.identity;
    ////    bubble.attachToSlot(this);
    ////    updateOverlay();
    ////    //Debug.Log("attach action " + actionBubble.info.name + " " + actionBubble.info.duration);
    ////    //DOTween.To(() => overlayImage.fillAmount, x => overlayImage.fillAmount = x, 1, actionBubble.info.duration);

    ////    //coroutine = StartCoroutine(finishAction(actionBubble.info.duration));
    ////}


    ////public IEnumerator finishAction(float duration)
    ////{
    ////    yield return new WaitForSeconds(duration);
    ////    if (actionBubble)
    ////    {
    ////        actionBubble.failed();
    ////        removeAction();
    ////    }
    ////}

    //public virtual void consumeEmotion(EmotionBubble bubble)
    //{

    //   // var value = BubbleCalculator.Instance.calculateAddScore(actionBubble.info.name, bubble.info.name);
    //    //score += value*baseScore;
    //    Destroy(bubble.gameObject);

    //    updateOverlay();
    //    //var actionBubbleLogs = actionBubble.info.emotionDict[bubble.info.name].logs;
    //    //var addValue = actionBubble.info.ingredientDict[bubble.info.name].addValue;
    //    //LogController.Instance.addLog(actionBubbleLogs[Random.Range(0, actionBubbleLogs.Length)], addValue > 0 ? Color.white : Color.grey);
    //    actionBubble.consumeIngredient(bubble, this);
    //}

    ////public void takeBubble(Bubble bubble)
    ////{
    ////    if (bubble.isActionBubble)
    ////    {
    ////        attachAction((ActionBubble)bubble);
    ////    }
    ////    else
    ////    {
    ////        consumeEmotion((EmotionBubble)bubble);
    ////    }
    ////}

    //public virtual bool canTakeBubble(Bubble bubble)
    //{
    //    return !isFinished;
    //    //if(bubble.isActionBubble && actionBubble == null)
    //    //{
    //    //    return true;
    //    //}
    //    //if (!bubble.isActionBubble && actionBubble != null) {
    //    //    return true;
    //    //}
    //    //return false;
    //}

    //void OnBecameInvisible()
    //{
    //    //Destroy(gameObject);
    //}
    //private void OnDestroy()
    //{
    //    Destroy(actionBubble.gameObject);
    //}
}
