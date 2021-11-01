using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogPanel : MonoBehaviour
{
    public TMP_Text logLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(string str)
    {
        logLabel.text = str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
