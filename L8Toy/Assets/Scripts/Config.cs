using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	public int numberOfPieces;
	public GameObject currentSelection;

	public List<GameObject> configuration = new List<GameObject> ();
	public List<SubConfig> subconfigs = new List<SubConfig> ();
	// Use this for initialization
	void Start () {
		
	}

	public void addPiece(GameObject pieceToAdd)
	{
		numberOfPieces++;
		configuration.Add (pieceToAdd);
		subconfigs.Add (new SubConfig());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
