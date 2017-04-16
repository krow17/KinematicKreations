using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour {

    Vector2 previousPosition;
    Vector3 forceDirection;
	public bool touched = false;
	void Start () {

	}


	public void FixedUpdate(){
		//MouseTouch ();
		ObjectTouch ();

	}


    //void MouseTouch()
    //{
    //	if (Input.GetMouseButtonDown (0)) {
    //		gameObjectSreenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);

    //		RaycastHit hit;
    //		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

    //		if (Physics.Raycast (ray, out hit)) {

    //			//Touch firstTouch = Input.GetTouch (i);
    //			//int fingerID1 = firstTouch.fingerId;

    //			//Vector2 vec = (firstTouch.deltaPosition) / firstTouch.deltaTime;

    //			//force = new Vector3 (vec.x, vec.y, gameObjectSreenPoint.z);

    //			Debug.Log ("You hit the " + hit.transform.name);
    //			//hit.rigidbody.AddForce (force * 10);
    //		}
    //	}
    //}


    void ObjectTouch()
    {
        RaycastHit hit;

        if (Input.touchCount > 0)
        {
            Debug.Log("Touch count = " + Input.touchCount);

            foreach (Touch t in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(t.position);

                switch (t.phase)
                {
                    case TouchPhase.Began:
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider == this.GetComponent<Collider>())
                            {
                                touched = true;
                                previousPosition = t.position;
                                Debug.Log("You hit the " + hit.transform.name);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        forceDirection = previousPosition - t.position;
                        transform.GetComponent<Rigidbody>().AddForce(forceDirection);
                        previousPosition = t.position;
                        break;

                    case TouchPhase.Ended:
                        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
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

}


















/*using System.Collections;
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
} */
