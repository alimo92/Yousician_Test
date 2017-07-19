using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionParser {

    private Tools tool;

    public DescriptionParser()
    {
        tool = new Tools();
    }


    public JSONObject GetJson(List<Description> list_description)
    {
        JSONObject json = new JSONObject();

        for(int i = 0; i < list_description.Count; i++)
        {
            json.AddField(list_description[i].DescriptionLanguage, list_description[i].DescriptionText);
        }

        return json;
    }


    public List<Description> GetListObject(JSONObject json)
    {
        List<Description> list_description = new List<Description>();
        Description description;

        for(int i = 0; i < json.keys.Count; i++)
        {
            description = new Description();
            description.DescriptionLanguage = json.keys[i];
            description.DescriptionText = tool.RemoveFirstLastCharacter(json.GetField(description.DescriptionLanguage).Print());
            list_description.Add(description);
        }

        return list_description;
    }
}
