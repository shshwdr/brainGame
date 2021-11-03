using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseInfo
{
    public string name;
}

public class BaseInfoWithProbability:BaseInfo
{
    public float possibility;
}
public class ActionIngredientResult:BaseInfoWithProbability
{

    public string[] logs;
    public float addValue;
}

public class ActionResult: BaseInfoWithProbability
{
    public string[] logs;
    public List<string> rewards;
}

public class BubbleInfo: BaseInfoWithProbability
{
    public string displayName;

}
public class ActionBubbleInfo: BubbleInfo
{
    public string[] logs;
    public ActionIngredientResult[] ingredients;
    public Dictionary<string, ActionIngredientResult> ingredientDict;

    public ActionResult[] successResults;

    public ActionResult[] failedResults;
    public float duration;

}
public class IngredientBubbleInfo: BubbleInfo
{
}

public class AllBubblesInfo
{
    public List<ActionBubbleInfo> actionBubble;
    public List<IngredientBubbleInfo> ingredientBubble;
}

public class BubbleManager : Singleton<BubbleManager>
{
    public static BaseInfoWithProbability pickInfoWithProbability<T>(T[] list) where T : BaseInfoWithProbability
    {
        List<float> probabilityList = new List<float>();
        float maxProb = 0;
        foreach (var info in list)
        {
            maxProb += info.possibility;
            probabilityList.Add(maxProb);
        }
        float rand = Random.Range(0f, maxProb);
        for (int i = 0; i < list.Length; i++)
        {
            if (rand <= probabilityList[i])
            {
                return list[i];
            }
        }
        Debug.LogError("pickInfoWithProbability reached somewhere wrong " + rand + " " + maxProb);
        return list[0];
    }
        public static BaseInfoWithProbability pickInfoWithProbability<T>(List<T> list) where T : BaseInfoWithProbability
    {
        return pickInfoWithProbability(list.ToArray());
    }
    public Transform generateTransform;
    public Dictionary<string, ActionBubbleInfo> actionBubbleInfoDict = new Dictionary<string, ActionBubbleInfo>();
    public Dictionary<string, IngredientBubbleInfo> ingredientBubbleInfoDict = new Dictionary<string, IngredientBubbleInfo>();

    public float generateTime = 2;
    float currentGenerateTime = 0;

    void Awake()
    {
        string text = Resources.Load<TextAsset>("json/BrainBubble").text;
        var allNPCs = JsonMapper.ToObject<AllBubblesInfo>(text);
        foreach (var info in allNPCs.actionBubble)
        {
            actionBubbleInfoDict[info.name] = info;
            info.ingredientDict = new Dictionary<string, ActionIngredientResult>();
            foreach(var ingredient in info.ingredients)
            {
                info.ingredientDict[ingredient.name] = ingredient;
            }
        }
        foreach (var info in allNPCs.ingredientBubble)
        {
            ingredientBubbleInfoDict[info.name] = info;
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

    public void actionBubbleGeneration(ActionBubbleInfo bubbleInfo)
    {
        rabbishGeneration();
        var rand = Random.Range(0, actionBubbleInfoDict.Count);
        var bubblePrefab = Resources.Load<GameObject>("actionBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);

        go.GetComponent<Bubble>().init(bubbleInfo);
    }
    public void actionBubbleGeneration()
    {

        var actionsBubbleList = actionBubbleInfoDict.Values.ToList();
        var picked = pickInfoWithProbability(actionsBubbleList);

        actionBubbleGeneration((ActionBubbleInfo) picked);
    }


    public void ingredientBubbleGeneration(IngredientBubbleInfo bubbleInfo)
    {

        var bubblePrefab = Resources.Load<GameObject>("ingredientBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);


        go.GetComponent<Bubble>().init(bubbleInfo);
    }
    public void ingredientBubbleGeneration()
    {



        var actionsBubbleList = ingredientBubbleInfoDict.Values.ToList();
        var picked = pickInfoWithProbability(actionsBubbleList);

        ingredientBubbleGeneration((IngredientBubbleInfo)picked);
    }

    public void specificBubbleGeneration(string bubbleName)
    {
        if (actionBubbleInfoDict.ContainsKey(bubbleName))
        {
            actionBubbleGeneration(actionBubbleInfoDict[bubbleName]);
        }else if (ingredientBubbleInfoDict.ContainsKey(bubbleName))
        {

            ingredientBubbleGeneration(ingredientBubbleInfoDict[bubbleName]);
        }
        else
        {
            Debug.LogError("wrong bubble name to generate " + bubbleName);
        }
    }

    public void bubbleGeneration()
    {

        rabbishGeneration();
        var rand = Random.Range(0, actionBubbleInfoDict.Count+ ingredientBubbleInfoDict.Count*2);
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
        actionBubbleGeneration();
        actionBubbleGeneration();
        ingredientBubbleGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        currentGenerateTime += Time.deltaTime;
        if (currentGenerateTime >= generateTime)
        {
            currentGenerateTime = 0;
            bubbleGeneration();
        }
    }
}
