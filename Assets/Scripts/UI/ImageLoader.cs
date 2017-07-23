using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    public string ImageUrl = "";
    public bool flag = true;

    private RawImage rawimage;

    private Tools tool;

    [SerializeField]
    private GameObject loading_circle;

    // Use this for initialization
    void Start () {
        InitComponent();

    }
	
	// Update is called once per frame
	void Update () {
        if (flag)
        {
            if (ImageUrl == "")
            {
                loading_circle.SetActive(false);
                GetComponent<RawImage>().enabled = true;
                rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png");
            }
            else
            {
                loading_circle.SetActive(true);
                StartCoroutine(WaitForResponse());
            }

            flag = false;
        }
	}


    IEnumerator WaitForResponse()
    {
        WWW Request = new WWW(ImageUrl);
        yield return Request;

        if (Request.error == null)
        {
            loading_circle.SetActive(false);
            GetComponent<RawImage>().enabled = true;
            rawimage.texture= Request.texture;

        }
        else
        {
            loading_circle.SetActive(false);
            GetComponent<RawImage>().enabled = true;
            rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png");
        }
    }


    private void InitComponent()
    {
        rawimage = GetComponent<RawImage>();
        tool = new Tools();
    }


}
