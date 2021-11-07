using LitJson;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseInfo
{
    public string name;
    public string displayName;
}

public class BubbleInfo:BaseInfo
{

}

public class ActionBubbleInfo:BubbleInfo
{
    public List<string> log;
    public Dictionary<string, float> successAttribute;
    public Dictionary<string, float> failedAttribute;
}


public class EmotionBubbleInfo: BubbleInfo
{
}


public class BubbleManager : Singleton<BubbleManager>
{


    public List<ActionBubbleInfo> actionBubbles;
    public List<EmotionBubbleInfo> emotionBubbles;
    //public static BaseInfoWithProbability pickInfoWithProbability<T>(T[] list) where T : BaseInfoWithProbability
    //{
    //    List<float> probabilityList = new List<float>();
    //    float maxProb = 0;
    //    foreach (var info in list)
    //    {
    //        maxProb += info.possibility;
    //        probabilityList.Add(maxProb);
    //    }
    //    float rand = Random.Range(0f, maxProb);
    //    for (int i = 0; i < list.Length; i++)
    //    {
    //        if (rand <= probabilityList[i])
    //        {
    //            return list[i];
    //        }
    //    }
    //    Debug.LogError("pickInfoWithProbability reached somewhere wrong " + rand + " " + maxProb);
    //    return list[0];
    //}

    public Transform generateTransform;
    public Dictionary<string, ActionBubbleInfo> actionBubbleInfoDict = new Dictionary<string, ActionBubbleInfo>();
    public Dictionary<string, EmotionBubbleInfo> emotionBubbleInfoDict = new Dictionary<string, EmotionBubbleInfo>();

    public float generateTime = 2;
    float currentGenerateTime = 0;

    void Awake()
    {
        actionBubbles = CsvUtil.LoadObjects<ActionBubbleInfo>("Idea.csv");
        foreach (var info in actionBubbles)
        {
            actionBubbleInfoDict[info.name] = info;
        }

        emotionBubbles = CsvUtil.LoadObjects<EmotionBubbleInfo>("Emotion.csv");
        foreach (var info in emotionBubbles)
        {
            emotionBubbleInfoDict[info.name] = info;
        }
    }

    Vector3 getPosition()
    {
        return Utils.randomVector3_2d(generateTransform.position, 0.1f);
    }

    void rabbishGeneration()
    {

        //var bubblePrefab = Resources.Load<GameObject>("blob");
        //var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);
    }

    public Bubble actionBubbleGeneration(ActionBubbleInfo bubbleInfo, Vector3 position)
    {
        rabbishGeneration();
        var rand = Random.Range(0, actionBubbleInfoDict.Count);
        var bubblePrefab = Resources.Load<GameObject>("actionBubble");
        if(position == Vector3.positiveInfinity)
        {
            position = getPosition();
        }
        var go = Instantiate(bubblePrefab, position, Quaternion.identity, transform);
        Bubble result = go.GetComponent<Bubble>();
        result.init(bubbleInfo);
        return result;
    }

    public Bubble actionBubbleGeneration(ActionBubbleInfo bubbleInfo)
    {
        return actionBubbleGeneration(bubbleInfo, Vector3.positiveInfinity);
    }
    public void actionBubbleGeneration()
    {

        //var actionsBubbleList = actionBubbleInfoDict.Values.ToList();
        //var picked = pickInfoWithProbability(actionsBubbleList);

        //actionBubbleGeneration((ActionBubbleInfo) picked);
    }


    public Bubble ingredientBubbleGeneration(EmotionBubbleInfo bubbleInfo)
    {

        var bubblePrefab = Resources.Load<GameObject>("ingredientBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);


        go.GetComponent<Bubble>().init(bubbleInfo);
        return go.GetComponent<Bubble>();
    }
    public void ingredientBubbleGeneration()
    {



        //var actionsBubbleList = ingredientBubbleInfoDict.Values.ToList();
        //var picked = pickInfoWithProbability(actionsBubbleList);

        //ingredientBubbleGeneration((EmotionBubbleInfo)picked);
    }

    public Bubble specificBubbleGeneration(string bubbleName,Vector3 position)
    {
        if (actionBubbleInfoDict.ContainsKey(bubbleName))
        {
            return actionBubbleGeneration(actionBubbleInfoDict[bubbleName], position);
        }else if (emotionBubbleInfoDict.ContainsKey(bubbleName))
        {

            return  ingredientBubbleGeneration(emotionBubbleInfoDict[bubbleName]);
        }
        else
        {
            Debug.LogError("wrong bubble name to generate " + bubbleName);
        }
        return null;
    }

    public void bubbleGeneration()
    {

        rabbishGeneration();
        var rand = Random.Range(0, actionBubbleInfoDict.Count+ emotionBubbleInfoDict.Count*2);
        if(rand< actionBubbleInfoDict.Count)
        {
            actionBubbleGeneration();
        }
        else
        {

            ingredientBubbleGeneration();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //actionBubbleGeneration();
        //actionBubbleGeneration();
        //ingredientBubbleGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        //currentGenerateTime += Time.deltaTime;
        //if (currentGenerateTime >= generateTime)
        //{
        //    currentGenerateTime = 0;
        //    bubbleGeneration();
        //}
    }
}
