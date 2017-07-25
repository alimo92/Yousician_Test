using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    private GameObject SecondScreen;
    public bool state = false;

	// Use this for initialization
	void Start () {
        InitComponent();
        SecondScreen.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        //switch between the first and second screens (panels) based on a bool value
        if (state && SecondScreen.activeSelf==false)
        {
            SecondScreen.SetActive(true);
        }
        else if(!state && SecondScreen.activeSelf == true)
        {
            SecondScreen.SetActive(false);
        }
	}


    private void InitComponent()
    {
        SecondScreen = GameObject.Find("SecondScreen");
        if (SecondScreen == null)
        {
            Debug.Log("Second Screen not found");
        }
    }
}
