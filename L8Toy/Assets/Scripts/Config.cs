using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	public int numberOfPieces;

	public static List<GameObject> configuration = new List<GameObject> ();
	// Use this for initialization
	void Start () {
		
	}

	public void addPiece(GameObject pieceToAdd)
	{
		numberOfPieces++;
		configuration.Add (pieceToAdd);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
