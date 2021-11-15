using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bubble : MonoBehaviour
{
    public SpriteRenderer rend;
    Vector3 prevFingerPosition = Vector3.zero;
    CircleCollider2D collider;
    Rigidbody2D rb;

    public TMP_Text nameLable;

    public bool isActionBubble;

    public virtual void init(BubbleInfo info)
    {
        nameLable.text = info.displayName;
    }
    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private Vector3 GetMouseOnPositionInWorldSpace()
    //{
    //    Plane dragPlane = new Plane(Camera.main.transform.forward, transform.position);
    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    float enter = 0.0f;
    //    if (dragPlane.Raycast(camRay, out enter))
    //    {
    //        return camRay.GetPoint(enter);
    //    }

    //    return prevFingerPosition;
    //}

    void OnMouseDown()
    {
        //prevFingerPosition = GetMouseOnPositionInWorldSpace();
        //collider.enabled = false;
        //rb.Sleep();
    }

    //void OnMouseDrag()
    //{
    //    Vector3 fingerPosition = GetMouseOnPositionInWorldSpace();
    //    transform.position = (fingerPosition );
    //    prevFingerPosition = fingerPosition;
    //}

    //public void attachToSlot(ActionSlot slot)
    //{

    //    collider.enabled = false;
    //    rb.Sleep();
    //    transform.parent = slot.transform;

    //}
    //private void OnMouseUp()
    //{
    //    foreach(var collider in Physics2D.OverlapCircleAll(transform.position, collider.radius))
    //    {
    //        var slot = collider.GetComponent<ActionSlot>();
    //        if (slot && slot.canTakeBubble(this))
    //        {
    //            slot.takeBubble(this);
    //            return;
    //        }
    //    }

    //    collider.enabled = true;
    //    rb.WakeUp();
    //}
}
