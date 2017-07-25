using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PublicationEventService {

    //value to show when no publication found
    private string no_date="---";

	public PublicationEventService()
    {

    }

    //Returns a PublicationEvent from a program based on a Type
    //Type only accepts "Next" for the nearest next publication and "Previous" for the last publication 
    public PublicationEvent GetPublication(Program program, string Type)
    {

        List<PublicationEvent> listpublicationevent = program.ProgramListPublicationEvent; // get list of publication events
        List<TimeSpan> list_delta_time = GetListDelta_TimeSpan(program); // get a list of timespans between NOW and the starting date of the publication events list 
        TimeSpan delta_time;

        if (Type == "Next")
        {
            delta_time = GetNearestTimeSpan(list_delta_time);
            return GetPublicationEventByTimeSpan(list_delta_time, delta_time, listpublicationevent); // Returns next publication events
        }
        else if (Type == "Previous")
        {
            delta_time = GetPreviousTimeSpan(list_delta_time);
            return GetPublicationEventByTimeSpan(list_delta_time, delta_time, listpublicationevent); // Returns previous publication events
        }

        return null;

    }


    //Returns a proper string based on the publication event to show the startingdate
    //This returns a no_data string when the publication is not found
    public string GetStringPublicationEvent(PublicationEvent publicationevent, string Type)
    {

        if (Type == "Next")
        {
            if (publicationevent.PublicationEventStartDate == default(DateTime))
            {
                return no_date;
            }
            else
            {
                return publicationevent.PublicationEventStartDate+"";
            }
        }else if (Type == "Previous")
        {
            if (publicationevent.PublicationEventStartDate == default(DateTime))
            {
                return no_date;
            }
            else
            {
                return publicationevent.PublicationEventStartDate + "";
            }
        }
        return null;
    }


    //Returns a list of timespans between NOW and starting date of the publication events
    private List<TimeSpan> GetListDelta_TimeSpan(Program program)
    {
        List<PublicationEvent> listpublicationevent = program.ProgramListPublicationEvent;
        List<TimeSpan> list_delta_time = new List<TimeSpan>();
        DateTime Now = DateTime.Now;
        TimeSpan delta_time;


        for (int i = 0; i < listpublicationevent.Count; i++)
        {
            delta_time = Now.Subtract(listpublicationevent[i].PublicationEventStartDate);
            list_delta_time.Add(delta_time);        }

        return list_delta_time;
    }


    //Get the nearest time span from a list of time spans
    //The nearest timespan would be the highest value from the list under 0 (timespan <0)
    private TimeSpan GetNearestTimeSpan(List<TimeSpan> list_delta_time)
    {
        TimeSpan delta_time = list_delta_time[0];

        delta_time = list_delta_time[0];

        for (int i = 1; i < list_delta_time.Count; i++)
        {
            if (list_delta_time[i] > delta_time && list_delta_time[i] < TimeSpan.Zero)
            {
                delta_time = list_delta_time[i];
            }
        }
        if (delta_time > TimeSpan.Zero)
        {
            return TimeSpan.Zero;
        }
        return delta_time;
    }

    //Get last time span from a list of time spans
    //The last timespan would be a the lowest value from the list superior to 0 (timespan >0)
    private TimeSpan GetPreviousTimeSpan(List<TimeSpan> list_delta_time)
    {
        TimeSpan delta_time = list_delta_time[0];

        delta_time = list_delta_time[0];

        for (int i = 1; i < list_delta_time.Count; i++)
        {
            if (list_delta_time[i] < delta_time && list_delta_time[i] > TimeSpan.Zero)
            {
                delta_time = list_delta_time[i];
            }
        }
        if (delta_time < TimeSpan.Zero)
        {
            return TimeSpan.Zero;
        }
        return delta_time;
    }


    //Returns the publication event linked to the timespan
    private PublicationEvent GetPublicationEventByTimeSpan(List<TimeSpan> list_delta_time, TimeSpan delta_time, List<PublicationEvent> listpublicationevent)
    {
        if (delta_time != TimeSpan.Zero)
        {
            for (int i = 0; i < listpublicationevent.Count; i++)
            {
                if (delta_time == list_delta_time[i])
                {
                    return listpublicationevent[i];
                }
            }
        }

        return new PublicationEvent();

    }


    //Get the duration of an EventPublication
    public TimeSpan GetDurationPublicationEvent(PublicationEvent publicationevent)
    {
        TimeSpan timespan = publicationevent.PublicationEventEndDate.Subtract(publicationevent.PublicationEventStartDate);
        return timespan;
    }
}
