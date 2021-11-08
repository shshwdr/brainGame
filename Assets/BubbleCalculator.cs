using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleCalculator : Singleton<BubbleCalculator>
{
    Dictionary<string, int> emotionToId = new Dictionary<string, int>();
    public Dictionary<string, List<float>> ideaEmotionRelationship = new Dictionary<string, List<float>>();
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
            ideaEmotionRelationship[row0] = new List<float>();
            row.RemoveAt(0);
            foreach(var value in row)
            {
                float floatValue;
                bool success = float.TryParse(value, out floatValue);
                if (success)
                {
                    ideaEmotionRelationship[row0].Add(floatValue);
                }
                else
                {
                    Debug.LogError("failed to parse into float " + value);
                }
            }
        }
    }

    public float calculateAddScore(string idea, string emotion)
    {
        if (!emotionToId.ContainsKey(emotion))
        {
            Debug.LogError("no emotion exists " + emotion);
            return 0;
        }
        var emotionId = emotionToId[emotion];
        return ideaEmotionRelationship[idea][emotionId];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
