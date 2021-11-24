using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonController : Singleton<PauseButtonController>
{
    public bool isPaused = false;
    public Sprite pauseIamge;
    public Sprite resumeImage;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void togglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = resumeImage;
            Time.timeScale = 0;
        }
        else
        {
            image.sprite = pauseIamge;
            Time.timeScale = 1;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
