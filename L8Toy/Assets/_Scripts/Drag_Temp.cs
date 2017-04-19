using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_Temp : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public Vector3 moveVector;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        //print("Current Position: " + curPosition);
        //print("Current Screen Point: " + curScreenPoint);
        //transform.position = curPosition;

        moveVector = curPosition - transform.position;
        GetComponent<Rigidbody>().AddForceAtPosition(moveVector * 360.0f, transform.position - offset);
    }

   
}
