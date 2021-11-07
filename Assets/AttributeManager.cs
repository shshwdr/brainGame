using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeInfo : BaseInfo
{
    public float value;
}

public class AttributeManager : Singleton<AttributeManager>
{
    public Dictionary<string,AttributeInfo> attributeDict = new Dictionary<string, AttributeInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        var actionBubbles = CsvUtil.LoadObjects<AttributeInfo>("Attribute.csv");
        foreach (var info in actionBubbles)
        {
            attributeDict[info.name] = info;
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
