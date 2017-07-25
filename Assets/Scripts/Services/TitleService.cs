using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleService  {

    public TitleService()
    {

    }

    //Get ProgramTitle based on language
    //When language is not found, it returns the first value from the title list with a small indication about the title language
    //example : "(sv) Swedish Title"
    public string GetTitleByLangue(Program program, string language)
    {
        for(int i = 0; i < program.ProgramListTile.Count; i++)
        {
            if (program.ProgramListTile[i].TitleLanguage == language)
            {
                return program.ProgramListTile[i].TitleText;
            }
        }
        return "("+program.ProgramListTile[0].TitleLanguage+")"+" "+ program.ProgramListTile[0].TitleText;
    }

}
