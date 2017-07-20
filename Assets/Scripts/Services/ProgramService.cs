using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgramService  {

    private TitleService titleservice;

    public ProgramService()
    {
        titleservice = new TitleService();
    }

    public void CreateProgram(Transform prefab_programitem, Transform parent, Program program, string language)
    {
        AlterComponentProgram(prefab_programitem, program, language);
        var temp_object = GameObject.Instantiate(prefab_programitem);
        temp_object.name = titleservice.GetTitleByLangue(program, language)+" ("+program.ProgramTypeMedia+")" ;
        temp_object.parent = parent.transform;
    }


    public void CreateListProgram(Transform prefab_programitem, Transform parent, List<Program> list_program, string language)
    {
        for(int i = 0; i < list_program.Count; i++)
        {
            CreateProgram(prefab_programitem, parent, list_program[i], language);
        }
    }



    private void AlterComponentProgram(Transform prefab_programitem, Program program, string language)
    {
        prefab_programitem.GetChild(0).GetComponent<Text>().text= titleservice.GetTitleByLangue(program, language);
    }

}
