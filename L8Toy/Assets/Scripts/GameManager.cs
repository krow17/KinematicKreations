using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null;
    public Config config;

	public Parameters parameters;

	public bool playMode = false;

	public MainFunctions mfuncs;

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

	void Update () {
		
	}
}
