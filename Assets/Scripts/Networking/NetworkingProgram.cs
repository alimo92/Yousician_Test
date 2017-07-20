using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NetworkingProgram : MonoBehaviour {

    private UrlBuilder urlbuilder;
    private ImageUrlBuilder imageurlbuilder;

    private WWW Request;
    private string url;

    private ProgramJob programjob;

    [SerializeField]
    private Transform prefab_programitem;

    private GameObject parent_programitems; 



    // Use this for initialization
    void Start () {
        initComponent();
        StartCoroutine(WaitForResponse());
    }
	
	// Update is called once per frame
	void Update () {
        
        if (programjob != null)
        {
            if (programjob.Update())
            {
                programjob = null;
            }
        }
    }


    private void initComponent()
    {
        urlbuilder = new UrlBuilder();
        imageurlbuilder = new ImageUrlBuilder();

        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "order", "publication.starttime:desc");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "q", "laulu");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "limit", "100");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "offset", "0");

        url = urlbuilder.GetUrl(urlbuilder.GetListUrlItem());

        Debug.Log(url);

        url = urlbuilder.GetNewListUrl("100","20");



        parent_programitems = GameObject.Find("Image");
        if (parent_programitems == null)
        {
            Debug.Log("Image Gameobject not found");
        }

        Debug.Log(url);

    }


    IEnumerator WaitForResponse()
    {
        Request = new WWW(url);
        yield return Request;

        if (Request.error == null)
        {
            
            /*
            string path = "./programs.txt";
            string file_content = programparser.GetJson(list_program).Print();
            //string file_content = data.Print();
            File.WriteAllText(path, file_content);
            */


            programjob = new ProgramJob(Request.text, prefab_programitem, parent_programitems.transform);
            programjob.Start();
        }
        else
        {
            Debug.Log(Request.error);
        }
    }
}
