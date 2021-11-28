using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverInfo
{
    public List<string> attributeRequirements;
    public string log;
    public string category;
}
    public class GameOverManager : MonoBehaviour
{
    public bool isGameOver = false;
    public List<GameOverInfo> gameoverInfos;
    public RectTransform logTransform;
    public GameObject restartButton;
    // Start is called before the first frame update
    void Awake()
    {
        gameoverInfos = CsvUtil.LoadObjects<GameOverInfo>("GameOver");
        EventPool.OptIn("checkGameOver", checkGameOver);
    }

    void extendLog()
    {
        logTransform.anchorMin = Vector2.zero;
        logTransform.anchorMax = Vector2.one;
        logTransform.sizeDelta = Vector2.zero;
        logTransform.anchoredPosition = Vector2.zero;
    }

    void checkGameOver()
    {
        if (!isGameOver)
        {
            if (!AttributeManager.Instance.canSurvive() || EventPlanManager.Instance.isLastDay())
            {
                gameover();
            }
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void gameover()
    {
        isGameOver = true;
        string logString = "";
        HashSet<string> categorySet = new HashSet<string>();
        foreach(var info in gameoverInfos)
        {
            bool isValid = true;
            if(info.category!=null&& info.category.Length >0 && categorySet.Contains(info.category))
            {
                continue;
            }
            foreach (var req in info.attributeRequirements)
            {
                var splits = req.Split(new char[] { '<', '>' });
                if (splits.Length != 2)
                {
                    Debug.LogError("game over requirement can't be split " + req);
                }
                int value;
                bool canParse = int.TryParse(splits[1],out value);
                if (!canParse)
                {
                    Debug.LogError("game over requirement can't be parse value " + req);
                }
                if (req.Contains("<"))
                {
                    if(AttributeManager.Instance.getAttributeValue( splits[0])> value)
                    {
                        isValid = false;
                        break;
                    }
                }else if (req.Contains(">"))
                {
                    if (AttributeManager.Instance.getAttributeValue(splits[0]) < value)
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            if (isValid)
            {
                logString += info.log;
                if(info.category == "survive")
                {
                    break;
                }

                if (info.category != null && info.category.Length > 0)
                {
                    categorySet.Add(info.category);
                }
            }
        }

        LogController.Instance.addLog(logString);
        extendLog();
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.2f);
        restartButton.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
