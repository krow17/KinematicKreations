using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {

	public int numberOfPieces;
	public GameObject currentSelection;
	public GameObject groundedJoint = null;

	public List<GameObject> configuration = new List<GameObject> ();
	public List<SubConfig> subconfigs = new List<SubConfig> ();
	// Use this for initialization
	void Start () {
		
	}

	public void addPiece(GameObject pieceToAdd)
	{
		numberOfPieces++;
		configuration.Add (pieceToAdd);
        SubConfig sc = new SubConfig();
        sc.subconfiguration.Add(pieceToAdd);
		subconfigs.Add (sc);
	}

	public void removePiece(GameObject pieceToRemove)
	{
		configuration.Remove(pieceToRemove);
		numberOfPieces--;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
