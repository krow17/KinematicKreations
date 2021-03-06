﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Material blueMat;
	public Material whiteMat;
	public Material yellowMat;
	public Material redMat;

	public Sprite playImage;
	public Sprite createImage;
	public Sprite addImage;
	public Sprite removeImage;
	public Sprite magnetsOnImage;
	public Sprite magnetsOffImage;
	public Sprite destroyJointsOnImage;
	public Sprite destroyJointsOffImage;
	public Sprite groundJointOffImage;
	public Sprite groundJointOnImage;


    public static GameManager instance = null;
    public Config config;

	public Parameters parameters;

	public bool playMode = false;
	public bool magnetsActive = true;

    public bool destroyJoint = false;
	public bool groundJoint = false;

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
