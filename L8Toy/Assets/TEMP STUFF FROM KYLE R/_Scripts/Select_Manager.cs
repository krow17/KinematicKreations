using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Manager : MonoBehaviour {

    public static Select_Manager sm = null;
    public bool selected = false;
    public bool contact_selectecd = false;

    public GameObject contact_1 = null;
    public GameObject contact_2 = null;
    public GameObject selected_L = null;

    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float distance = 0.0f;

	
    void Awake()
    {
        if(sm == null)
        {
            sm = this;
        }

        else if(sm != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		 
	}

    public void Reset()
    {
        selected = false;
        contact_selectecd = false;

        contact_1 = null;
        contact_2 = null;
        selected_L = null;

        distance = 0.0f;
    }
}
