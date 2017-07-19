using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PublicationEventParser  {

    private Tools tool;

    public PublicationEventParser()
    {
        tool = new Tools();
    }


    public JSONObject GetJson(PublicationEvent publicationevent)
    {
        JSONObject json = new JSONObject();

        json.AddField("id", publicationevent.PublicationEventId);
        json.AddField("region", publicationevent.PublicationEventRegion);
        json.AddField("duration", publicationevent.PublicationEventDuration);
        json.AddField("temporalStatus", publicationevent.PublicationEventTemporalStatus);
        json.AddField("type", publicationevent.PublicationEventType);
        json.AddField("endTime", publicationevent.PublicationEventEndDate.ToString());
        json.AddField("sartTime", publicationevent.PublicationEventStartDate.ToString());

        return json;
    }


    public PublicationEvent GetObject(JSONObject json)
    {
        string temp_date;
        PublicationEvent publicationevent = new PublicationEvent();

        publicationevent.PublicationEventDuration = tool.RemoveFirstLastCharacter(json.GetField("duration").Print());
        publicationevent.PublicationEventId  = tool.RemoveFirstLastCharacter(json.GetField("id").Print());
        publicationevent.PublicationEventRegion = tool.RemoveFirstLastCharacter(json.GetField("region").Print());
        publicationevent.PublicationEventTemporalStatus = tool.RemoveFirstLastCharacter(json.GetField("temporalStatus").Print());
        publicationevent.PublicationEventType = tool.RemoveFirstLastCharacter(json.GetField("type").Print());

        temp_date = tool.RemoveFirstLastCharacter(json.GetField("startTime").Print());
        publicationevent.PublicationEventStartDate = Convert.ToDateTime(temp_date);

        temp_date = tool.RemoveFirstLastCharacter(json.GetField("endTime").Print());
        publicationevent.PublicationEventEndDate = Convert.ToDateTime(temp_date);


        return publicationevent;
    }


    public JSONObject GetJson(List<PublicationEvent> list_publicationevent)
    {
        JSONObject jsonobject = new JSONObject(JSONObject.Type.ARRAY);
        JSONObject temp_json;

        for (int i = 0; i < list_publicationevent.Count; i++)
        {
            temp_json = GetJson(list_publicationevent[i]);
            jsonobject.Add(temp_json);
        }

        return jsonobject;
    }


    public List<PublicationEvent> getListObject(JSONObject jsonarray)
    {
        List<PublicationEvent> list_publicationevent = new List<PublicationEvent>();
        JSONObject temp_json;

        for (int i = 0; i < jsonarray.Count; i++)
        {
            temp_json = jsonarray[i];
            list_publicationevent.Add(GetObject(temp_json));
        }

        return list_publicationevent;
    }
}
