using System.Collections.Generic;
using UnityEngine;

public class ProgramJob : ThreadedJob
{
    public string ProgramResponse;

    private ProgramParser programparser;
    private ProgramService programservice;

    private List<Program> list_program;

    private Transform prefab_programitem;
    private Transform parent_programitems;

    public ProgramJob()
    {
        programparser = new ProgramParser();
        programservice = new ProgramService();
    }


    public ProgramJob(string programresponse, Transform prefab, Transform parent)
    {
        ProgramResponse = programresponse;
        programparser = new ProgramParser();
        programservice = new ProgramService();
        prefab_programitem = prefab;
        parent_programitems = parent;
    }


    //this is executed by in parrallel to the Unity main thread
    protected override void ThreadFunction()
    {
        Initialization();
    }

    // This is executed by the Unity main thread when the job is finished
    protected override void OnFinished()
    {
        //programservice.CreateProgram(prefab_programitem, parent_programitems, list_program[0], "fi");
        programservice.CreateListProgram(prefab_programitem, parent_programitems, list_program, "fi");

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
