using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	List<GameObject> buttonsToFade;

	// Use this for initialization
	void Start () {
		buttonsToFade = GameObject.Find ("Buttons").GetComponent<ButtonList>().buttonsToFade;
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
		if (!GameManager.instance.playMode) {
			if (GameManager.instance.config.currentSelection == null) {
				return;
			}
			switch (direction) {
			case "raise":
				foreach (SubConfig sc in GameManager.instance.config.subconfigs) {
					if (sc.subconfiguration.Contains (GameManager.instance.config.currentSelection)) {
						sc.Raise ();
					}
				}
				break;

			case "lower":
				foreach (SubConfig sc in GameManager.instance.config.subconfigs) {
					if (sc.subconfiguration.Contains (GameManager.instance.config.currentSelection)) {
						sc.Lower ();
					}
				}
				break;
			}
		}
	}

	public void colorButton(string color)
	{
		if (!GameManager.instance.playMode && GameManager.instance.config.currentSelection != null) {
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
	}

	public void clearButton()
	{
		if (!GameManager.instance.playMode) {
			GameManager.instance.mfuncs.clear ();
		}
	}

	public void zoomButton(bool zoomIn)
	{
		GameManager.instance.mfuncs.zoom (zoomIn);
	}

	public void magnetButton()
	{
		if (!GameManager.instance.playMode) {
			if (GameManager.instance.magnetsActive)
			{
				GameManager.instance.mfuncs.MagnetToggle (false);
			} 
			else
			{
				GameManager.instance.mfuncs.MagnetToggle (true);
			}
		}

	}

    public void destroyJointButton()
    {
        if(!GameManager.instance.playMode)
        {
			if (GameManager.instance.destroyJoint)
			{
				GameManager.instance.mfuncs.DestroyJoint(false);
			} 
			else
			{
				GameManager.instance.mfuncs.DestroyJoint(true);
			}
                
        }
    }


    public void viewAll()
    {
        GameManager.instance.mfuncs.viewAll();
    }

    public void playCreateButton()
	{
		if (GameManager.instance.playMode) {
			GetComponent<Image> ().sprite = GameManager.instance.playImage;
			foreach (GameObject b in buttonsToFade) {
				b.GetComponent<Image> ().color = GameManager.instance.parameters.buttonColor;
			}
		} else {
			GetComponent<Image> ().sprite = GameManager.instance.createImage;
			foreach (GameObject b in buttonsToFade) {
				b.GetComponent<Image> ().color = GameManager.instance.parameters.buttonColorFaded;
			}
		}
		GameManager.instance.mfuncs.playCreateModeSwitch();
	}


}
