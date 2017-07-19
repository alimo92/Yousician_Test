using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleParser  {

    private Tools tool;


    public TitleParser()
    {
        tool = new Tools();
    }

    //Returns a JSONObject based on a list of title
    //the JSONObject can have multiple fields, one for each available language
    public JSONObject GetJson(List<Title> list_title)
    {
        JSONObject json = new JSONObject();

        for (int i = 0; i < list_title.Count; i++)
        {
            json.AddField(list_title[i].TitleLanguage, list_title[i].TitleText);
        }

        return json;
    }

    //Returns a list of Title based on a JSONObject
    //Each field represents a title object in the list
    public List<Title> GetListObject(JSONObject json)
    {
        List<Title> list_title = new List<Title>();
        Title title;

        for (int i = 0; i < json.keys.Count; i++)
        {
            title = new Title();
            title.TitleLanguage = json.keys[i];
            title.TitleText = tool.RemoveFirstLastCharacter(json.GetField(title.TitleLanguage).Print());
            list_title.Add(title);
        }

        return list_title;
    }

}
