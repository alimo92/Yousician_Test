using System.Collections.Generic;
using UnityEngine;

public class ProgramJob : ThreadedJob
{
    public string ProgramResponse;
    private string ProgramLanguage;

    private ProgramParser programparser;
    private ProgramService programservice;

    private List<Program> list_program;

    private Transform prefab_program;
    private Transform parent_program;


    //ObjectPool is a GameObject where non used Program item are stored
    //Those program item are recycled when needed
    //SimpleObjectPool script is responsable of the gameobject trafic between the scrolling list and the ObjectPool gameobject
    private SimpleObjectPool simple_object_pool;


    public ProgramJob()
    {
        programparser = new ProgramParser();
        programservice = new ProgramService();
    }


    public ProgramJob(string programresponse, Transform prefab, Transform parent,string language, GameObject objectpool)
    {
        ProgramResponse = programresponse;
        programparser = new ProgramParser();
        programservice = new ProgramService();
        prefab_program = prefab;
        parent_program = parent;
        ProgramLanguage = language;
        simple_object_pool = objectpool.GetComponent<SimpleObjectPool>() ;
    }


    //this is executed in parrallel to the Unity main thread
    protected override void ThreadFunction()
    {
        Initialization();
    }

    // This is executed by the Unity main thread when the job is finished
    protected override void OnFinished()
    {
        //Create a list of programs and put them in the scrolling list
        programservice.CreateListProgram(prefab_program, parent_program, list_program, ProgramLanguage, simple_object_pool);

    }


    //get JSONArray from the field called "data"
    private JSONObject GetData(JSONObject JsonResponse)
    {
        return JsonResponse.GetField("data");
    }


    private void Initialization()
    {
        //Turn string result into JSONObject
        JSONObject JsonResponse = new JSONObject(ProgramResponse);
        
        //Serialize the returned data into Program objects
        list_program = programparser.GetListObject(GetData(JsonResponse));
    }
}
