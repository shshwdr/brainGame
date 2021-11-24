using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    public Image image;
    public TMP_Text nameLabel;
    public Button button;
    int index;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(delegate { onClick(); });
    }

    void onClick()
    {
        if (PauseButtonController.Instance.isPaused)
        {
            return;
        }
        if (index >= 0)
        {

            Inventory.Instance.consumeItem(index);
        }
    }

    public void init(int ind, string na)
    {
        index = ind;
        name = na;
        var info = BubbleManager.Instance.emotionBubbleInfoDict[name];
       // nameLabel.text = info.displayName;
        image.sprite = Resources.Load<Sprite>("icons/" + name);
        //image.color = Color.white;
    }

    public void initDark()
    {
        index = -1;
        image.sprite = Resources.Load<Sprite>("icons/star_dark");
        //image.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
