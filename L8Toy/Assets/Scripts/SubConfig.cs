﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SubConfig {

	public List<GameObject> subconfiguration = new List<GameObject>();


	void Move(){
		//move whole subconfig

	}


	public void Raise(){
        if (!checkLayerLimits("raise"))
        {
            foreach (GameObject con in subconfiguration)
            {
                con.GetComponent<LShape>().layer++;
                con.GetComponent<Transform>().position += new Vector3(0, GameManager.instance.parameters.layerThickness, 0);
            }
        }
	}

	public void Lower(){
        if (!checkLayerLimits("lower"))
        {
            foreach (GameObject con in subconfiguration)
            {
                con.GetComponent<LShape>().layer--;
                con.GetComponent<Transform>().position -= new Vector3(0, GameManager.instance.parameters.layerThickness, 0);
            }
        }
	}

    bool checkLayerLimits(string raiseOrLower)
    {
        switch(raiseOrLower)
        {
            case "raise":
                foreach (GameObject con in subconfiguration)
                {
                    if (con.GetComponent<LShape>().layer == GameManager.instance.parameters.layerLimit)
                    {
                        return true;
                    }
                }
                break;
            case "lower":
                foreach (GameObject con in subconfiguration)
                {
                    if (con.GetComponent<LShape>().layer == -GameManager.instance.parameters.layerLimit)
                    {
                        return true;
                    }
                }
                break;
        }
        return false;
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
			
		//declare new list, put first l in it *DONE

		// check 1st L's joints for any other L that is in the OG list
				// if connected, put that other L in the new List
				// go to next connection to see if there are more, repeat untill no more connections in first L
				// move to next L in list (repeat)
				// once exhaused, put remaining L's in OG list to 2nd new List
	}

	bool checkListForL(ref SubConfig List1)
	{
		bool done = true;

		foreach (GameObject shape in List1.subconfiguration) 
		{
			Magnet[] mag = shape.GetComponent<LShape>().GetComponentsInChildren<Magnet>();
			foreach (Magnet m in mag) 
			{
				if (m.connection != null) 
				{
					foreach (GameObject L in subconfiguration) 
					{
						if (m.connection.GetComponent<Magnet>().LShape == L) 
						{
							if (!List1.subconfiguration.Contains (L)) 
							{
								List1.subconfiguration.Add (L);
								return false;
							}
						}
					}
				}
			}
		}
		return done;
	}


	public void Merge(SubConfig con1){

		if (this != con1)
		{
			foreach (GameObject c in con1.subconfiguration) 
			{
				if (!subconfiguration.Contains (c))
				{
					subconfiguration.Add(c);
				}
			}
			GameManager.instance.config.subconfigs.Remove (con1);
		}
	}
		
}
