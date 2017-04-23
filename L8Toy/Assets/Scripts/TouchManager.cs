using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {


	public Vector3 touchPosition;
	public Vector3 offset;
	public Vector3 forceDirection;

	void Update(){
		if (GameManager.instance.playMode) {
			//playWith ();
			clampVelocity ();
		} else {
			createWith ();
		}
	}

	// CREATE MODE
	void createWith()
	{
		RaycastHit hit;

		if (Input.touchCount > 0) {
			//ONLY ALLOW FOR SINGLE INPUT
			if (Input.touchCount == 1) 
			{
				foreach (Touch t in Input.touches) 
				{
					Ray ray = Camera.main.ScreenPointToRay (t.position);
					touchPosition = new Vector3(t.position.x/Screen.width, t.position.y/Screen.height, 0);

					switch (t.phase) 
					{
					//SELECTION
					case TouchPhase.Began:
						if (Physics.Raycast (ray, out hit))
						{
							Debug.Log ("initial touch");
							if (hit.transform.tag == "shape")
							{
								GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
								GameManager.instance.config.currentSelection.GetComponent<LShape>().touched = true;
								offset = GameManager.instance.config.currentSelection.transform.position - Camera.main.ScreenToWorldPoint(touchPosition);
							} else if (hit.collider.tag == "contact")
							{
								GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
								GameManager.instance.config.currentSelection.GetComponent<LShape>().touched = true;
							}
						} 
						else
						{
							GameManager.instance.mfuncs.unselectAllPieces ();
						}
						break;

					//MOVE L PIECE
					case TouchPhase.Moved:
						if (GameManager.instance.config.currentSelection != null)
						{
							if (GameManager.instance.config.currentSelection.GetComponent<LShape>().touched && GameManager.instance.config.currentSelection.tag == "shape") 
							{
								forceDirection = (touchPosition + offset) - GameManager.instance.config.currentSelection.transform.position;
								Debug.DrawLine (GameManager.instance.config.currentSelection.transform.position, forceDirection, Color.red);
								GameManager.instance.config.currentSelection.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
						}
						break;

					case TouchPhase.Stationary:
						if (GameManager.instance.config.currentSelection != null)
						{
							if (GameManager.instance.config.currentSelection.GetComponent<LShape>().touched && GameManager.instance.config.currentSelection.tag == "shape")
							{
								GameManager.instance.config.currentSelection.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
							}
						}
						break;

					case TouchPhase.Ended:
						foreach (GameObject L in GameManager.instance.config.configuration)
						{
							L.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
							L.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
							L.GetComponent<LShape> ().touched = false;
						}
						break;

						break;
					}
				}
			}
		}
	}

//	void playWith()
//	{
//		RaycastHit hit;
//
//		if (Input.touchCount > 0)
//		{
//			if (Input.touchCount  == 2) {
//
//				foreach (Touch t in Input.touches) {
//					Ray ray = Camera.main.ScreenPointToRay (t.position);
//
//					switch (t.phase) {
//					case TouchPhase.Began:
//						if (Physics.Raycast (ray, out hit)) {
//							if (hit.collider == transform.GetComponent<Collider> ()) {
//								touched = true;
//								previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//								Debug.Log ("You hit the2 " + hit.transform.name);
//							}
//						}
//						break;
//
//					case TouchPhase.Stationary:
//						GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
//						break;
//					case TouchPhase.Moved:
//						if (touched) {
//							forceDirection = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//							Debug.DrawLine (transform.position, forceDirection, Color.red);
//							transform.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
//							previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//						}
//						break;
//
//					case TouchPhase.Ended:
//						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
//						//GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//						touched = false;
//						break;
//					}
//				}
//			}
//			else if (Input.touchCount == 1)
//			{
//				foreach (Touch t in Input.touches) {
//					Ray ray = Camera.main.ScreenPointToRay (t.position);
//
//					switch (t.phase) {
//					case TouchPhase.Began:
//						if (Physics.Raycast (ray, out hit)) {
//							if (hit.collider == transform.GetComponent<Collider> ()) {
//								touched = true;
//								previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//								Debug.Log ("You hit the " + hit.transform.name + " in Play Mode");
//							}
//						}
//						break;
//
//					case TouchPhase.Stationary:
//						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
//						GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
//						break;
//					case TouchPhase.Moved:
//						if (touched) {
//							//forceDirection = new Vector3(t.position.x ,0 , t.position.y) - previousPosition;
//							forceDirection = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//							Debug.DrawLine (transform.position, forceDirection, Color.red);
//							Debug.Log ("here");
//							foreach (GameObject L in GameManager.instance.config.configuration)
//							{
//								L.transform.GetComponent<Rigidbody>().AddForce(forceDirection * GameManager.instance.parameters.forceMultiplier);
//							}
//							previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
//						}
//						break;
//
//					case TouchPhase.Ended:
//						foreach (GameObject L in GameManager.instance.config.configuration)
//						{
//							GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
//							GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//						}
//						touched = false;
//						break;
//					}
//				}
//			}
//			else
//			{
//				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//				GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//			}
//		}
//		else
//		{
//			GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
//			GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
//		}
//	}

	void clampVelocity()
	{
		if (transform.GetComponent<Rigidbody> ().velocity.magnitude > GameManager.instance.parameters.velocityClamp) {
			Vector3 currentVelocity = transform.GetComponent<Rigidbody> ().velocity;
			Vector3 clampedVelocity = Vector3.ClampMagnitude (currentVelocity, GameManager.instance.parameters.velocityClamp);
			transform.GetComponent<Rigidbody> ().velocity.Set (clampedVelocity.x, clampedVelocity.y, clampedVelocity.z);
		}
	}
}
