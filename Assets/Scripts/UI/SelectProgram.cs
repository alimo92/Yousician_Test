using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectProgram : MonoBehaviour {

    private Button button;

    private ProgramParser programparser;
    private Program program;

    private GameObject SelectedProgramObject;
    private PanelManager panelmanager;
    private PopulateProgram populateprogram;


    // Use this for initialization
    void Start () {
        InitComponent();


        
        


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitComponent()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);


        SelectedProgramObject = GameObject.Find("SelectedProgram");
        if (SelectedProgramObject == null)
        {
            Debug.Log("SelectedProgramObject not found");
        }

        panelmanager = SelectedProgramObject.GetComponent<PanelManager>();
        if (panelmanager == null)
        {
            Debug.Log("PanelManager not found");
        }

    }


    private void Select()
    {
        program = GetComponent<Program>();
        panelmanager.state = true;
        AlterProgramComponent(SelectedProgramObject, program);
        SelectedProgramObject.gameObject.SetActive(true);
    }


    private void AlterProgramComponent(GameObject SelectedProgramObject, Program program)
    {
        SelectedProgramObject.GetComponent<Program>().ProgramId = program.ProgramId;
        SelectedProgramObject.GetComponent<Program>().ProgramImageId = program.ProgramImageId;
        SelectedProgramObject.GetComponent<Program>().ProgramListDescription = program.ProgramListDescription;
        SelectedProgramObject.GetComponent<Program>().ProgramListPublicationEvent = program.ProgramListPublicationEvent;
        SelectedProgramObject.GetComponent<Program>().ProgramListTile = program.ProgramListTile;
        SelectedProgramObject.GetComponent<Program>().ProgramType = program.ProgramType;
        SelectedProgramObject.GetComponent<Program>().ProgramTypeMedia = program.ProgramTypeMedia;
    }
}
