using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Color color;

    private Vector3 screenPoint;
    private Vector3 offset;

    private float constantHeight;

    public Vector3 moveVector;

    public bool toMove = false;


    // Use this for initialization
    void Start ()
    {
        constantHeight = transform.position.y;	
	}

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(transform.position.x, constantHeight, transform.position.z);
       // transform.rotation = new Quaternion.AngleAxis(0.0f, transform.rotation.y, 0.0f);
    }

    void OnMouseDown()
    {
        if (Select_Manager.sm.selected == false && this.tag == "shape") //select an L
        {
            Select_Manager.sm.selected_L = this.gameObject;
            print(Select_Manager.sm.selected_L);
            this.GetComponent<Renderer>().material.color = new Color(0.0f, 200.0f, 0.0f, 0.0f);
            print("Color was changed");
            Select_Manager.sm.selected = true;
        }

        if (Select_Manager.sm.contact_selectecd == true && this.tag == "contact")
        {
            print("Contact 2 was selected");
            Select_Manager.sm.contact_2 = this.gameObject;
            Select_Manager.sm.targetPosition = Select_Manager.sm.contact_2.transform.position;
            print(Select_Manager.sm.contact_2);
            this.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 200.0f, 0.0f);

            //get distance from contact_1 to contact_2, and get the direction vector
            Select_Manager.sm.distance = Vector3.Distance(Select_Manager.sm.contact_2.transform.position, Select_Manager.sm.contact_1.transform.position);
            print(Select_Manager.sm.distance);
            toMove = true;

            //get move vector 

            
        }

        else if (Select_Manager.sm.selected == true && this.tag == "contact") //select a contact point you'd like to manipulate
        {
            print("contact 1 was selected");
            Select_Manager.sm.contact_1 = this.gameObject;
            Select_Manager.sm.startPosition = Select_Manager.sm.contact_1.transform.position;
            print(Select_Manager.sm.contact_1);
            this.GetComponent<Renderer>().material.color = new Color(200.0f, 0.0f, 0.0f, 0.0f);
            Select_Manager.sm.contact_selectecd = true;
        }

		Debug.Log ("here");
     
    }

    void OnTriggerEnter(Collider other)
    {

        if (this.tag != "shape" && other.tag == "contact")
        {
            //print("Contact was made");
            //this.gameObject.GetComponent<FixedJoint>().connectedBody = transform.parent.gameObject.GetComponent<Rigidbody>();
            this.gameObject.GetComponent<HingeJoint>().connectedBody = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    IEnumerator Reposition()
    {
        if (Select_Manager.sm.contact_1.transform.position != Select_Manager.sm.contact_2.transform.position)
        {
            moveVector = new Vector3(Select_Manager.sm.contact_2.transform.position.x, 0.0f, Select_Manager.sm.contact_2.transform.position.z) - new Vector3(Select_Manager.sm.contact_1.transform.position.x, 0.0f, Select_Manager.sm.contact_1.transform.position.z);
            Select_Manager.sm.contact_1.GetComponent<Rigidbody>().AddForceAtPosition(moveVector * 100.0f, Select_Manager.sm.contact_1.transform.position);
        }
        else
        {
            toMove = false;
        }
        
        yield return new WaitForSeconds(1);
    }


   
}
