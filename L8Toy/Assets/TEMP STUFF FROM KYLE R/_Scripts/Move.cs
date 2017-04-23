using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Color color;

    private Vector3 screenPoint;
    private Vector3 offset;

    private float constantHeight;

    public Vector3 moveVector;

    public bool toMove = false;

    void OnTriggerEnter(Collider other)
    {

        if (this.tag != "shape" && other.tag == "contact")
        {
            //print("Contact was made");
            //this.gameObject.GetComponent<FixedJoint>().connectedBody = transform.parent.gameObject.GetComponent<Rigidbody>();
            this.gameObject.GetComponent<HingeJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
        }
    }
   
}
