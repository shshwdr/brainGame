using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : Singleton<LogController>
{
    public Transform content;
    ScrollRect scrollRect;
    public static void ScrollToTop(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
    public static void ScrollToBottom(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
    public void addLog(string str)
    {
        var logPrefab = Resources.Load<GameObject>("log");
        var go = Instantiate(logPrefab);
        go.GetComponent<LogPanel>().init(str);
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
