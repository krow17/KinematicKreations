using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFunctions : MonoBehaviour {

    public GameObject LPiece;
    public List<Vector3> SpawnLocations;
	//Use this to add all the main functions we will call.


	// Use this for initialization
	void Start () {
	
		SpawnLocations.Add(new Vector3 (0, 0, 0));
		SpawnLocations.Add(new Vector3 (-.7f, 0, -1.3f));
		SpawnLocations.Add(new Vector3 (-.7f, 0, 2f));
		SpawnLocations.Add(new Vector3 (1.6f, 0, -1.3f));
		SpawnLocations.Add(new Vector3 (1.6f, 0, 2f));
		SpawnLocations.Add(new Vector3 (2.1f, 0, -1.3f));
		SpawnLocations.Add(new Vector3 (2.1f, 0, 2f));
		SpawnLocations.Add(new Vector3 (-1f, 0, 2f));
	}

	public void playCreateModeSwitch()
	{
		if (!GameManager.instance.playMode) 
		{
			unselectAllPieces();
			GameManager.instance.playMode = true;
			MagnetToggle (false);
		}
		else
		{
			GameManager.instance.playMode = false;
			MagnetToggle (true);
		}
	}

	public void addPiece()
	{
			Debug.Log ("clicked");
		if (GameManager.instance.config.numberOfPieces < 8)
		{
			float radius = .1f;
			foreach (Vector3 pos in SpawnLocations) {
				if (!Physics.CheckSphere (pos, radius)) {
					GameObject tempL = Instantiate (LPiece, pos, Quaternion.identity);
					GameManager.instance.config.addPiece(tempL);
					break;
				}
			}
		

			MagnetToggle (true);
		} 
		else 
		{
			Debug.Log ("Limit of pieces reached");
		}



		//instantiate piece and add it to the correct files
	}

	public void removePiece(GameObject pieceToRemove)
	{

		List<GameObject> magnets = new List<GameObject> ();

		magnets.Add (pieceToRemove.GetComponent<LShape>().northOne);
		magnets.Add (pieceToRemove.GetComponent<LShape>().northTwo);
		magnets.Add (pieceToRemove.GetComponent<LShape>().northThree);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southOne);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southTwo);
		magnets.Add (pieceToRemove.GetComponent<LShape>().southThree);

		foreach(GameObject m in magnets)
		{
			removeJoint (m); 
		}
		GameManager.instance.config.configuration.Remove(pieceToRemove);
		unselectPiece (pieceToRemove);
		Destroy (pieceToRemove);
	}

	public void clear (){
		unselectAllPieces();
        foreach (GameObject L in GameManager.instance.config.configuration)
        {
            Destroy(L);
        }
        GameManager.instance.config.subconfigs.Clear();
        GameManager.instance.config.configuration.Clear();
        GameManager.instance.config.numberOfPieces = 0;
	}

	public void selectPiece(GameObject piece)
	{
		unselectAllPieces();
		piece.GetComponent<LShape> ().selected = true;
		GameManager.instance.config.currentSelection = piece;
		GameObject.Find ("Buttons").GetComponent<ButtonList>().addButton.SetActive(false);
		GameObject.Find ("Buttons").GetComponent<ButtonList>().removeButton.SetActive(true);
	}

	public void unselectPiece(GameObject piece)
	{
		piece.GetComponent<LShape>().selected = false;
		GameManager.instance.config.currentSelection = null;
		GameObject.Find ("Buttons").GetComponent<ButtonList>().addButton.SetActive(true);
		GameObject.Find ("Buttons").GetComponent<ButtonList>().removeButton.SetActive(false);
	}

	public void unselectAllPieces()
	{
		foreach (GameObject p in GameManager.instance.config.configuration) {
			unselectPiece (p);
		}
	}

	public void addJoint(GameObject magnetOne, GameObject magnetTwo)
	{

		if (magnetOne.GetComponent<Magnet>().pole != magnetTwo.GetComponent<Magnet>().pole)
		{
			magnetOne.GetComponent<Magnet>().connection = magnetTwo;


			magnetOne.GetComponent<HingeJoint> ().connectedBody = magnetTwo.GetComponent<Rigidbody> ();

			foreach(SubConfig sc in GameManager.instance.config.subconfigs)
			{
				if (sc.subconfiguration.Contains(magnetOne.GetComponent<Magnet>().LShape))
				{
					foreach (SubConfig sc2 in GameManager.instance.config.subconfigs)
					{
						if (sc2.subconfiguration.Contains(magnetTwo.GetComponent<Magnet>().LShape))
						{
							sc.Merge(sc2);
							break;
						}
					}
					break;
				}
			}

			//magnetOne.GetComponentInParent<HingeJoint> ().connectedBody = magnetTwo.GetComponentInParent<Rigidbody> ();
			//magnetTwo.GetComponentInParent<HingeJoint> ().connectedBody = magnetOne.GetComponentInParent<Rigidbody> ();

		}



	}


	public void removeJoint(GameObject magnet)
	{
			GameObject obj = magnet.GetComponent<Magnet>().connection;

		magnet.GetComponent<HingeJoint> ().connectedBody = magnet.GetComponent<JointChecker> ().partnerJoint.GetComponent<Rigidbody>();
		obj.GetComponent<HingeJoint> ().connectedBody = obj.GetComponent<JointChecker> ().partnerJoint.GetComponent<Rigidbody>();
			
			foreach (SubConfig config in GameManager.instance.config.subconfigs) {
				
				if (config.subconfiguration.Contains (obj.GetComponent<Magnet> ().LShape)) {
					GameManager.instance.config.subconfigs.Add (config.Split ());
					break;
				}
			}
				
	
	}

	public void zoom(bool zoomIn)
	{
		if (zoomIn && Camera.main.transform.position.y > 5) 
		{
			Camera.main.transform.Translate (Vector3.forward * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
            GameObject.Find("3D Camera").transform.Translate(Vector3.forward * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
        }
		else if (!zoomIn && Camera.main.transform.position.y < 28) 
		{
			Camera.main.transform.Translate (Vector3.back * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
            GameObject.Find("3D Camera").transform.Translate(Vector3.back * Time.deltaTime * GameManager.instance.parameters.zoomSpeed);
        }
	}

    public void viewAll()
    {
        float zoomOutDistance = 28.0f - Camera.main.transform.position.y + 0.01f;
        Camera.main.transform.Translate(Vector3.back * zoomOutDistance);
        GameObject.Find("3D Camera").transform.Translate(Vector3.back * zoomOutDistance);
    }

    public void changeColorOfSelectedL(string color)
	{
		if (GameManager.instance.config.currentSelection != null) {
			switch(color)
			{
			case "blue":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Blue);
				break;
			case "yellow":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Yellow);
				break;
			case "white":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.White);
				break;
			case "red":
				GameManager.instance.config.currentSelection.GetComponent<LShape> ().changeColor (LShape.Colors.Red);
				break;

			}
		}
	
	}

	public void MagnetToggle(bool on){
		if (on == false) {
			foreach (GameObject p in GameManager.instance.config.configuration) {
				Magnet[] mag = p.GetComponentsInChildren<Magnet> ();

				foreach (Magnet m in mag) {
					m.GetComponent<CapsuleCollider> ().enabled = false; //NOTE: MAKE CAPSULE COLLIDERS FOR MAGNETS
				}
			}
		} else {
			foreach (GameObject p in GameManager.instance.config.configuration) {
				Magnet[] mag = p.GetComponentsInChildren<Magnet> ();

				foreach (Magnet m in mag) {
					m.GetComponent<CapsuleCollider> ().enabled = true;
				}
			}

		}
	}



	// Update is called once per frame
	void Update () {

	}
}
