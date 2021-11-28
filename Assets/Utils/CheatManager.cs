using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : Singleton<CheatManager>
{
    public bool hasUnlimitResource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            hasUnlimitResource = !hasUnlimitResource;
        }
        for(int i = 0; i < 7; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                var attributeInfo = AttributeManager.Instance.attributeList[i];
                if (Input.GetKey(KeyCode.Space))
                {

                    AttributeManager.Instance.addAttribute(attributeInfo.name, -10);
                    EventPool.Trigger("checkGameOver");
                }
                else
                {

                    AttributeManager.Instance.addAttribute(attributeInfo.name, 10);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            EventPlanManager.Instance.gotoTheLastDay();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventPlanManager.Instance.addDay();
        }
    }
}
