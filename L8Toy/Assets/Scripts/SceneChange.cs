using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void changeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
