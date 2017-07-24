using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateProgram : MonoBehaviour {

    private GameObject SelectedProgram;
    private Program programselected;
    private ProgramParser programparser;
    private PanelManager panelmanager;
    private ProgramService programservice;

    public bool flag = false;

    // Use this for initialization
    void Start () {
        programparser = new ProgramParser();
        InitComponent();
    }
	
	// Update is called once per frame
	void Update () {

        if (panelmanager.state && programselected.ProgramId!= GetComponent<Program>().ProgramId)
        {
            PopulateProgramComponent(programselected);
            programservice.SetDetailProgramScreen(transform.gameObject, programselected,"fi");
        }
		
	}


    private void InitComponent()
    {
        SelectedProgram = GameObject.Find("SelectedProgram");
        if (SelectedProgram == null)
        {
            Debug.Log("SelectedProgram not found");
        }
        programselected = SelectedProgram.GetComponent<Program>();
        if (programselected==null)
        {
            Debug.Log("ProgramSelected not found");
        }
        panelmanager = SelectedProgram.GetComponent<PanelManager>();
        if (panelmanager == null)
        {
            Debug.Log("PanelManager not found");
        }

        programservice = new ProgramService();
    }


    private void PopulateProgramComponent(Program program)
    {
        GetComponent<Program>().ProgramId = program.ProgramId;
        GetComponent<Program>().ProgramImageId = program.ProgramImageId;
        GetComponent<Program>().ProgramListDescription = program.ProgramListDescription;
        GetComponent<Program>().ProgramListPublicationEvent = program.ProgramListPublicationEvent;
        GetComponent<Program>().ProgramListTile = program.ProgramListTile;
        GetComponent<Program>().ProgramType = program.ProgramType;
        GetComponent<Program>().ProgramTypeMedia = program.ProgramTypeMedia;
    } 
}
