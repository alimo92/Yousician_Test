﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendList : MonoBehaviour {

    public bool end_reached= false;

    private ScrollRect scrollrect;
    private Vector2 rect_position;

    // Use this for initialization
    void Start () {

        InitComponent();
    }
	
	// Update is called once per frame
	void Update () {
        end_reached= HandleEnd_Reached(scrollrect, end_reached);
	}

    private void InitComponent()
    {
        scrollrect = GetComponent<ScrollRect>();
    }

    private bool HandleEnd_Reached(ScrollRect scrollrect, bool end_reached)
    {
        rect_position = scrollrect.normalizedPosition;
        if (rect_position.y <0.01)
        {
            scrollrect.normalizedPosition = new Vector2(0,0.03f);
            return true;
        }
        else
        {
            return false;
        }
    }
}