using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeRequirementCell : MonoBehaviour
{
    public TMP_Text amountLabel;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(string name,int amount)
    {
        amountLabel.text = amount.ToString();
        if (AttributeManager.Instance.getAttributeValue(name) >= amount)
        {

            amountLabel.color = Color.black;
        }
        else
        {

            amountLabel.color = Color.red;
        }
        image.sprite = AttributeManager.Instance.attributeDict[name].icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
