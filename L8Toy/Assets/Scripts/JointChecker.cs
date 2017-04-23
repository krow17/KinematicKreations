using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointChecker : MonoBehaviour {

	public GameObject partnerJoint;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "contact")
		{
			if (other.gameObject.GetComponent<Magnet> ().LShape != gameObject.GetComponent<Magnet> ().LShape)
			{
				GameManager.instance.mfuncs.addJoint (gameObject, other.gameObject);
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
