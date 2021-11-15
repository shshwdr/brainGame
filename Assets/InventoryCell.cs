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
        Inventory.Instance.consumeItem(index);
    }

    public void init(int ind, string na)
    {
        index = ind;
        name = na;
        nameLabel.text = name;
        image.color = BubbleManager.Instance.emotionBubbleInfoDict[na].color;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
