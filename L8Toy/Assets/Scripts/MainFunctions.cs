using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFunctions : MonoBehaviour {


	public GameObject LPiece;

	//Use this to add all the main functions we will call.


	// Use this for initialization
	void Start () {
		
	}

	public void playCreateModeSwitch()
	{
		if (GameManager.instance.playMode) 
		{
			unselectAllPieces ();
			GameManager.instance.playMode = false;
		}
		else
		{
			GameManager.instance.playMode = true;
		}
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

	public void removePiece(GameObject pieceToRemove)
	{
		GameManager.instance.config.configuration.Remove(pieceToRemove);
		// REMOVE ALL JOINTS!!!!!!!!
		List<Magnet> magnets = pieceToRemove.GetComponentsInChildren<Magnet>();
		foreach(Magnet m in magnets)
		{
			removeJoint (m);
		}
		Destroy (pieceToRemove);
	}

	public void selectPiece(GameObject piece)
	{
		piece.GetComponent<LShape> ().selected = true;
		GameManager.instance.config.currentSelection = piece;
	}

	public void unselectPiece(GameObject piece)
	{
		piece.GetComponent<LShape> ().selected = false;
		GameManager.instance.config.currentSelection = null;
	}

	public void unselectAllPieces()
	{
		foreach (GameObject p in GameManager.instance.config.configuration) {
			unselectPiece (p);
		}
	}

	public void addJoint(GameObject magnetOne, GameObject magnetTwo)
	{
		//check they are free or ERROR
		//check Poles are opposite or ERROR
		// make magnet.connection = to opposite for each
		//connect joints
		//create joint between pieces
	}

	public void removeJoint(GameObject magnet)
	{
		if (magnet.GetComponent<Magnet> ().connection != null) {
			//remove joint between pieces
		}
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
