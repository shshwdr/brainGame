using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDayManager : Singleton<DefaultDayManager>
{
    public List<List<string>> dayInfoList;
    public List<string> dayTime;
    // Start is called before the first frame update
    void Awake()
    {

        dayInfoList = CsvUtil.LoadList("DayPlan",true);
        dayTime = dayInfoList[0];
        dayInfoList.RemoveAt(0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
