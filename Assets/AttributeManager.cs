using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeInfo : BaseInfo
{
    public float value;
    public int isGame;
    public int deprecated;
    public string iconString;
    public Sprite icon { get { return Resources.Load<Sprite>("icons/" + iconString + "_2"); } }
    public Sprite emptyIcon { get { return Resources.Load<Sprite>("icons/" + iconString + "_1"); } }
    public Sprite textIcon { get { return Resources.Load<Sprite>("icons/" + iconString + "_3"); } }
    public bool isUsed { get { return deprecated != 1; } }
}


public class AttributeManager : Singleton<AttributeManager>
{
    public List <AttributeInfo> attributeList;
    public Dictionary<string,AttributeInfo> attributeDict = new Dictionary<string, AttributeInfo>();
    public Dictionary<string, Transform> attributeCellTrans = new Dictionary<string, Transform>();

    List<string> gameAttributes = new List<string>();

    public float gameAttributesValue()
    {
        float res = 0;
        foreach(var att in gameAttributes)
        {
            if (!attributeDict.ContainsKey(att))
            {
                Debug.Log("no game attribute " + att);
            }
            res += attributeDict[att].value;
        }
        return res;
    }

    // Start is called before the first frame update
    void Awake()
    {
        attributeList = CsvUtil.LoadObjects<AttributeInfo>("Attribute");
        foreach (var info in attributeList)
        {
            attributeDict[info.name] = info;
            if (info.isGame == 1)
            {
                gameAttributes.Add(info.name);
            }
            else
            {
                info.value = 50;
            }
        }
    }

    public void addOneAttribute(string n, float v)
    {
        if (attributeDict.ContainsKey(n))
        {
            attributeDict[n].value += v;
        }
        else
        {
            Debug.LogError("no attribute " + n);
        }
    }

    public void addAttribute(string n, float v)
    {
        addOneAttribute(n, v);
        EventPool.Trigger("attributeUpdate");
    }
    public bool canSurvive()
    {
        foreach (var pair in attributeDict.Keys)
        {
            if (!(attributeDict[pair].isGame == 1))
            {
                if(attributeDict[pair].value <= 0)
                {
                    return false;
                }

            }
        }
        return true;
    }
    public void reduceAllAttributes(int amount)
    {
        foreach(var pair in attributeDict.Keys)
        {
            if (!(attributeDict[pair].isGame == 1))
            {
                attributeDict[pair].value -= amount;

            }
        }
        EventPool.Trigger("attributeUpdate");
        EventPool.Trigger("checkGameOver");
    }
    public void addAttributes(Dictionary<string,float> dict)
    {
        foreach(var pair in dict)
        {
            addOneAttribute(pair.Key, pair.Value);
        }
        EventPool.Trigger("attributeUpdate");
    }

    public float getAttributeValue(string n)
    {
        if (attributeDict.ContainsKey(n))
        {
            return attributeDict[n].value;
        }
        else
        {
            Debug.LogError("no attribute " + n);
            return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
