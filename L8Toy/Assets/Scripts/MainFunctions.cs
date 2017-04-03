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
		if (GameManager.instance.config.numberOfPieces < 8) {
			Instantiate (LPiece, Vector3.zero, Quaternion.identity);
			GameManager.instance.config.numberOfPieces++;
		} else {
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





	// Update is called once per frame
	void Update () {
		
	}
}
