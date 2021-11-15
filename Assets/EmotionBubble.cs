using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionBubble : Bubble
{
    public EmotionBubbleInfo info;
    public char symbol;
    public int value;
    public int emotionType;
    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        symbol = Utils.randomFromList(new List<char> { '+', '-' });
        value = Random.Range(1, 5);
        nameLable .text= symbol+value.ToString();
        emotionType = inf.name == "高兴" ? 0 : 1;
        rend.color = BubbleManager.Instance.emotionIdToColor[emotionType];
        info = (EmotionBubbleInfo)inf;
       // GetComponent<SpriteRenderer>().color = Color.blue;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
