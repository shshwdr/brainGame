using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDayIdeaGenerator : MonoBehaviour
{
    public Transform dayContent;
    public float moveSpeed = 1;
    public Transform startTransform;
    Vector3 startPosition;
    public float slotInterval = 2;
    
    private void Awake()
    {
        startPosition = startTransform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        generateADay();
    }

    void generateSlot(string idea,string time)
    {
        GameObject slotPrefab = Resources.Load<GameObject>("prefab/ideaSlot");
        var slot= Instantiate(slotPrefab, startPosition,Quaternion.identity, dayContent);
        slot.GetComponent<ActionSlot>().init(idea, time, startPosition);
        startPosition += Vector3.right * slotInterval;
    }

    void generateADay()
    {
        var aday = DefaultDayManager.Instance.dayInfoList[0];
        for(int i = 0;i<aday.Count;i++)
        {
            generateSlot(aday[i], DefaultDayManager.Instance.dayTime[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        dayContent.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
