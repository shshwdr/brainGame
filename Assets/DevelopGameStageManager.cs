using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageInfo : BaseInfo
{
    public int levelupPoint;
    public Dictionary<string, float> ideas;
}

public class DevelopGameStageManager : Singleton<DevelopGameStageManager>
{
    public Dictionary<string, GameStageInfo> gameStageInfo = new Dictionary<string, GameStageInfo>();
    public List<GameStageInfo> gameStageInfoList;
    public int currentStageId = 0;
    public float currentScore = 0;
    bool isMaxLevel;
    // Start is called before the first frame update
    void Awake()
    {

        //gameStageInfoList = CsvUtil.LoadObjects<GameStageInfo>("GameStage");
        //foreach (var info in gameStageInfoList)
        //{
        //    gameStageInfo[info.name] = info;
        //}
    }

    private void Start()
    {
    }
    public GameStageInfo currentStageInfo()
    {
        return gameStageInfoList[currentStageId];
    }

    public void updateStage()
    {
        
        currentStageId++;
        if(currentStageId >= gameStageInfoList.Count)
        {
            isMaxLevel = true;
        }
    }

    public void addProcess(float addAmount)
    {
        currentScore += addAmount;
        if (!isMaxLevel && currentScore >= currentStageInfo().levelupPoint)
        {
            updateStage();
        }
        EventPool.Trigger("gameStageUpdate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
