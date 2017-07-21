using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeMediaIconLoader : MonoBehaviour {

    private RawImage rawimage;
    private Tools tool;

    public string TypeMedia;

    // Use this for initialization
    void Start () {
        InitComponent();


        if (TypeMedia =="RadioContent")
        {
            rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/radioicon.png");
        }
        else if (TypeMedia =="TVContent")
        {
            rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/tvicon.png");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void InitComponent()
    {
        rawimage = GetComponent<RawImage>();
        tool = new Tools();
    }
}
