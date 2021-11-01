using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientBubble : Bubble
{
    public IngredientBubbleInfo info;
    public override void init(BubbleInfo inf)
    {
        base.init(inf);
        info = (IngredientBubbleInfo)inf;
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
