using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NetworkingProgram : MonoBehaviour {

    private UrlBuilder urlbuilder;
    public string URL;

    public string Language;

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

    [SerializeField]
    private GameObject big_loading_circle;

    private bool SearchKeywordChanged = false;



    [SerializeField]
    private GameObject FinnishToggleObject;

    [SerializeField]
    private GameObject SwedishToggleObject;


    private Toggle FinnishToggle;
    private Toggle SwedishToggle;

    private bool language_state = true; 

    // Use this for initialization
    void Start () {
        initComponent();
    }
	
	// Update is called once per frame
	void Update () {

        
        //update the url and send request to retrieve data
        if (SendindRequestAllowed && extendlist.end_reached )
        {
            SendindRequestAllowed = false;

            int temp = count * step;

            urlbuilder.AlterUrlItemFromList(list_url_item, "offset", temp + "");
            URL = urlbuilder.GetUrl(list_url_item);
            big_loading_circle.SetActive(true);
            StartCoroutine(WaitForResponse());
        }
        

        //When programjob finishes the task
        if (programjob != null)
        {
            if (programjob.Update())
            {
                SendindRequestAllowed = true;
                programjob = null;
                count++;
                big_loading_circle.SetActive(false);
            }
        }

        //triggers changes only after 1s is passed without typing in the research bar
        if (SearchKeywordChanged)
        {
            if (timer(1))
            {
                SearchKeywordChanged = false;
                count = 0;
                SearchKeyWord = SearchBar.text;
                UpdateListURLItem();
                programservice.RemoveAllPrograms(prefab_content, object_pool.GetComponent<SimpleObjectPool>());
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


        FinnishToggle = FinnishToggleObject.GetComponent<Toggle>();
        FinnishToggle.onValueChanged.AddListener(LanguageToggleListener);


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
            programjob = new ProgramJob(Request.text, prefab_program, prefab_content,Language, object_pool); // initialize the thread parameters
            programjob.Start(); 
        }
        else
        {
            Debug.Log(Request.error);
        }
    }


    //initialize the basic parameters for the url
    private List<UrlItem> InitListURLItem(UrlBuilder urlbuilder)
    {
        List<UrlItem> list = urlbuilder.GetListUrlItem();

        urlbuilder.AlterUrlItemFromList(list, "order", "publication.starttime:desc"); // showing the programs based on starting time made more sense than showing programs dating from 2014
        urlbuilder.AlterUrlItemFromList(list, "limit", step+""); //how many items to bring when the end of the scrolling list is reached
        if (SafeCheckLanguage(Language))
        {
            urlbuilder.AlterUrlItemFromList(list, "language", Language); //if language is specified
        }


        return list;
    } 


    //triggered after typing on 
    private void InputListener(string value)
    {
        /*
         * this is better than launching a request everytime a user enters a character
         * waiting for at least 1s after he stops typing is necessary before sending a request
         */
        SearchKeywordChanged = true; //a change has been made in the research bar
    }


    //Specifies which mediatype is selected (radio, tv or both)
    private void ToggleListener(bool value)
    {
        count = 0;
        HandleMediaTypeValue();
        UpdateListURLItem();
        programservice.RemoveAllPrograms(prefab_content, object_pool.GetComponent<SimpleObjectPool>());
    }

    //Specifies wich language is selected (finnish or swedish)
    private void LanguageToggleListener(bool value)
    {
        if (language_state != value)
        {
            language_state = !language_state;
            count = 0;
            if (value)
            {
                Language = "fi";
            }
            else
            {
                Language = "sv";
            }
            UpdateListURLItem();
            programservice.RemoveAllPrograms(prefab_content, object_pool.GetComponent<SimpleObjectPool>());
        }

    }

    //MediaType is a string variable depending on the state of the MediaTypeToggles
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


    //Updates the URL based on the inputs from the UI
    private void UpdateListURLItem()
    {
        //MediaType 
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

        //Research bar
        if (SearchKeyWord != "")
        {
            urlbuilder.AlterUrlItemFromList(list_url_item, "q", SearchKeyWord);
        }
        else
        {
            urlbuilder.RemoveUrlItemFromList(list_url_item,"q");
        }

        //Language 
        urlbuilder.AlterUrlItemFromList(list_url_item, "language", Language);
    }


    //simple timer, period in seconds
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

    //function for testing purposes, check if language is either finnish or swedish
    private bool SafeCheckLanguage(string language)
    {
        if(language=="fi" || language == "sv")
        {
            return true;
        }

        return false; 
    }

}
