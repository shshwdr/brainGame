using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public JsonData data;
    // Start is called before the first frame update
    void Awake()
    {
        var json_text = Resources.Load<TextAsset>("json/gameManager").text;
         data = JsonMapper.ToObject(json_text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
