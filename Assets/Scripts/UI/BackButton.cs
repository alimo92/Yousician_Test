using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour {

    private Button back_Button;
    private PanelManager panelmanager;

	// Use this for initialization
	void Start () {
        back_Button = GetComponent<Button>();
        back_Button.onClick.AddListener(GoBack);

        panelmanager = GameObject.Find("SelectedProgram").GetComponent<PanelManager>();
        if (panelmanager == null)
        {
            Debug.Log("panelmanager not found");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GoBack()
    {
        panelmanager.state = false;
    }
}
