using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PublicationEvent {

    public DateTime PublicationEventStartDate;
    public DateTime PublicationEventEndDate;
    public string PublicationEventDuration;
    public string PublicationEventId;
    public string PublicationEventTemporalStatus;
    public string PublicationEventType;
    public string PublicationEventRegion;


    public PublicationEvent()
    {

    }

    public override string ToString()
    {
        return "duration:"+PublicationEventDuration+"    " + "id:" + PublicationEventId + "    " +
            "temporalStatus:" + PublicationEventTemporalStatus + "    " + "region:" + PublicationEventRegion + "    " + "type:" + PublicationEventType+ "    " +
            "startTime:" + PublicationEventStartDate + "    "+ "endtime:" + PublicationEventEndDate;
    }
}
