using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    public string ImageUrl = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";

    // Use this for initialization
    void Start () {
        StartCoroutine(WaitForResponse());
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
            RawImage rawimage = GetComponent<RawImage>();
            rawimage.texture= Request.texture;
        }
        else
        {
            Debug.Log(Request.error);
        }
    }
}
