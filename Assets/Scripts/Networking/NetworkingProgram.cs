using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NetworkingProgram : MonoBehaviour {

    private UrlBuilder urlbuilder;
    public string URL;

    public string SearchKeyWord;

    private WWW Request;

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

    private ProgramService programservice;
    private ExtendList extendlist;
    private bool SendindRequestAllowed = true;
    private int count = 0;


    [SerializeField]
    private GameObject TVToggleObject;

    [SerializeField]
    private GameObject RadioToggleObject;

    [SerializeField]
    private GameObject SearchBarObject;

    private Toggle TVToggle;
    private Toggle RadioToggle;
    private InputField SearchBar;

    public string MediaTypeValue;

    [SerializeField]
    private List<UrlItem> list_url_item;



    [SerializeField]
    private GameObject UrlObject;


    // Use this for initialization
    void Start () {
        initComponent();
        //StartCoroutine(WaitForResponse());
    }
	
	// Update is called once per frame
	void Update () {

        
        if (SendindRequestAllowed && extendlist.end_reached )
        {
            SendindRequestAllowed = false;

            int temp = count * step;

            //URL = urlmanager.URL;
            //URL = urlbuilder.GetNewListUrl(temp + "", step + "");
            urlbuilder.AlterUrlItemFromList(list_url_item, "offset", temp + "");
            URL = urlbuilder.GetUrl(list_url_item);

            StartCoroutine(WaitForResponse());
            //Debug.Log(parent_programitems.transform.childCount);
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
        programservice = new ProgramService();

        MediaTypeValue = "ALL";

        TVToggle = TVToggleObject.GetComponent<Toggle>();
        TVToggle.onValueChanged.AddListener(ToggleListener);

        RadioToggle = RadioToggleObject.GetComponent<Toggle>();
        RadioToggle.onValueChanged.AddListener(ToggleListener);

        SearchBar = SearchBarObject.GetComponent<InputField>();
        SearchBar.onValueChanged.AddListener(InputListener);


        list_url_item = InitListURLItem(urlbuilder);

        URL = urlbuilder.GetUrl(list_url_item);

        
        
        extendlist = ScrollView.GetComponent<ExtendList>();
        if (extendlist == null)
        {
            Debug.Log("extendlist not found");
        }



    }


    IEnumerator WaitForResponse()
    {
        Request = new WWW(URL);
        UrlObject.GetComponent<Text>().text = URL;
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


    private List<UrlItem> InitListURLItem(UrlBuilder urlbuilder)
    {
        List<UrlItem> list = urlbuilder.GetListUrlItem();

        urlbuilder.AlterUrlItemFromList(list, "order", "publication.starttime:desc");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "limit", step+"");

        return list;
    } 


    private void InputListener(string value)
    {
        count = 0;
        SearchKeyWord = SearchBar.text;
        UpdateListURLItem();
        programservice.RemoveAllPrograms(prefab_content, object_pool.GetComponent<SimpleObjectPool>());
    }


    private void ToggleListener(bool value)
    {
        count = 0;
        HandleMediaTypeValue();
        UpdateListURLItem();
        programservice.RemoveAllPrograms(prefab_content, object_pool.GetComponent<SimpleObjectPool>());
    }


    private void HandleMediaTypeValue()
    {

        if (!TVToggle.isOn && !RadioToggle.isOn || TVToggle.isOn && RadioToggle.isOn)
        {
            MediaTypeValue = "ALL";
        }
        else if (TVToggle.isOn && !RadioToggle.isOn)
        {
            MediaTypeValue = "TV";
        }
        else if (!TVToggle.isOn && RadioToggle.isOn)
        {
            MediaTypeValue = "Radio";
        }
    }



    private void UpdateListURLItem()
    {
        switch (MediaTypeValue)
        {
            case "ALL":
                urlbuilder.RemoveUrlItemFromList(list_url_item, "mediaobject");
                break;

            case "TV":
                urlbuilder.AlterUrlItemFromList(list_url_item, "mediaobject", "video");
                break;

            case "Radio":
                urlbuilder.AlterUrlItemFromList(list_url_item, "mediaobject", "audio");
                break;
        }
        if (SearchKeyWord != "")
        {
            urlbuilder.AlterUrlItemFromList(list_url_item, "q", SearchKeyWord);
        }
    }

    private float time = 0;
    private bool timer(float period)
    {
        time = time + Time.deltaTime;
        if (time > period)
        {
            time = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
