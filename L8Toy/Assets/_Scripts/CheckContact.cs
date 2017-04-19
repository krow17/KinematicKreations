using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        print("Contact made");
        this.gameObject.AddComponent<HingeJoint>();
        this.gameObject.GetComponent<HingeJoint>().axis = new Vector3(0, 10, 0);
        this.gameObject.GetComponent<HingeJoint>().useMotor = true;
        //this.gameObject.GetComponent<HingeJoint>().;
        other.gameObject.AddComponent<HingeJoint>();
        other.gameObject.GetComponent<HingeJoint>().axis = new Vector3(0, 10, 0);
    }
}
