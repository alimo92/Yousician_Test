using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingProgram : MonoBehaviour {

    private UrlBuilder urlbuilder;
    private ImageUrlBuilder imageurlbuilder;

    private WWW Request;
    private string url;

    private ProgramJob programjob;



    // Use this for initialization
    void Start () {
        initComponent();
        StartCoroutine(WaitForResponse());

        List<string> list_attributes = new List<string>();
        list_attributes.Add("w_120");
        list_attributes.Add("h_120");
        list_attributes.Add("r_max");


        string transformation = imageurlbuilder.ConstructTransformation(list_attributes);

        //Debug.Log(imageurlbuilder.GetImageUrl(transformation, "13-1-2185369","png"));
        //Debug.Log(urlbuilder.GetUrl());



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
        urlbuilder.AddUrlItemToList(urlbuilder.GetListUrlItem(), "q", "mummi");

        url = urlbuilder.GetUrl(urlbuilder.GetListUrlItem());
    }


    IEnumerator WaitForResponse()
    {
        Request = new WWW(url);
        yield return Request;

        if (Request.error == null)
        {
            // Show results as text
            Debug.Log(Request.text);


            DescriptionParser descriptionparser = new DescriptionParser();
            PublicationEventParser publicationeventparser = new PublicationEventParser();

            JSONObject result = new JSONObject(Request.text);
            JSONObject data = result.GetField("data");
            JSONObject Program = data[24];
            JSONObject Description = Program.GetField("description");
            JSONObject publicationEvents = Program.GetField("publicationEvent");


            List<Description> list_description = descriptionparser.GetListObject(Description);
            List<PublicationEvent> list_publicationevent = publicationeventparser.getListObject(publicationEvents);


            Debug.Log(publicationeventparser.GetJson(list_publicationevent));



            programjob = new ProgramJob();
            programjob.Start();
        }
        else
        {
            Debug.Log(Request.error);
        }
    }
}
