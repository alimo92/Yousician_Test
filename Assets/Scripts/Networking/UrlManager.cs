using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UrlManager : MonoBehaviour {

    public int step;
    public int count;

    public string research_keyword;

    [SerializeField]
    private GameObject TVToggleObject;

    [SerializeField]
    private GameObject RadioToggleObject;

    [SerializeField]
    private Transform prefab_content;

    [SerializeField]
    private GameObject object_pool;


    private Toggle TVToggle;
    private Toggle RadioToggle;
    private SimpleObjectPool simple_object_pool;
    private NetworkingProgram networkingprogram;

    public string MediaTypeValue ;
    public string URL;

    private ProgramService programservice;
    private UrlBuilder urlbuilder;


    // Use this for initialization
    void Start () {
        InitComponent();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void InitComponent()
    {
        programservice = new ProgramService();
        urlbuilder = new UrlBuilder();


        URL = urlbuilder.GetUrl();
        MediaTypeValue = "ALL";
        UpdateURL();

        GetComponent<NetworkingProgram>().URL = URL;

        TVToggle = TVToggleObject.GetComponent<Toggle>();
        TVToggle.onValueChanged.AddListener(ToggleListener);

        RadioToggle = RadioToggleObject.GetComponent<Toggle>();
        RadioToggle.onValueChanged.AddListener(ToggleListener);


        simple_object_pool = object_pool.GetComponent<SimpleObjectPool>();

    }


    private void ToggleListener(bool value)
    {
        HandleMediaTypeValue();
        UpdateURL();
        programservice.RemoveAllPrograms(prefab_content, simple_object_pool);
    }

    private void HandleMediaTypeValue()
    {

        if(!TVToggle.isOn && !RadioToggle.isOn || TVToggle.isOn && RadioToggle.isOn)
        {
            MediaTypeValue = "ALL";
        }else if (TVToggle.isOn && !RadioToggle.isOn)
        {
            MediaTypeValue = "TV";
        }else if(!TVToggle.isOn && RadioToggle.isOn)
        {
            MediaTypeValue = "Radio";
        }
    }

    private void UpdateURL()
    {
        switch (MediaTypeValue)
        {
            case "ALL":
                urlbuilder.RemoveUrlItemFromList(urlbuilder.GetListUrlItem(), "mediaobject");
                break;

            case "TV":
                urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "mediaobject", "video");
                break;

            case "Radio":
                urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "mediaobject", "audio");
                break;
        }
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "order", "publication.starttime:desc");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "limit", "50");
        urlbuilder.AlterUrlItemFromList(urlbuilder.GetListUrlItem(), "offset", "0");
        URL = urlbuilder.GetUrl(urlbuilder.GetListUrlItem());
    }

}
