using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionBubble : Bubble
{
    public EmotionBubbleInfo info;
    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        info = (EmotionBubbleInfo)inf;
        GetComponent<SpriteRenderer>().color = Color.blue;
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
