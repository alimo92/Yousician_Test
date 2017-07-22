using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NetworkingProgram : MonoBehaviour {

    private UrlBuilder urlbuilder;
    private UrlManager urlmanager;

    private WWW Request;
    public string URL;

    private ProgramJob programjob;

    [SerializeField]
    private Transform prefab_program;

    [SerializeField]
    private Transform prefab_content;

    [SerializeField]
    private GameObject object_pool;

    [SerializeField]
    private int step=10;

    [SerializeField]
    private GameObject ScrollView;



    private ExtendList extendlist;
    private bool SendindRequestAllowed = true;
    private int count = 0;



    // Use this for initialization
    void Start () {
        initComponent();
        StartCoroutine(WaitForResponse());
    }
	
	// Update is called once per frame
	void Update () {

        /*
        if (SendindRequestAllowed && extendlist.end_reached )
        {
            SendindRequestAllowed = false;

            int temp = count * step;

            //URL = urlmanager.URL;
            //URL = urlbuilder.GetNewListUrl(temp + "", step + "");



            StartCoroutine(WaitForResponse());
            //Debug.Log(parent_programitems.transform.childCount);
        }
        */

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
        
        
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "order", "publication.starttime:desc");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "q", "laulu");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "mediaobject", "video");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "limit", "50");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "offset", "0");

        URL = urlbuilder.GetUrl(urlbuilder.GetListUrlItem());
        
        
        extendlist = ScrollView.GetComponent<ExtendList>();
        if (extendlist == null)
        {
            Debug.Log("extendlist not found");
        }

        urlmanager = GetComponent<UrlManager>();
        if (urlmanager == null)
        {
            Debug.Log("urlmanager not found");
        }


    }


    IEnumerator WaitForResponse()
    {
        Request = new WWW(URL);
        yield return Request;

        if (Request.error == null)
        {

            programjob = new ProgramJob(Request.text, prefab_program, prefab_content, object_pool);

            programjob.Start();
            
        }
        else
        {
            Debug.Log(Request.error);
        }
    }


}
