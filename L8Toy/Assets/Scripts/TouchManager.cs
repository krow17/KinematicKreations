using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TouchManager : MonoBehaviour {

	public bool touchIndicatorsFound = false;

	public GameObject touchIndicatorOne;
	public GameObject touchIndicatorTwo;

	public GameObject grabbedObjectOne;
	public GameObject grabbedObjectTwo;

	public Vector3 touchPosition;
	public Vector3 touch;
	public Vector3 offset;
	public Vector3 forceDirection;
	public Rect rayLimit;

	void Start()
	{
		
	}

	void Update(){

		if (SceneManager.GetActiveScene ().name == "Main")
		{
			if (GameManager.instance.playMode) {
				playWith ();
			} else {
				createWith ();
			}
		}

		if (SceneManager.GetActiveScene ().name == "Main" && !touchIndicatorsFound)
		{
			rayLimit = new Rect (Screen.width - Screen.width * (1 - Camera.main.transform.GetComponent<Camera> ().rect.min.x), 0, Screen.width * Camera.main.rect.width, Screen.height);
			touchIndicatorOne = GameObject.Find("Touch Indicator One");
			touchIndicatorTwo = GameObject.Find("Touch Indicator Two");
			touchIndicatorOne.SetActive (false);
			touchIndicatorTwo.SetActive (false);
			touchIndicatorsFound = true;
		} 
		else if (SceneManager.GetActiveScene ().name != "Main" && touchIndicatorsFound)
		{
			touchIndicatorsFound = false;
		}
			
	}

	// CREATE MODE
	void createWith()
	{
		RaycastHit hit;

		if (Input.touchCount > 0)
		{
			if (Input.touchCount == 2)
			{
				foreach (Touch t in Input.touches)
				{
					if (rayLimit.Contains (t.position))
					{

						Ray ray = Camera.main.ScreenPointToRay (t.position);
						touchPosition = t.position;

						switch (t.phase)
						{
						case TouchPhase.Began:

							if (t.fingerId == 0)
							{
								touchIndicatorOne.SetActive (true);
							} else if (t.fingerId == 1)
							{
								touchIndicatorTwo.SetActive (true);
							}

							if (Physics.Raycast (ray, out hit))
							{
								if (t.fingerId == 0)
								{
									if (hit.transform.tag == "shape")
									{
										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
										grabbedObjectOne = hit.transform.gameObject;
										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
										offset = grabbedObjectOne.transform.position - touch;
									} 
									else if (hit.collider.tag == "contact")
									{
										if (!GameManager.instance.destroyJoint && !GameManager.instance.groundJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
										} 
										else if (GameManager.instance.destroyJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.removeJoint (grabbedObjectOne.GetComponent<PartnerJoint> ().partnerJoint);
											GameManager.instance.mfuncs.DestroyJoint (false);

											GameObject.Find("Joint Destroy Button").GetComponent<Image> ().sprite = GameManager.instance.destroyJointsOffImage;
										} 
										else if (GameManager.instance.groundJoint)
										{
											Debug.Log ("here");
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.connectJointToGround (grabbedObjectOne);
											GameManager.instance.mfuncs.groundJoint (false);

											GameObject.Find("Joint Ground Button").GetComponent<Image> ().sprite = GameManager.instance.groundJointOffImage;
										}
									}
								} else if (t.fingerId == 1)
								{
									if (hit.transform.tag == "shape")
									{
										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
										grabbedObjectTwo = hit.transform.gameObject;
										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
										offset = grabbedObjectTwo.transform.position - touch;
									} else if (hit.collider.tag == "contact")
									{
										if (!GameManager.instance.destroyJoint && !GameManager.instance.groundJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectTwo = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
											offset = grabbedObjectTwo.transform.position - touch;
										}
										else if (GameManager.instance.destroyJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.removeJoint (grabbedObjectOne.GetComponent<PartnerJoint> ().partnerJoint);
											GameManager.instance.mfuncs.DestroyJoint (false);

											GameObject.Find("Joint Destroy Button").GetComponent<Image> ().sprite = GameManager.instance.destroyJointsOffImage;
										} 
										else if (GameManager.instance.groundJoint)
										{
											Debug.Log ("here");
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.connectJointToGround (grabbedObjectOne);
											GameManager.instance.mfuncs.groundJoint (false);

											GameObject.Find("Joint Ground Button").GetComponent<Image> ().sprite = GameManager.instance.groundJointOffImage;
										}
									}
								}
							}
							{
								GameManager.instance.mfuncs.unselectAllPieces ();
							}
							break;

						case TouchPhase.Moved:
							if (t.fingerId == 0)
							{
								moveIndicator (touchIndicatorOne, t);
								if (grabbedObjectOne != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
									Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
									grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							} else if (t.fingerId == 1)
							{
								moveIndicator (touchIndicatorTwo, t);
								if (grabbedObjectTwo != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
									Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
									grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							}
							break;

						case TouchPhase.Stationary:
							if (t.fingerId == 0)
							{
								moveIndicator (touchIndicatorOne, t);
								if (grabbedObjectOne != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
									Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
									grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							} else if (t.fingerId == 1)
							{
								moveIndicator (touchIndicatorTwo, t);
								if (grabbedObjectTwo != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
									Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
									grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							}
							break;

						case TouchPhase.Ended:
							if (t.fingerId == 0)
							{
								touchIndicatorOne.SetActive (false);
								grabbedObjectOne = null;
							} else if (t.fingerId == 1)
							{
								touchIndicatorTwo.SetActive (false);
								grabbedObjectTwo = null;
							}
							break;
						}
					} 
					else
					{
						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (false);
							grabbedObjectOne = null;
						} else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (false);
							grabbedObjectTwo = null;
						}
					}
				}
			} 
			else if (Input.touchCount == 1)
			{
				foreach (Touch t in Input.touches)
				{
					if (rayLimit.Contains (t.position))
					{

						Ray ray = Camera.main.ScreenPointToRay (t.position);
						touchPosition = t.position;

						switch (t.phase)
						{
						case TouchPhase.Began:

							if (t.fingerId == 0)
							{
								touchIndicatorOne.SetActive (true);
							} else if (t.fingerId == 1)
							{
								touchIndicatorTwo.SetActive (true);
							}

							if (Physics.Raycast (ray, out hit))
							{
								if (t.fingerId == 0)
								{
									if (hit.transform.tag == "shape")
									{
										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
										grabbedObjectOne = hit.transform.gameObject;
										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
										offset = grabbedObjectOne.transform.position - touch;
									} else if (hit.collider.tag == "contact")
									{
										if (!GameManager.instance.destroyJoint && !GameManager.instance.groundJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
										}
										else if (GameManager.instance.destroyJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.removeJoint (grabbedObjectOne.GetComponent<PartnerJoint> ().partnerJoint);
											GameManager.instance.mfuncs.DestroyJoint (false);

											GameObject.Find("Joint Destroy Button").GetComponent<Image> ().sprite = GameManager.instance.destroyJointsOffImage;
										} 
										else if (GameManager.instance.groundJoint)
										{
											Debug.Log ("here");
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.connectJointToGround (grabbedObjectOne);
											GameManager.instance.mfuncs.groundJoint (false);

											GameObject.Find("Joint Ground Button").GetComponent<Image> ().sprite = GameManager.instance.groundJointOffImage;
										}
									}
								} else if (t.fingerId == 1)
								{

									if (hit.transform.tag == "shape")
									{
										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
										grabbedObjectTwo = hit.transform.gameObject;
										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
										offset = grabbedObjectTwo.transform.position - touch;
									} else if (hit.collider.tag == "contact")
									{
										if (!GameManager.instance.destroyJoint && !GameManager.instance.groundJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectTwo = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
											offset = grabbedObjectTwo.transform.position - touch;
										}
										else if (GameManager.instance.destroyJoint)
										{
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.removeJoint (grabbedObjectOne.GetComponent<PartnerJoint> ().partnerJoint);
											GameManager.instance.mfuncs.DestroyJoint (false);

											GameObject.Find("Joint Destroy Button").GetComponent<Image> ().sprite = GameManager.instance.destroyJointsOffImage;
										} 
										else if (GameManager.instance.groundJoint)
										{
											Debug.Log ("here");
											GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
											grabbedObjectOne = hit.transform.gameObject;
											touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
											offset = grabbedObjectOne.transform.position - touch;
											GameManager.instance.mfuncs.connectJointToGround (grabbedObjectOne);
											GameManager.instance.mfuncs.groundJoint (false);

											GameObject.Find("Joint Ground Button").GetComponent<Image> ().sprite = GameManager.instance.groundJointOffImage;
										}
									}
								}
							} else
							{
								GameManager.instance.mfuncs.unselectAllPieces ();
							}
							break;

						case TouchPhase.Moved:
							if (t.fingerId == 0)
							{

								moveIndicator (touchIndicatorOne, t);

								if (grabbedObjectOne != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
									Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
									grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							} else if (t.fingerId == 1)
							{

								moveIndicator (touchIndicatorTwo, t);
								if (grabbedObjectTwo != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
									Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
									grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							}
							break;

						case TouchPhase.Stationary:
							if (t.fingerId == 0)
							{
								moveIndicator (touchIndicatorOne, t);
								if (grabbedObjectOne != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
									Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
									grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							} else if (t.fingerId == 1)
							{
								moveIndicator (touchIndicatorTwo, t);
								if (grabbedObjectTwo != null)
								{
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
									Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
									grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
								}
								break;
							}
							break;

						case TouchPhase.Ended:
							if (t.fingerId == 0)
							{
								touchIndicatorOne.SetActive (false);
								grabbedObjectOne = null;
							} else if (t.fingerId == 1)
							{
								touchIndicatorTwo.SetActive (false);
								grabbedObjectTwo = null;
							}
							break;
						}
					}
					else
					{
						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (false);
							grabbedObjectOne = null;
						} else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (false);
							grabbedObjectTwo = null;
						}
					}
				}
			}

		}
		else
		{
			foreach (GameObject L in GameManager.instance.config.configuration)
			{
				L.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
				L.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
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
					touchPosition = t.position;

					switch (t.phase) {
					case TouchPhase.Began:

						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (true);
						}
						else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (true);
						}

						if (Physics.Raycast (ray, out hit)) {
							if (t.fingerId == 0)
							{
								if (hit.transform.tag == "shape")
								{
									grabbedObjectOne = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									offset = grabbedObjectOne.transform.position - touch;
								} 
								else if (hit.collider.tag == "contact")
								{
									grabbedObjectOne = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									offset = grabbedObjectOne.transform.position - touch;
								}
							}
							else if (t.fingerId == 1)
							{
								if (hit.transform.tag == "shape")
								{
									grabbedObjectTwo = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									offset = grabbedObjectTwo.transform.position - touch;
								} 
								else if (hit.collider.tag == "contact")
								{
									grabbedObjectTwo = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									offset = grabbedObjectTwo.transform.position - touch;
								}
							}
						}
						break;

					case TouchPhase.Moved:
						if (t.fingerId == 0)
						{
							moveIndicator (touchIndicatorOne, t);
							if (grabbedObjectOne != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						} else if (t.fingerId == 1)
						{
							moveIndicator (touchIndicatorTwo, t);
							if (grabbedObjectTwo != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
								Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
								grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						}
						break;

					case TouchPhase.Stationary:
						if (t.fingerId == 0)
						{
							moveIndicator (touchIndicatorOne, t);
							if (grabbedObjectOne != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						} else if (t.fingerId == 1)
						{
							moveIndicator (touchIndicatorTwo, t);
							if (grabbedObjectTwo != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
								Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
								grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						}
						break;

					case TouchPhase.Ended:
						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (false);
							grabbedObjectOne = null;
						} 
						else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (false);
							grabbedObjectTwo = null;
						}
						break;
					}
				}
			}
			else if (Input.touchCount == 1)
			{
				foreach (Touch t in Input.touches) {

					Ray ray = Camera.main.ScreenPointToRay (t.position);
					touchPosition = t.position;

					switch (t.phase) {
					case TouchPhase.Began:

						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (true);
						}
						else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (true);
						}

						if (Physics.Raycast (ray, out hit)) {
							if (t.fingerId == 0)
							{
								if (hit.transform.tag == "shape")
								{
									grabbedObjectOne = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									offset = grabbedObjectOne.transform.position - touch;
								} 
								else if (hit.collider.tag == "contact")
								{
									grabbedObjectOne = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
									offset = grabbedObjectOne.transform.position - touch;
								}
							}
							else if (t.fingerId == 1)
							{

								if (hit.transform.tag == "shape")
								{
									grabbedObjectTwo = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									offset = grabbedObjectTwo.transform.position - touch;
								} 
								else if (hit.collider.tag == "contact")
								{
									grabbedObjectTwo = hit.transform.gameObject;
									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
									offset = grabbedObjectTwo.transform.position - touch;
								}
							}
						}
						break;

					case TouchPhase.Moved:
						if (t.fingerId == 0)
						{

							moveIndicator (touchIndicatorOne, t);

							if (grabbedObjectOne != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						} else if (t.fingerId == 1)
						{

							moveIndicator (touchIndicatorTwo, t);
							if (grabbedObjectTwo != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
								Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
								grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						}
						break;

					case TouchPhase.Stationary:
						if (t.fingerId == 0)
						{
							moveIndicator (touchIndicatorOne, t);
							if (grabbedObjectOne != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						} else if (t.fingerId == 1)
						{
							moveIndicator (touchIndicatorTwo, t);
							if (grabbedObjectTwo != null)
							{
								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectTwo.transform.position.y));
								forceDirection = (touch + offset) - grabbedObjectTwo.transform.position;
								Debug.DrawLine (grabbedObjectTwo.transform.position, forceDirection, Color.green);
								grabbedObjectTwo.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
							}
							break;
						}
						break;

					case TouchPhase.Ended:
						if (t.fingerId == 0)
						{
							touchIndicatorOne.SetActive (false);
							grabbedObjectOne = null;
						} 
						else if (t.fingerId == 1)
						{
							touchIndicatorTwo.SetActive (false);
							grabbedObjectTwo = null;
						}
						break;
					}
				}
			}
			else
			{
				foreach (GameObject L in GameManager.instance.config.configuration)
				{
					L.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
					L.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
				}
			}
		}
		else
		{
			foreach (GameObject L in GameManager.instance.config.configuration)
			{
				L.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
				L.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
			}
		}
	}

	void moveIndicator(GameObject indicator, Touch touch)
	{
		indicator.transform.localPosition = new Vector3 (touch.position.x / GameObject.Find("Canvas").GetComponent<Canvas>().scaleFactor, touch.position.y / GameObject.Find("Canvas").GetComponent<Canvas>().scaleFactor, 0);
	}
}


//OLD CODE FOR REFRENCE

//				foreach (Touch t in Input.touches)
//				{ 
//					if (rayLimit.Contains (t.position))
//					{
//						Ray ray = Camera.main.ScreenPointToRay (t.position);
//						touchPosition = t.position;
//
//						switch (t.phase)
//						{
//						//SELECTION
//						case TouchPhase.Began:
//							if (Physics.Raycast (ray, out hit))
//							{
//								if (hit.transform.tag == "shape")
//								{
//									GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject);
//									grabbedObjectOne = GameManager.instance.config.currentSelection;
//									touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
//									offset = grabbedObjectOne.transform.position - touch;
//								} 
//								else if (hit.collider.tag == "contact")
//								{
//									if (!GameManager.instance.destroyJoint)
//									{
//										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
//										grabbedObjectOne = hit.transform.gameObject;
//										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
//										offset = grabbedObjectOne.transform.position - touch;
//									}
//									else
//									{
//										GameManager.instance.mfuncs.selectPiece (hit.transform.gameObject.GetComponent<Magnet> ().LShape);
//										grabbedObjectOne = hit.transform.gameObject;
//										touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
//										offset = grabbedObjectOne.transform.position - touch;
//										GameManager.instance.mfuncs.removeJoint(grabbedObjectOne.GetComponent<PartnerJoint>().partnerJoint);
//										GameManager.instance.mfuncs.DestroyJoint (false);
//									}
//								}
//							} 
//							else
//							{
//								GameManager.instance.mfuncs.unselectAllPieces ();
//							}
//							break;
//
//						//MOVE L PIECE
//						case TouchPhase.Moved:
//							if (GameManager.instance.config.currentSelection != null)
//							{
//								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
//								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
//								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
//								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
//							}
//							break;
//
//						case TouchPhase.Stationary:
//							if (GameManager.instance.config.currentSelection != null)
//							{
//								touch = Camera.main.ScreenToWorldPoint (new Vector3 (touchPosition.x, touchPosition.y, Camera.main.transform.position.y - grabbedObjectOne.transform.position.y));
//								forceDirection = (touch + offset) - grabbedObjectOne.transform.position;
//								Debug.DrawLine (grabbedObjectOne.transform.position, forceDirection, Color.red);
//								grabbedObjectOne.GetComponent<Rigidbody> ().AddForce (forceDirection * GameManager.instance.parameters.forceMultiplier);
//							}
//							break;
//
//						case TouchPhase.Ended:
//							foreach (GameObject L in GameManager.instance.config.configuration)
//							{
//								L.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
//								L.GetComponent<Rigidbody> ().angularVelocity = new Vector3 (0, 0, 0);
//							}
//							break;
//						}
//					} else
//					{
//						
//					}
//
//				}
//			}