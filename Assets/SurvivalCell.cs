using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalCell : MonoBehaviour
{
    public Image image;
    public string name;
    public TMP_Text valueLabe;
    // Start is called before the first frame update
    void Start()
    {
        onAttributeUpdate();
        EventPool.OptIn("attributeUpdate",onAttributeUpdate);
    }
    void onAttributeUpdate()
    {
        image.fillAmount = AttributeManager.Instance.getAttributeValue(name) / 100f;
        valueLabe.text = name+" " +AttributeManager.Instance.getAttributeValue(name).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
