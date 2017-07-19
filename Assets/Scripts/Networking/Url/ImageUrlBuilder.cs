using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUrlBuilder {


    private string ImageUrlBasic = "http://images.cdn.yle.fi/image/upload/";


    public ImageUrlBuilder()
    {

    }


    //Returns Image based on ImageId, transformation and format
    //transformation alters the the original picture (size, shape ...) example : "w_200,h_200"
    //Supported formats are png, jpg and gif
    public string GetImageUrl(string imagetransformation, string imageid, string imageformat)
    {
        string url = ImageUrlBasic + imagetransformation + "/" + imageid + "." + imageformat;

        return url;
    }


    //Returns ImageUrl based on ImageId and ImageFormat - Original size in a specific format 
    //Supported formats are png, jpg and gif
    public string GetImageUrl(string imageid, string imageformat)
    {
        string url = ImageUrlBasic + imageid + "." + imageformat;

        return url;
    }


    //Returns ImageUrl based on ImageId - Original size in a default format
    public string GetImageUrl(string imageid)
    {
        string url = ImageUrlBasic + imageid;

        return url;
    }


    //Construct transformation string based on a list of attributes
    public string ConstructTransformation(List<string> list_attributes)
    {
        string transformation="";

        for(int i=0;i< list_attributes.Count; i++)
        {
            transformation += list_attributes[i];
            if(i!= list_attributes.Count - 1)
            {
                transformation += ",";
            }
        }

        return transformation;
    }

}
