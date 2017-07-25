using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionService {


    public DescriptionService()
    {

    }

    //Returns a language description based on a program
    //language parameter only accept "fi" for finnish and "sv" for swedish
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
