using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFunctions : MonoBehaviour {


	public GameObject LPiece;

	//Use this to add all the main functions we will call.


	// Use this for initialization
	void Start () {
		
	}

	public void addPiece()
	{
		Debug.Log ("clicked");
		if (GameManager.instance.config.numberOfPieces < 8)
		{
			GameObject tempL = Instantiate (LPiece, Vector3.zero, Quaternion.identity);
			GameManager.instance.config.addPiece(tempL);
		} 
		else 
		{
			Debug.Log ("Limit of pices reached");
		}

		//instantiate piece and add it to the correct files
	}

	public void removePiece()
	{
		//delete piece and delete from correct files
	}

	public void selectPiece()
	{
		//select piece
	}

	public void addJoint()
	{
		//create joint between pieces
	}

	public void zoom(bool zoomIn)
	{
		if (zoomIn && Camera.main.transform.position.y > 5) 
		{
			Camera.main.transform.Translate (Vector3.forward * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
		}
		else if (!zoomIn && Camera.main.transform.position.y < 28) 
		{
			Camera.main.transform.Translate (Vector3.back * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
		}
	}



	// Update is called once per frame
	void Update () {
		
	}
}
