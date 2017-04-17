using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubConfig : MonoBehaviour {

	List<GameObject> subconfiguration;


	void Move(){
		//move whole subconfig

	}


	void Raise(){
		foreach (GameObject con in subconfiguration) {
			con.GetComponent<LShape> ().layer++;
		}
	}

	void Lower(){
		foreach (GameObject con in subconfiguration) {
			con.GetComponent<LShape> ().layer--;
		}

	}


	public SubConfig Split(){

		bool complete = false;
		SubConfig List1 = new SubConfig ();
		SubConfig List2 = new SubConfig ();

		List1.subconfiguration.Add (subconfiguration[0]);

		while (!complete) {
			complete = checkListForL (ref List1);
		}

		foreach (GameObject L in subconfiguration) {
			if (!List1.subconfiguration.Contains (L)) {
				List2.subconfiguration.Add (L);
			}
		}

		subconfiguration = List1.subconfiguration;
		return List2;
			
		//declare new list, put first l in it
		// check 1st L's joints for any other L that is in the OG list
				// if connected, put that other L in the new List
				//go to next connection to see if there are more, repeat untill no more connections in first L
				// move to next L in list (repeat)
				// once exhaused, put remaining L's in OG list to 2nd new List


	}

	bool checkListForL(ref SubConfig List1)
	{
		foreach (GameObject shape in List1.subconfiguration) {
			Magnet[] mag = shape.GetComponent<LShape>().GetComponentsInChildren<Magnet>();

			foreach (Magnet m in mag) {
				if (m.connection != null) {
					foreach (GameObject L in subconfiguration) {
						if (m.connection.GetComponent<Magnet>().LShape == L) {
							if (!List1.subconfiguration.Contains (L)) {
								List1.subconfiguration.Add (L);
								return true;
							}
						}
					}
				}
			}
		}
		return false;
	}


	void Merge(SubConfig con1){
		
		foreach (GameObject c in con1.subconfiguration) {
			subconfiguration.Add (c);
		}
		GameManager.instance.config.subconfigs.Remove (con1);
	}

}
