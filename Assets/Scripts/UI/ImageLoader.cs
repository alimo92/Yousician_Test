using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    public string ImageUrl = "";
    public bool flag = true;
    public string ImageId;
    public List<string> ListImageAttributes;

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
        if (flag)
        {

            if (ImageId == "not available")
            {
                loading_circle.SetActive(false);
                GetComponent<RawImage>().enabled = true;
                rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/noimage.png");
            }
            else
            {
                ImageUrl = imageurlbuilder.GetImageUrl(imageurlbuilder.ConstructTransformation(ListImageAttributes), ImageId, "png");
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
        imageurlbuilder = new ImageUrlBuilder();
        tool = new Tools();
    }


}
