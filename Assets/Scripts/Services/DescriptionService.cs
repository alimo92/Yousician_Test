using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionService {


    public DescriptionService()
    {

    }

    public string GetDescriptionByLanguage(Program program, string language)
    {
        for(int i = 0; i < program.ProgramListDescription.Count; i++)
        {
            if (program.ProgramListDescription[i].DescriptionLanguage == language)
            {
                return program.ProgramListDescription[i].DescriptionText;
            }
        }

        return "";
    }

}
