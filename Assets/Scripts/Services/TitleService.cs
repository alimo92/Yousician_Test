using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleService  {

    public TitleService()
    {

    }

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
