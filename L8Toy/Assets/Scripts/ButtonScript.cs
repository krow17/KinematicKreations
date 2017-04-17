using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addRemoveButton(bool addL)
	{	
		if (addL) 
		{
			GameManager.instance.mfuncs.addPiece ();
		}
		else
		{
			if (GameManager.instance.config.currentSelection != null)
			{
				GameManager.instance.mfuncs.removePiece(GameManager.instance.config.currentSelection);
			}
		}
	}

	public void colorButton(string color)
	{
		switch (color) {
		case "white":
			GameManager.instance.mfuncs.changeColorOfSelectedL ("white");
			break;

		case "yellow":
			GameManager.instance.mfuncs.changeColorOfSelectedL ("yellow");
			break;

		case "red":
			GameManager.instance.mfuncs.changeColorOfSelectedL ("red");
			break;

		case "blue":
			GameManager.instance.mfuncs.changeColorOfSelectedL ("blue");
			break;
		}
	}

	public void clearButton()
	{
		GameManager.instance.mfuncs.clear();
	}

	public void zoomButton(bool zoomIn)
	{
		GameManager.instance.mfuncs.zoom (zoomIn);
	}

	public void playCreateButton()
	{
		if (GameManager.instance.playMode) {
			GetComponent<Image> ().sprite = GameManager.instance.createImage;
		} else {
			GetComponent<Image> ().sprite = GameManager.instance.playImage;

		}
		GameManager.instance.mfuncs.playCreateModeSwitch ();
	}
}
