﻿using System.Collections;
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
		if (!GameManager.instance.playMode) 
		{
			unselectAllPieces();
			GameManager.instance.playMode = true;
		}
		else
		{
			GameManager.instance.playMode = false;
		}
	}

	public void addPiece()
	{
		Debug.Log ("clicked");
		if (GameManager.instance.config.numberOfPieces < 8)
		{
			GameObject tempL = Instantiate (LPiece, Vector3.zero, Quaternion.identity);
			GameManager.instance.config.addPiece(tempL);
			//NOTE: NEED TO DD TO OWN SUBCONFIG
			selectPiece (tempL);
		} 
		else 
		{
			Debug.Log ("Limit of pices reached");
		}


		//instantiate piece and add it to the correct files
	}

	public void removePiece(GameObject pieceToRemove)
	{

		List<GameObject> magnets = new List<GameObject> ();

		magnets.Add (pieceToRemove.GetComponent<LShape>().northOne);
		magnets.Add (pieceToRemove.GetComponent<LShape>().northTwo);
		magnets.Add (pieceToRemove.GetComponent<LShape>().northThree);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southOne);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southTwo);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southThree);

		foreach(GameObject m in magnets)
		{
			removeJoint (m); 
		}
		GameManager.instance.config.configuration.Remove(pieceToRemove);
		Destroy (pieceToRemove);
	}

	public void clear (){
		unselectAllPieces();
		for (int i = 0; i < GameManager.instance.config.configuration.Count; i++) {
			GameManager.instance.config.configuration.Remove (GameManager.instance.config.configuration [i]);
			Destroy(GameManager.instance.config.configuration [i]);
		}
	}

	public void selectPiece(GameObject piece)
	{
		unselectAllPieces();
		piece.GetComponent<LShape> ().selected = true;
		GameManager.instance.config.currentSelection = piece;
	}

	public void unselectPiece(GameObject piece)
	{
		piece.GetComponent<LShape>().selected = false;
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
		if (magnetOne.GetComponent<Magnet>().connection == null && magnetTwo.GetComponent<Magnet>().connection == null) {
			if (magnetOne.GetComponent<Magnet>().pole != magnetTwo.GetComponent<Magnet>().pole)
			{
				magnetOne.GetComponent<Magnet>().connection = magnetTwo;
				magnetTwo.GetComponent<Magnet>().connection = magnetOne;

				magnetOne.AddComponent<HingeJoint>();
				magnetOne.GetComponent<HingeJoint>().axis = new Vector3(0,10,0);

				magnetTwo.AddComponent<HingeJoint>();
				magnetTwo.GetComponent<HingeJoint>().axis = new Vector3(0,10,0);


				magnetOne.GetComponent<HingeJoint> ().connectedBody = magnetTwo.GetComponent<Rigidbody> ();
				magnetTwo.GetComponent<HingeJoint> ().connectedBody = magnetOne.GetComponent<Rigidbody> ();

                foreach(SubConfig sc in GameManager.instance.config.subconfigs)
                {
                    if (sc.subconfiguration.Contains(magnetOne.GetComponent<Magnet>().LShape))
                    {
                        foreach (SubConfig sc2 in GameManager.instance.config.subconfigs)
                        {
                            if (sc2.subconfiguration.Contains(magnetTwo.GetComponent<Magnet>().LShape))
                            {
                                sc.Merge(sc2);
                                break;
                            }
                        }
                        break;
                    }
                }

				//magnetOne.GetComponentInParent<HingeJoint> ().connectedBody = magnetTwo.GetComponentInParent<Rigidbody> ();
				//magnetTwo.GetComponentInParent<HingeJoint> ().connectedBody = magnetOne.GetComponentInParent<Rigidbody> ();

			}
		}


	}

	public void removeJoint(GameObject magnet)
	{
		if (magnet.GetComponent<Magnet>().connection != null) {

			GameObject obj = magnet.GetComponent<Magnet>().connection;

			magnet.GetComponent<HingeJoint>().connectedBody = null;
			obj.GetComponent<HingeJoint>().connectedBody = null;

			obj.GetComponent<Magnet>().connection = null;
			magnet.GetComponent<Magnet>().connection = null;

			foreach (SubConfig config in GameManager.instance.config.subconfigs) {
				
				if (config.subconfiguration.Contains (obj.GetComponent<Magnet> ().LShape)) {
					GameManager.instance.config.subconfigs.Add (config.Split ());
					break;
				}
			}
				
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

	public void changeColorOfSelectedL(string color)
	{
		if (GameManager.instance.config.currentSelection != null) {
			switch(color)
			{
			case "blue":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Blue);
				break;
			case "yellow":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Yellow);
				break;
			case "white":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.White);
				break;
			case "red":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Red);
				break;

			}
		}

	}



	// Update is called once per frame
	void Update () {

	}
}
