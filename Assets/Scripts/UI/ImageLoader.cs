using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    public string ImageUrl = "";
    public bool flag = true;
    public string ImageId;
    public List<string> ListImageAttributes; //initialized from Editor

    private RawImage rawimage;

    private Tools tool;
    private ImageUrlBuilder imageurlbuilder;

    [SerializeField]
    private GameObject loading_circle;

    // Use this for initialization
    void Start () {
        InitComponent();

    }
	
	// Update is called once per frame
	void Update () {
        //if a change occures
        if (flag)
        {

            if (ImageId == "not available") // if image non available, load image from the project
            {
                loading_circle.SetActive(false);
                GetComponent<RawImage>().enabled = true;
                rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png");
            }
            else //otherwise load image based on URL
            {
                ImageUrl = imageurlbuilder.GetImageUrl(imageurlbuilder.ConstructTransformation(ListImageAttributes), ImageId, "png");
                loading_circle.SetActive(true); // Loading circle enabled
                StartCoroutine(WaitForResponse());
            }

            flag = false;
        }
	}


    IEnumerator WaitForResponse()
    {
        WWW Request = new WWW(ImageUrl);
        yield return Request;

        //after getting response
        loading_circle.SetActive(false); //disable loading circle
        GetComponent<RawImage>().enabled = true; //enable image to be seen

        if (Request.error == null)
        {
            rawimage.texture= Request.texture; //load returned image
        }
        else
        {
            rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png"); //in case there's a problem with the image, loads image from project folder
        }
    }


    private void InitComponent()
    {
        rawimage = GetComponent<RawImage>();
        imageurlbuilder = new ImageUrlBuilder();
        tool = new Tools();
    }


}
