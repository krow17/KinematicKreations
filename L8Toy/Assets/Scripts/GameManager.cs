﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

	void Awake () {
		
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

	}

    public void changeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

	void Update () {
		
	}
}
