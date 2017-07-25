using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramParser  {

    private Tools tool;
    private DescriptionParser descriptionparser;
    private PublicationEventParser publicationeventparser;
    private TitleParser titleparser;


    public ProgramParser()
    {
        tool = new Tools();
        descriptionparser = new DescriptionParser();
        publicationeventparser = new PublicationEventParser();
        titleparser = new TitleParser();
    }

    //Retruns a JSONObject of the Program Object
    public JSONObject GetJson(Program program)
    {
        JSONObject json = new JSONObject();

        json.AddField("id", program.ProgramId);
        json.AddField("image", program.ProgramImageId);
        json.AddField("type",program.ProgramType);
        json.AddField("typeMedia", program.ProgramTypeMedia);
        json.AddField("title", titleparser.GetJson(program.ProgramListTile));
        json.AddField("description", descriptionparser.GetJson(program.ProgramListDescription));
        json.AddField("publicationEvent", publicationeventparser.GetJson(program.ProgramListPublicationEvent));


        return json;
    }


    //Returns a Program Object based on a JSONObject
    public Program GetObject(JSONObject json)
    {
        Program program = new Program();

        program.ProgramId = tool.RemoveFirstLastCharacter(json.GetField("id").Print());
        program.ProgramType = tool.RemoveFirstLastCharacter(json.GetField("type").Print());
        program.ProgramTypeMedia = tool.RemoveFirstLastCharacter(json.GetField("typeMedia").Print());


        //check if "image" is not empty
        if (json.GetField("image").keys.Count != 0)
        {
            program.ProgramImageId = tool.RemoveFirstLastCharacter(json.GetField("image").GetField("id").Print());
        }
        else
        {
            program.ProgramImageId = "not available";
        }

        program.ProgramListTile = titleparser.GetListObject(json.GetField("title"));
        program.ProgramListDescription = descriptionparser.GetListObject(json.GetField("description"));
        program.ProgramListPublicationEvent = publicationeventparser.GetListObject(json.GetField("publicationEvent"));

        return program;
    }


    //Returns a JSONArray based on a list of programs
    //Each JSONObject from the JSONArray represents a Program 
    public JSONObject GetJson(List<Program> list_program)
    {
        JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject temp_json;

        for (int i = 0; i < list_program.Count; i++)
        {
            temp_json = GetJson(list_program[i]);
            jsonobject.Add(temp_json);
        }

        return jsonobject;
    }


    //Returns a list of Progaram Objects based on a JSONArray
    //Each program from the list represents a JSONObject from the JSONArray
    public List<Program> GetListObject(JSONObject jsonarray)
    {
        List<Program> list_program = new List<Program>();
        JSONObject temp_json;

        for (int i = 0; i < jsonarray.Count; i++)
        {
            temp_json = jsonarray[i];
            list_program.Add(GetObject(temp_json));
        }

        return list_program;
    }

}
