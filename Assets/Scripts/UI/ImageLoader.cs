using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    public string ImageUrl = "";
    private RawImage rawimage;

    private Tools tool;

    // Use this for initialization
    void Start () {
        InitComponent();
        if (ImageUrl == "")
        {
            rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png");
        }
        else
        {
            StartCoroutine(WaitForResponse());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator WaitForResponse()
    {
        WWW Request = new WWW(ImageUrl);
        yield return Request;

        if (Request.error == null)
        {
            rawimage.texture= Request.texture;
        }
        else
        {
            Debug.Log(Request.error);
        }
    }


    private void InitComponent()
    {
        rawimage = GetComponent<RawImage>();
        tool = new Tools();
    }


}
