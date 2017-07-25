using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tools  {

    public Tools()
    {

    }

    //Removes the first and last character from a text
    /*
     * Notes: I don't know if it's normal or it is just me who is missing something
     * The free JSONObject library I am using has an issue when retrieving string values from a JSONObject
     * I need to remove the " character at the begining and the end of the returned value
     * 
     * Example
     * Without this function the result would : "yle" when it's supposed to be simply yle 
     */
    public string RemoveFirstLastCharacter(string text)
    {
        text = text.Remove(0, 1);
        text = text.Remove(text.Length - 1, 1);

        return text;
    }


    //returns a texture based on a filePath from the project
    public Texture2D LoadImage(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        else
        {
            Debug.Log("image path does not exist");
        }
        return tex;
    }

}
