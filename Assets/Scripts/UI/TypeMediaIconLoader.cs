using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeMediaIconLoader : MonoBehaviour {

    private RawImage rawimage;
    private Tools tool;

    public string TypeMedia;
    public bool flag = true;

    // Use this for initialization
    void Start () {
        InitComponent();



    }
	
	// Update is called once per frame
	void Update () {
        //if TypeMedia value has changed then load the correct icon depending on which typemedia it is
        if (flag)
        {

            if (TypeMedia == "RadioContent")
            {
                rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/radioicon.png");
            }
            else if (TypeMedia == "TVContent")
            {
                rawimage.texture = tool.LoadImage("./Assets/Sprites/Icons/tvicon.png");
            }


            flag = false;
        }
	}


    private void InitComponent()
    {
        rawimage = GetComponent<RawImage>();
        tool = new Tools();
    }
}
