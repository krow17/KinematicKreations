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
		if (!GameManager.instance.playMode) {
			if (addL) {
				GameManager.instance.mfuncs.addPiece();
			} else {
				if (GameManager.instance.config.currentSelection != null) {
					GameManager.instance.mfuncs.removePiece (GameManager.instance.config.currentSelection);
				}
			}
		}
	}

	public void changeLayer(string direction)
	{
        if (GameManager.instance.config.currentSelection == null)
        {
            return;
        }
            switch (direction) 
		{
		case "raise":
                foreach (SubConfig sc in GameManager.instance.config.subconfigs)
                {
                    if (sc.subconfiguration.Contains(GameManager.instance.config.currentSelection))
                    {
                        sc.Raise();
                    }
                }
			break;

		case "lower":
                foreach (SubConfig sc in GameManager.instance.config.subconfigs)
                {
                    if (sc.subconfiguration.Contains(GameManager.instance.config.currentSelection))
                    {
                        sc.Lower();
                    }
                }
                break;
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
			GetComponent<Image> ().sprite = GameManager.instance.playImage;
		} else {
			GetComponent<Image> ().sprite = GameManager.instance.createImage;

		}
		GameManager.instance.mfuncs.playCreateModeSwitch();
	}
}
