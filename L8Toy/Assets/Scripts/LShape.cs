using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LShape : MonoBehaviour {

	public enum Color {Blue,Green,Yellow,Red};

	int layer;
	Color color;
	bool selected;

	void Start () {
		layer = 0;
		color = Color.Blue; //WE SHOULD RANDOMIZE;
		selected = true;
		transform.position = new Vector3 (0, 0, 0);
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
