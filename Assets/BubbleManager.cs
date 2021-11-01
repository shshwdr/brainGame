using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionIngredientResult {

    public string name;
    public string[] logs;
    public float addValue;
}

public class ActionResult
{
    public string[] logs;
}

public class BubbleInfo
{
    public string name;
    public string displayName;

}
public class ActionBubbleInfo: BubbleInfo
{
    public string[] logs;
    public ActionIngredientResult[] ingredients;

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

public class BubbleManager : MonoBehaviour
{
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

    public void actionBubbleGeneration()
    {

        var rand = Random.Range(0, actionBubbleInfoDict.Count);
        var bubblePrefab = Resources.Load<GameObject>("actionBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);
        go.GetComponent<Bubble>().init(actionBubbleInfoDict["makeGame"]);
    }

    public void ingredientBubbleGeneration()
    {


        var bubblePrefab = Resources.Load<GameObject>("ingredientBubble");
        var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);
        go.GetComponent<Bubble>().init(ingredientBubbleInfoDict["passion"]);
    }

    public void bubbleGeneration()
    {

    var rand = Random.Range(0, actionBubbleInfoDict.Count+ ingredientBubbleInfoDict.Count);
        if(rand< actionBubbleInfoDict.Count)
        {
            var bubblePrefab = Resources.Load<GameObject>("actionBubble");
            var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);
            go.GetComponent<Bubble>().init(actionBubbleInfoDict["makeGame"]);
        }
        else
        {

            var bubblePrefab = Resources.Load<GameObject>("ingredientBubble");
            var go = Instantiate(bubblePrefab, getPosition(), Quaternion.identity, transform);
            go.GetComponent<Bubble>().init(ingredientBubbleInfoDict["passion"]);
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
