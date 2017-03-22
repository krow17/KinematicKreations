using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Color : MonoBehaviour 
{
	enum color {Blue,Green,Yellow,Red};
}

public class LShape : MonoBehaviour {

	int layer;
	Color color;

	void Start () {
		transform.position = new Vector3 (0, 0, 0);
		AddPiece();
		AddPiece();
		CheckLocation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void AddPiece(){
		Config.configuration.Add(this.gameObject);
		Debug.Log (Config.configuration.Count);
	}

	Vector3 CheckLocation(){
		//Vector3 vec = new Vector3 (0, 0, 0);
		//vec = Config.configuration [0].transform.position;
		//return vec;
		return this.transform.position;
	}


}
