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
    private ExtendList extendlist;
    private bool SendindRequestAllowed = true;
    private int count = 0;



    // Use this for initialization
    void Start () {
        initComponent();
        StartCoroutine(WaitForResponse());
        //var temp_object = GameObject.Instantiate(prefab_programitem);
        //temp_object.parent = parent_programitems.transform;
    }
	
	// Update is called once per frame
	void Update () {


        if (SendindRequestAllowed && extendlist.end_reached)
        {
            SendindRequestAllowed = false;
            int temp = count * 10;
            url = urlbuilder.GetNewListUrl(temp+"","10");
            //StartCoroutine(WaitForResponse());
            Debug.Log(parent_programitems.transform.childCount);
        }
        

        if (programjob != null)
        {
            if (programjob.Update())
            {
                SendindRequestAllowed = true;
                programjob = null;
                count++;
            }
        }

    }


    private void initComponent()
    {
        urlbuilder = new UrlBuilder();
        imageurlbuilder = new ImageUrlBuilder();

        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "order", "publication.starttime:desc");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "q", "laulu");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "limit", "50");
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "offset", "0");

        url = urlbuilder.GetUrl(urlbuilder.GetListUrlItem());



        parent_programitems = GameObject.Find("ListProgram");
        if (parent_programitems == null)
        {
            Debug.Log("Image Gameobject not found");
        }

        extendlist = GameObject.Find("Panel").GetComponent<ExtendList>();
        if (extendlist == null)
        {
            Debug.Log("extendlist not found");
        }

        Debug.Log(url);

    }


    IEnumerator WaitForResponse()
    {
        Request = new WWW(url);
        yield return Request;

        if (Request.error == null)
        {

            programjob = new ProgramJob(Request.text, prefab_programitem, parent_programitems.transform);

            programjob.Start();
            
        }
        else
        {
            Debug.Log(Request.error);
        }
    }
}
