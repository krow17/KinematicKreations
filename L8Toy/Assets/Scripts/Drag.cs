﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    public Vector3 previousPosition;
    public Vector3 forceDirection;
	public bool touched = false;
	void Start () {

	}

	void Update(){
		if (GameManager.instance.playMode) {
			playWith ();
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
			if (Input.touchCount == 1) {
				foreach (Touch t in Input.touches) {
					Ray ray = Camera.main.ScreenPointToRay (t.position);

					switch (t.phase) {
					case TouchPhase.Began:
						if (Physics.Raycast (ray, out hit)) {
							if (hit.collider == transform.GetComponent<Collider> ()) {
								if (hit.transform.tag == "shape") {
									GameManager.instance.mfuncs.selectPiece (this.gameObject);
								} else if (hit.collider.tag == "contact") {
									GameManager.instance.mfuncs.selectPiece (this.gameObject.GetComponent<Magnet> ().LShape);
								} 
								touched = true;
								previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
								Debug.Log ("You hit the " + hit.transform.name+ " in Create Mode");
							} else {
								GameManager.instance.mfuncs.unselectAllPieces ();
							}
						}
						break;

					case TouchPhase.Moved:
						if (touched && tag == "shape") {
							forceDirection = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
							Debug.DrawLine (transform.position, forceDirection, Color.red);
							foreach (GameObject mag in gameObject.GetComponent<LShape>().allMagnets) {
								//mag.transform.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
						}
						break;

					case TouchPhase.Stationary:
						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
						break;

					case TouchPhase.Ended:
						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
						GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
						touched = false;
						break;

						break;
					}
				}
			}
		}
	}

    void playWith()
    {
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
			if (Input.touchCount  == 2) {

				foreach (Touch t in Input.touches) {
					Ray ray = Camera.main.ScreenPointToRay (t.position);

					switch (t.phase) {
					case TouchPhase.Began:
						if (Physics.Raycast (ray, out hit)) {
							if (hit.collider == transform.GetComponent<Collider> ()) {
								touched = true;
								previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
								Debug.Log ("You hit the2 " + hit.transform.name);
							}
						}
						break;

					case TouchPhase.Stationary:
						GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
						break;
					case TouchPhase.Moved:
						if (touched) {
							forceDirection = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
							Debug.DrawLine (transform.position, forceDirection, Color.red);
							transform.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
						}
						break;

					case TouchPhase.Ended:
						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		                //GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
						touched = false;
						break;
					}
				}
			}
			else if (Input.touchCount == 1)
			{
				foreach (Touch t in Input.touches) {
					Ray ray = Camera.main.ScreenPointToRay (t.position);

					switch (t.phase) {
					case TouchPhase.Began:
						if (Physics.Raycast (ray, out hit)) {
							if (hit.collider == transform.GetComponent<Collider> ()) {
								touched = true;
								previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
								Debug.Log ("You hit the " + hit.transform.name + " in Play Mode");
							}
						}
						break;

					case TouchPhase.Stationary:
						GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
						GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
						break;
					case TouchPhase.Moved:
						if (touched) {
							//forceDirection = new Vector3(t.position.x ,0 , t.position.y) - previousPosition;
							forceDirection = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
							Debug.DrawLine (transform.position, forceDirection, Color.red);
							Debug.Log ("here");
							foreach (GameObject L in GameManager.instance.config.configuration)
							{
								L.transform.GetComponent<Rigidbody>().AddForce(forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							previousPosition = new Vector3 (t.deltaPosition.x, 0, t.deltaPosition.y);
						}
						break;

					case TouchPhase.Ended:
						foreach (GameObject L in GameManager.instance.config.configuration)
						{
							GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
							GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
						}
						touched = false;
						break;
					}
				}
			}
			else
			{
				GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
				GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
			}
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }
    }

	void clampVelocity()
	{
		if (transform.GetComponent<Rigidbody> ().velocity.magnitude > GameManager.instance.parameters.velocityClamp) {
			Vector3 currentVelocity = transform.GetComponent<Rigidbody> ().velocity;
			Vector3 clampedVelocity = Vector3.ClampMagnitude (currentVelocity, GameManager.instance.parameters.velocityClamp);
			transform.GetComponent<Rigidbody> ().velocity.Set (clampedVelocity.x, clampedVelocity.y, clampedVelocity.z);
		}
	}

}
