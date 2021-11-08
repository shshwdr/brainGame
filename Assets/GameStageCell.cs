using Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStageCell : MonoBehaviour
{

    public Image progress;
    public TMP_Text nameLabel;
    // Start is called before the first frame update
    void Start()
    {
        onStageUpdate();
        EventPool.OptIn("gameStageUpdate",onStageUpdate);
    }

    void onStageUpdate()
    {
        var currentStage = DevelopGameStageManager.Instance.currentStageInfo();
        int currentScore =(int) DevelopGameStageManager.Instance.currentScore;
        int levelUpNeedScore = (int)currentStage.levelupPoint;
        nameLabel.text = currentStage.displayName+" "+ currentScore.ToString()+"/"+ levelUpNeedScore.ToString();
        progress.fillAmount = currentScore / levelUpNeedScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
