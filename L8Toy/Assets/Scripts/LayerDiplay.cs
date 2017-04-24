using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerDiplay : MonoBehaviour {

	public Text layerDisplayText; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.instance.config.currentSelection != null)
		{
			layerDisplayText.text = GameManager.instance.config.currentSelection.GetComponent<LShape> ().layer.ToString ();
		}
		else
		{
			layerDisplayText.text = "";
		}

	}
}
