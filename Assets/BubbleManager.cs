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

public class CollectionBubbleInfo : ActionBubbleInfo
{

    public Sprite icon { get { return Resources.Load<Sprite>("icons/" + iconString); } }
    public string iconString;
}


public class ActionBubbleInfo:BubbleInfo
{
    public List<string> log;
    public Dictionary<string, float> successAttribute;
    public Dictionary<string, float> failedAttribute;
    public int gameProcess;

    public Dictionary<string, float> isLockedBy;
    public int happy;
    public int sad;
    public int angry;
    public int afraid;
    public int excited;
    public int calm;
    public string category;

    public int getEmotionRequirement(string emo)
    {
        switch (emo)
        {
            case "happy":
                return happy;
            case "sad":
                return sad;
            case "angry":
                return angry;
            case "afraid":
                return afraid;
            case "excited":
                return excited;
            case "calm":
                return calm;
        }
        Debug.LogError("wrong emotion required"+ emo);
        return 0;
    }


    public int finishedTime;
    public bool isUnlocked;
}


public class EmotionBubbleInfo: BubbleInfo
{
    public string colorString;
    public Color color { get { return Utils.ToColor(colorString); } }
    public int deprecated;
    public bool isUsed { get { return deprecated != 1; } }
    public Sprite icon { get { return Resources.Load<Sprite>("icons/" + iconString ); } }
    public string iconString;
}


public class BubbleManager : Singleton<BubbleManager>
{
    public List<Color> emotionIdToColor = new List<Color> {Color.red,Color.blue };
    public Transform generateTransform;
    Transform[] generateTransforms;
    public List<string> gameIdeas = new List<string>();
    public Dictionary<string, ActionBubbleInfo> actionBubbleInfoDict = new Dictionary<string, ActionBubbleInfo>();
    public Dictionary<string, EmotionBubbleInfo> emotionBubbleInfoDict = new Dictionary<string, EmotionBubbleInfo>();
    public Dictionary<string, CollectionBubbleInfo> collectionBubbleInfoDict = new Dictionary<string, CollectionBubbleInfo>();
    public Dictionary<string, List<string>> categoryToIds = new Dictionary<string, List<string>>();

    public Transform collectionParentTransform;
    public Dictionary<string, List<Transform>> collectionPositions = new Dictionary<string, List<Transform>>();

    public float emotionGenerateTime = 1;
    public float ideaGenerateTime = 2;
    public float dayIdeaGenerateTime = 2;
    public float collectionGenerateTime = 2;
    float currentEmotionGenerateTime = 0;
    float currentIdeaGenerateTime = 0;
    float currentCollectionGenerateTime = 0;

    void Awake()
    {
        generateTransforms = generateTransform.GetComponentsInChildren<Transform>();

        var actionBubbles = CsvUtil.LoadObjects<ActionBubbleInfo>("Idea");
        foreach (var info in actionBubbles)
        {
            actionBubbleInfoDict[info.name] = info;
            if (info.isLockedBy.Count == 0)
            {
                info.isUnlocked = true;

                if (!categoryToIds.ContainsKey(info.category))
                {
                    categoryToIds[info.category] = new List<string>();
                }
                categoryToIds[info.category].Add(info.name);
                if (info.gameProcess == 1)
                {
                    gameIdeas.Add(info.name);
                }
            }
        }

        var emotionBubbles = CsvUtil.LoadObjects<EmotionBubbleInfo>("Emotion");
        foreach (var info in emotionBubbles)
        {
            if (info.isUsed)
            {

                emotionBubbleInfoDict[info.name] = info;
            }
        }



        foreach (Transform tran in collectionParentTransform)
        {
            collectionPositions[tran.name] = tran.GetComponentsInChildren<Transform>().ToList();
            collectionPositions[tran.name].RemoveAt(0);
        }

        var collectionBubbles = CsvUtil.LoadObjects<CollectionBubbleInfo>("collection");
        foreach (var info in collectionBubbles)
        {
            if (collectionPositions.ContainsKey(info.name))
            {

                collectionBubbleInfoDict[info.name] = info;
            }
        }
    }

    Vector3 getPosition()
    {
        Transform selectTransform =  Utils.randomFromArray(generateTransforms);

        return Utils.randomVector3_2d(selectTransform.position, 0.1f);
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

    public Bubble collectionBubbleGeneration(CollectionBubbleInfo bubbleInfo)
    {
        rabbishGeneration();
        var rand = Random.Range(0, actionBubbleInfoDict.Count);
        var bubblePrefab = Resources.Load<GameObject>("collectionBubble");
       // if (position.Equals(Vector3.positiveInfinity))
        //{
            var position = getPosition();
        //}
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
        var picked = Utils.randomFromList(gameIdeas);
       // var picked = Utils.pickRandomWithProbability(DevelopGameStageManager.Instance.currentStageInfo().ideas,10);
        if (picked!=default(string))
        {
            actionBubbleGeneration(actionBubbleInfoDict[picked]);

        }

    }

    public void collectionBubbleGeneration()
    {
        var picked = Utils.randomFromList(collectionBubbleInfoDict.Values.ToList());
        // var picked = Utils.pickRandomWithProbability(DevelopGameStageManager.Instance.currentStageInfo().ideas,10);
        //if (picked != default(string))
        {
            collectionBubbleGeneration(picked);

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
        if (categoryToIds.ContainsKey(bubbleName))
        {
            //if (categoryToIds.ContainsKey(bubbleName))
            //{

                return actionBubbleGeneration(actionBubbleInfoDict[Utils.randomFromList(categoryToIds[ bubbleName])], position);
            //}
            //else
            //{

                //Debug.LogError("wrong catelog name to generate " + bubbleName);
            //}
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
        dayIdeaGenerateTime = (float)GameManager.Instance.data["dayIdeaBubbleGenerationTime"];
        collectionGenerateTime = (float)GameManager.Instance.data["collectionBubbleGenerationTime"];



    }

    public void reduceEmotionGenerationTime(float time)
    {
        emotionGenerateTime -= time;
    }

    // Update is called once per frame
    void Update()
    {
        if (DefaultDayManager.Instance.isResting())
        {
            return;
        }
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

        currentCollectionGenerateTime += Time.deltaTime;
        if (currentCollectionGenerateTime >= collectionGenerateTime)
        {
            currentCollectionGenerateTime = 0;
            collectionBubbleGeneration();
        }


    }
}
