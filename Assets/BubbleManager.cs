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
    public int gameProcess;
}


public class EmotionBubbleInfo: BubbleInfo
{
}


public class BubbleManager : Singleton<BubbleManager>
{
    public List<Color> emotionIdToColor = new List<Color> {Color.red,Color.blue };
    public Transform generateTransform;
    public Dictionary<string, ActionBubbleInfo> actionBubbleInfoDict = new Dictionary<string, ActionBubbleInfo>();
    public Dictionary<string, EmotionBubbleInfo> emotionBubbleInfoDict = new Dictionary<string, EmotionBubbleInfo>();

    public float emotionGenerateTime = 1;
    public float ideaGenerateTime = 2;
    float currentEmotionGenerateTime = 0;
    float currentIdeaGenerateTime = 0;

    void Awake()
    {
        var actionBubbles = CsvUtil.LoadObjects<ActionBubbleInfo>("Idea");
        foreach (var info in actionBubbles)
        {
            actionBubbleInfoDict[info.name] = info;
        }

        var emotionBubbles = CsvUtil.LoadObjects<EmotionBubbleInfo>("Emotion");
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
        if(position.Equals( Vector3.positiveInfinity))
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
    public void ideaBubbleGeneration()
    {
        var picked = Utils.pickRandomWithProbability(DevelopGameStageManager.Instance.currentStageInfo().ideas,10);
        if (picked!=default(string))
        {
            actionBubbleGeneration(actionBubbleInfoDict[picked]);

        }

    }


    public Bubble emotionBubbleGeneration(EmotionBubbleInfo bubbleInfo)
    {

        var bubblePrefab = Resources.Load<GameObject>("ingredientBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);


        go.GetComponent<Bubble>().init(bubbleInfo);
        return go.GetComponent<Bubble>();
    }
    public void emotionBubbleGeneration()
    {
        var actionsBubbleList = emotionBubbleInfoDict.Values.ToList();
        var pickedBubbleInfo = Utils.randomFromList(actionsBubbleList);

        emotionBubbleGeneration(pickedBubbleInfo);
    }

    public Bubble specificBubbleGeneration(string bubbleName,Vector3 position)
    {
        if (actionBubbleInfoDict.ContainsKey(bubbleName))
        {
            return actionBubbleGeneration(actionBubbleInfoDict[bubbleName], position);
        }else if (emotionBubbleInfoDict.ContainsKey(bubbleName))
        {

            return emotionBubbleGeneration(emotionBubbleInfoDict[bubbleName]);
        }
        else
        {
            Debug.LogError("wrong bubble name to generate " + bubbleName);
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        emotionGenerateTime = (float)GameManager.Instance.data["emotionBubbleGenerationTime"];
        ideaGenerateTime = (float)GameManager.Instance.data["ideaBubbleGenerationTime"];

        emotionBubbleGeneration();
        emotionBubbleGeneration();
        emotionBubbleGeneration();
        emotionBubbleGeneration();
        emotionBubbleGeneration();
        emotionBubbleGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        currentEmotionGenerateTime += Time.deltaTime;
        if (currentEmotionGenerateTime >= emotionGenerateTime)
        {
            currentEmotionGenerateTime = 0;
            emotionBubbleGeneration();
        }

        currentIdeaGenerateTime += Time.deltaTime;
        if (currentIdeaGenerateTime >= ideaGenerateTime)
        {
            currentIdeaGenerateTime = 0;
            ideaBubbleGeneration();
        }


    }
}
