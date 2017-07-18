using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
