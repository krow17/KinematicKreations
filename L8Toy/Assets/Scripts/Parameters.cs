using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parameters : MonoBehaviour {

	public int maxDragSpeed = 20;
	public int forceMultiplier = 360;
	public int zoomSpeed = 30;
	public float velocityClamp = 10;
    public float layerThickness = 0.027f;
    public int layerLimit = 4;

	public Color buttonColor = new Color(1, 1, 1, 1);
	public Color buttonColorFaded = new Color (1, 1, 1, 0.5f);




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
