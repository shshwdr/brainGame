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
    Dictionary<string, int> emotionToId = new Dictionary<string, int>();
    public Dictionary<string, List<CompareExpressionRange>> ideaEmotionRelationship = new Dictionary<string, List<CompareExpressionRange>>();
    // Start is called before the first frame update
    void Awake()
    {
        var ideaEmtionMap = CsvUtil.LoadList("IdeaEmotionMap",true);
        for (int i = 1;i < ideaEmtionMap[0].Count;i++) {

            var emo = ideaEmtionMap[0][i];
            emotionToId[emo] = i - 1;
        }
        ideaEmtionMap.RemoveAt(0);
        foreach (var row in ideaEmtionMap)
        {
            var row0 = row[0];
            ideaEmotionRelationship[row0] = new List<CompareExpressionRange>();
            row.RemoveAt(0);
            foreach(string value in row)
            {
                CompareExpressionRange expression = new CompareExpressionRange();
                if (value.Length > 0)
                {
                    expression.symbol = value[0];
                    string number = value.Substring(1, value.Length-1);
                    string[] split = number.Split(',');
                    expression.minValue = int.Parse(split[0]);
                    expression.maxValue = int.Parse(split[1]);
                    ideaEmotionRelationship[row0].Add(expression);
                }
                else
                {
                    ideaEmotionRelationship[row0].Add(null);
                }
            }
        }
    }


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

    // Update is called once per frame
    void Update()
    {
        
    }
}
