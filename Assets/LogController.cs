using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionLog {
    public string idea;
    public int success;
    public List<string> log;
}



public class LogController : Singleton<LogController>
{
    public Transform content;
    ScrollRect scrollRect;
    public Dictionary<string, Dictionary<int,List<string>>> actionBubbleInfoDict = new Dictionary<string, Dictionary<int, List<string>>>();

    private void Awake()
    {

        var logs = CsvUtil.LoadObjects<ActionLog>("Log");
        foreach (var info in logs)
        {
            if (!actionBubbleInfoDict.ContainsKey(info.idea)){

                actionBubbleInfoDict[info.idea] = new Dictionary<int, List<string>>();
            }

            actionBubbleInfoDict[info.idea][info.success] = info.log;
        }
    }

    public string getActionLog(string actionName, int success)
    {
        if (!actionBubbleInfoDict.ContainsKey(actionName))
        {
            Debug.LogError("action name does not exist in log " + actionName);
            return "";
        }
        if (!actionBubbleInfoDict[actionName].ContainsKey(success))
        {
            Debug.LogError("action success does not exist in log " + actionName+" "+success);
            return "";
        }
        return Utils.randomFromList( actionBubbleInfoDict[actionName][success]);
    }

    public static void ScrollToTop(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    public static void ScrollToBottom(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    public void addLog(string str,Color color)
    {
        var logPrefab = Resources.Load<GameObject>("log");
        var go = Instantiate(logPrefab);
        go.GetComponent<LogPanel>().init(str,color);
        go.transform.parent = content;

        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        if (scrollRect.GetComponent<RectTransform>().rect.height <= content.GetComponent<RectTransform>().rect.height)
        {
            ScrollToBottom(scrollRect);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponentInParent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
