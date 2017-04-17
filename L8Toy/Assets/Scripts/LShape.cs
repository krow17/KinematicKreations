using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LShape : MonoBehaviour {

	public enum Colors {Blue,White,Yellow,Red};

	public int layer;
	public Colors color;
	public Color glowColor;
	public bool selected;

	public Magnet northOne;
	public Magnet northTwo;
	public Magnet northThree;
	public Magnet southOne;
	public Magnet southTwo;
	public Magnet southThree;

	public float highlightTimer;

	void Start () {
		
		northOne.GetComponent<Magnet> ().pole = Magnet.Pole.North;
		northTwo.GetComponent<Magnet> ().pole = Magnet.Pole.North;
		northThree.GetComponent<Magnet> ().pole = Magnet.Pole.North;

		southOne.GetComponent<Magnet> ().pole = Magnet.Pole.South;
		southTwo.GetComponent<Magnet> ().pole = Magnet.Pole.South;
		southThree.GetComponent<Magnet> ().pole = Magnet.Pole.South;

		highlightTimer = 0;
		layer = 0;


		int val = Random.Range(0,4);
		if (val == 0)
			color = Colors.Blue;
		if (val == 1)
				color = Colors.Red;
		if (val == 2)
				color = Colors.White;
		if (val == 3)
				color = Colors.Red;
		

		selected = true;
		transform.position = new Vector3 (0, 0, 0);
	}

	public void changeColor(Colors newColor)
	{
		switch(newColor)
		{
		case Colors.Blue:
			color = Colors.Blue;
			this.GetComponent<Renderer> ().material = GameManager.instance.blueMat;
			break;
		case Colors.White:
			color = Colors.White;
			this.GetComponent<Renderer> ().material = GameManager.instance.whiteMat;
			break;
		case Colors.Yellow:
			color = Colors.Yellow;
			this.GetComponent<Renderer> ().material = GameManager.instance.yellowMat;
			break;
		case Colors.Red:
			color = Colors.Red;
			this.GetComponent<Renderer> ().material = GameManager.instance.redMat;
			break;
		}
	}

	void glow()
	{
		if (selected) {
			highlightTimer += Time.deltaTime;
			float emission = Mathf.PingPong (highlightTimer, 1.0f);
			GetComponent<Collider> ().GetComponent<Renderer> ().material.SetColor ("_EmissionColor", glowColor * Mathf.LinearToGammaSpace (emission));
		} else {
			highlightTimer = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		glow ();
	}
}
