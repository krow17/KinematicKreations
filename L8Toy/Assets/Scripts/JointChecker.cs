using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointChecker : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.parent != null && other.gameObject.transform.parent.tag == "contact")
		{
			if (other.gameObject.transform.parent.GetComponent<Magnet>().LShape != gameObject.transform.parent.GetComponent<Magnet> ().LShape)
			{
				GameManager.instance.mfuncs.addJoint(gameObject.transform.parent.gameObject, other.gameObject.transform.parent.gameObject);
			}	
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
