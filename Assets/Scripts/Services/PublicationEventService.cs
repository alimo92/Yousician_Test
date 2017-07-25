using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PublicationEventService {

    private string no_date="---";

	public PublicationEventService()
    {

    }

    public PublicationEvent GetPublication(Program program, string Type)
    {
        List<PublicationEvent> listpublicationevent = program.ProgramListPublicationEvent;
        List<TimeSpan> list_delta_time = GetListDelta_TimeSpan(program);
        TimeSpan delta_time;

        if (Type == "Next")
        {
            delta_time = GetNearestTimeSpan(list_delta_time);
            return GetPublicationEventByTimeSpan(list_delta_time, delta_time, listpublicationevent);
        }
        else if (Type == "Previous")
        {
            delta_time = GetPreviousTimeSpan(list_delta_time);
            return GetPublicationEventByTimeSpan(list_delta_time, delta_time, listpublicationevent);
        }

        return null;

    }


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


    public TimeSpan GetDurationPublicationEvent(PublicationEvent publicationevent)
    {
        TimeSpan timespan = publicationevent.PublicationEventEndDate.Subtract(publicationevent.PublicationEventStartDate);
        return timespan;
    }
}
