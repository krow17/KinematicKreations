using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LShape : MonoBehaviour {

	public enum Color {Blue,White,Yellow,Red};

	int layer;
	Color color;
	bool selected;

	public GameObject northOne;
	public GameObject northTwo;
	public GameObject northThree;
	public GameObject southOne;
	public GameObject southTwo;
	public GameObject southThree;

	void Start () {
		layer = 0;
		color = Color.Blue; //WE SHOULD RANDOMIZE;
		selected = true;
		transform.position = new Vector3 (0, 0, 0);
		CheckLocation();
	}

	public void changeColor(Color newColor)
	{
		switch(newColor)
		{
		case Color.Blue:
			color = Color.Blue;
			this.GetComponent<Renderer> ().material = GameManager.instance.blueMat;
			break;
		case Color.White:
			color = Color.White;
			this.GetComponent<Renderer> ().material = GameManager.instance.whiteMat;
			break;
		case Color.Yellow:
			color = Color.Yellow;
			this.GetComponent<Renderer> ().material = GameManager.instance.yellowMat;
			break;
		case Color.Red:
			color = Color.Red;
			this.GetComponent<Renderer> ().material = GameManager.instance.redMat;
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	Vector3 CheckLocation(){
		//Vector3 vec = new Vector3 (0, 0, 0);
		//vec = Config.configuration [0].transform.position;
		//return vec;
		return this.transform.position;
	}


}
