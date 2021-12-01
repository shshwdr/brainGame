using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalCell : MonoBehaviour
{
    public Image emptyImage;
    public Image fullImage;
    public Image textImage;
    public Transform collectTransform;
    public string name;
    public TMP_Text valueLabe;

    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("attributeUpdate",onAttributeUpdate);
        onAttributeUpdate();
    }
    void onAttributeUpdate()
    {
        if (AttributeManager.Instance.attributeDict.ContainsKey(name))
        {
            AttributeManager.Instance.attributeCellTrans[name] = collectTransform;
            var info = AttributeManager.Instance.attributeDict[name];
            emptyImage.sprite = info.emptyIcon;
            fullImage.sprite = info.icon;
            textImage.sprite = info.textIcon;
            fullImage.fillAmount = AttributeManager.Instance.getAttributeValue(name) / (float)GameManager.Instance.data["maxAttributeValue"];
            valueLabe.text = AttributeManager.Instance.getAttributeValue(name).ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
