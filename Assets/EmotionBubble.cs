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
        //info = inf;
        //symbol = Utils.randomFromList(new List<char> { '+', '-' });
        //value = Random.Range(1, 5);
        nameLable .text= inf.displayName;
        //emotionType = inf.name == "高兴" ? 0 : 1;
        //rend.color = BubbleManager.Instance.emotionIdToColor[emotionType];
        info = (EmotionBubbleInfo)inf;
        //rend.color = info.color;
        rend.sprite = info.icon;
       // GetComponent<SpriteRenderer>().color = Color.blue;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnMouseDown()
    {
        if (PauseButtonController.Instance.isPaused)
        {
            return;
        }
        //Debug.Log("on mouse down");
        if (Inventory.Instance.canAddItem(info.name))
        {

            MusicManager.Instance.playOneShot(audioClip);
            Inventory.Instance.addItem(info.name);
            Destroy(gameObject);
        }
        else
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
