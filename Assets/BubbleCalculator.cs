using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareExpressionRange {
    public char symbol;
    public int minValue;
    public int maxValue;
}
public class CompareExpression
{

    public char symbol;
    public int value;
    public string ToString()
    {
        if(symbol == '\0')
        {
            return value.ToString();
        }
        return symbol + value.ToString(); 
    }
    public CompareExpression(CompareExpressionRange range)
    {
        symbol = range.symbol;
        value = Random.Range(range.minValue, range.maxValue); 
    }
}

public class BubbleCalculator : Singleton<BubbleCalculator>
{
    //public Dictionary<string, int> emotionToId = new Dictionary<string, int>();
    //public Dictionary<int, string> IdToEmotion = new Dictionary<int, string>();
    //public Dictionary<string, List<float>> ideaEmotionRelationship = new Dictionary<string, List<float>>();
    //// Start is called before the first frame update
    //void Awake()
    //{
    //    var ideaEmtionMap = CsvUtil.LoadList("IdeaEmotionMap", true);
    //    for (int i = 1; i < ideaEmtionMap[0].Count; i++)
    //    {

    //        var emo = ideaEmtionMap[0][i];
    //        emotionToId[emo] = i - 1;
    //        IdToEmotion[i - 1] = emo;
    //    }
    //    ideaEmtionMap.RemoveAt(0);
    //    foreach (var row in ideaEmtionMap)
    //    {
    //        var row0 = row[0];
    //        ideaEmotionRelationship[row0] = new List<float>();
    //        row.RemoveAt(0);
    //        foreach (var value in row)
    //        {
    //            float floatValue;
    //            bool success = float.TryParse(value, out floatValue);
    //            if (success)
    //            {
    //                ideaEmotionRelationship[row0].Add(floatValue);
    //            }
    //            else
    //            {
    //                if (value == "")
    //                {
    //                    ideaEmotionRelationship[row0].Add(0);
    //                }
    //                else
    //                {

    //                    Debug.LogError("failed to parse into float " + value);
    //                }
    //            }
    //        }

    //        for(int i = row.Count; i< IdToEmotion.Count; i++)
    //        {
    //            ideaEmotionRelationship[row0].Add(0);
    //        }
    //    }
    //}

    //public float calculateAddScore(string idea, string emotion)
    //{
    //    if (!emotionToId.ContainsKey(emotion))
    //    {
    //        Debug.LogError("no emotion exists " + emotion);
    //        return 0;
    //    }
    //    var emotionId = emotionToId[emotion];
    //    return ideaEmotionRelationship[idea][emotionId];
    //}
}
