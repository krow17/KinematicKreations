using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

	public Vector3 gameObjectSreenPoint;
	public Vector3 mousePreviousLocation;
	public Vector3 mouseCurLocation;

	void OnMouseDown()
	{
		//This grabs the position of the object in the world and turns it into the position on the screen
		gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		//Sets the mouse pointers vector3
		mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
	}

	public Vector3 force;
	public Vector3 objectCurrentPosition;
	public Vector3 objectTargetPosition;
	public float topSpeed;

	void OnMouseDrag()
	{
		mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
		force = new Vector3(mouseCurLocation.x - mousePreviousLocation.x, mouseCurLocation.z - mousePreviousLocation.z, mouseCurLocation.y - mousePreviousLocation.y);
		mousePreviousLocation = mouseCurLocation;
		GetComponent<Rigidbody>().AddForce (force * GameManager.instance.parameters.forceMultiplier);
	}

	public void OnMouseUp()
	{

		//Makes sure there isn't a ludicrous speed
		//if (GetComponent<Rigidbody>().velocity.magnitude > topSpeed)
		//	force = GetComponent<Rigidbody>().velocity.normalized * topSpeed;

	}

	public void FixedUpdate()
	{
		if (!Input.GetMouseButton (0)) {
			GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		}

	}// Use this for initialization
	void Start () {

		topSpeed = GameManager.instance.parameters.maxDragSpeed;

	}

	// Update is called once per frame
	void Update () {

	}
}
