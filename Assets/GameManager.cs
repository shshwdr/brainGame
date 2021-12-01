using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.ComponentModel;

public class GameManager : Singleton<GameManager>
{
    public JsonData data;
    // Start is called before the first frame update
    void Awake()
    {
        var json_text = "";


#if UNITY_EDITOR
        json_text = Resources.Load<TextAsset>("json/gameManager").text;
#else
            var m_path = Application.dataPath+ "/json/gameManager.json";
         if (File.Exists(m_path))
         {
             byte[] m_bytes = File.ReadAllBytes(m_path);
 
             json_text = System.Text.Encoding.UTF8.GetString(m_bytes);
 
             //Debug.Log(json_text);
         }else{
            Debug.LogError(m_path);
         }
#endif


        data = JsonMapper.ToObject(json_text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
