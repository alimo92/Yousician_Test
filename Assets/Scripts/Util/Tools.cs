using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tools  {

    public Tools()
    {

    }

    public string RemoveFirstLastCharacter(string text)
    {
        text = text.Remove(0, 1);
        text = text.Remove(text.Length - 1, 1);

        return text;
    }


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
