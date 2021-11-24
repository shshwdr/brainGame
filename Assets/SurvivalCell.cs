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
        AttributeManager.Instance.attributeCellTrans[name] = collectTransform;
        var info = AttributeManager.Instance.attributeDict[name];
        emptyImage.sprite = info.emptyIcon;
        fullImage.sprite = info.icon;
        textImage.sprite = info.textIcon;
        onAttributeUpdate();
        EventPool.OptIn("attributeUpdate",onAttributeUpdate);
    }
    void onAttributeUpdate()
    {
        fullImage.fillAmount = AttributeManager.Instance.getAttributeValue(name) / 100f;
        valueLabe.text = name+" " +AttributeManager.Instance.getAttributeValue(name).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
