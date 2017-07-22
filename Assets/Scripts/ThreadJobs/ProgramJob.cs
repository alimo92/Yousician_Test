using System.Collections.Generic;
using UnityEngine;

public class ProgramJob : ThreadedJob
{
    public string ProgramResponse;

    private ProgramParser programparser;
    private ProgramService programservice;

    private List<Program> list_program;

    private Transform prefab_program;
    private Transform parent_program;

    private SimpleObjectPool simple_object_pool;


    public ProgramJob()
    {
        programparser = new ProgramParser();
        programservice = new ProgramService();
    }


    public ProgramJob(string programresponse, Transform prefab, Transform parent, GameObject objectpool)
    {
        ProgramResponse = programresponse;
        programparser = new ProgramParser();
        programservice = new ProgramService();
        prefab_program = prefab;
        parent_program = parent;
        simple_object_pool = objectpool.GetComponent<SimpleObjectPool>() ;
    }


    //this is executed by in parrallel to the Unity main thread
    protected override void ThreadFunction()
    {
        Initialization();
    }

    // This is executed by the Unity main thread when the job is finished
    protected override void OnFinished()
    {
        //programservice.CreateListProgram(prefab_programitem, parent_programitems, list_program, "fi");

        programservice.CreateListProgram(prefab_program, parent_program, list_program, "fi", simple_object_pool);

        Debug.Log(list_program.Count); 
    }


    private JSONObject GetData(JSONObject JsonResponse)
    {
        return JsonResponse.GetField("data");
    }


    private void Initialization()
    {
        JSONObject JsonResponse = new JSONObject(ProgramResponse);
        list_program = programparser.GetListObject(GetData(JsonResponse));


    }
}
