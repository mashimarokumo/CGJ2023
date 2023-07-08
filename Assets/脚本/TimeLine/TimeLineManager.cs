using System.Collections.Generic;
using System;

public class TimeLineManager : Singleton<TimeLineManager>
{
    List<TimeLine> timeLines = new List<TimeLine>();


    //public void Init()
    //{
    //    for (int i = 0; i < timeLines.Count; i++)
    //    {
    //        timeLines[i] = CreateTimeLine();
    //    }
        
    //}

    public TimeLine CreateTimeLine()
    {
        TimeLine timeLine = new TimeLine();
        timeLines.Add(timeLine);
        return timeLine;
    }

    public void Loop(float deltaTime)
    {
        for (int i = 0; i < timeLines.Count; i++)
        {
            timeLines[i].Loop(deltaTime);
        }
    }

    public void StopAllTimeLines()
    {
        for (int i = 0; i < timeLines.Count; i++)
        {
            timeLines[i].Pause();
        }
    }
}
