using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public void init(CollectionBubbleInfo info)
    {
        GetComponent<SpriteRenderer>().sprite = info.icon;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.DOPunchScale(Vector3.one, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
