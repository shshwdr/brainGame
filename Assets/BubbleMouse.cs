using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMouse : MonoBehaviour
{
    SpriteRenderer rend;
    Vector3 prevFingerPosition = Vector3.zero;
    CircleCollider2D collider;
    Rigidbody2D rb;

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

    void OnMouseDown()
    {
        prevFingerPosition = GetMouseOnPositionInWorldSpace();
        //collider.enabled = false;
        rb.Sleep();
    }

    void OnMouseDrag()
    {
        Vector3 fingerPosition = GetMouseOnPositionInWorldSpace();
        transform.position = (fingerPosition);
        prevFingerPosition = fingerPosition;
    }
    private Vector3 GetMouseOnPositionInWorldSpace()
    {
        Plane dragPlane = new Plane(Camera.main.transform.forward, transform.position);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter = 0.0f;
        if (dragPlane.Raycast(camRay, out enter))
        {
            return camRay.GetPoint(enter);
        }

        return prevFingerPosition;
    }
}
