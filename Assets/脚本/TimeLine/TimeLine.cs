using System;

public class TimeLine
{
    public float TimeSpeed { get; set; }

    public bool m_isStart;
    private bool m_isPause;
    private float m_curTime;
    private Action m_reset;
    private Action<float> m_upadte;
    public TimeLine()
    {
        TimeSpeed = 1;

        Reset();
    }

    public void AddEvent(float delay, int id, Action<int> method)
    {
        LineEvent param = new LineEvent(delay, id, method);
        m_upadte += param.Invoke;
        m_reset += param.Reset;
    }
    public void Start()
    {
        Reset();

        m_isStart = true;
        m_isPause = false;
    }
    public void Pause()
    {
        m_isPause = true;
    }
    public void Resume()
    {
        m_isPause = false;
    }
    public void Reset()
    {
        m_curTime = 0;
        m_isStart = false;
        m_isPause = false;

        if(null != m_reset)
        {
            m_reset();
        }
    }
    public void Loop(float deltaTime)
    {
        if(!m_isStart || m_isPause)
        {
            return;
        }
        m_curTime += deltaTime;
        if(null != m_upadte)
        {
            m_upadte(m_curTime);
        }
    }

    private class LineEvent
    {
        public float Delay { get; protected set; }
        public int Id { get; protected set; }
        public Action<int> Method { get; protected set; }

        private bool m_isInvoke = false;
        public LineEvent(float delay, int id, Action<int> method)
        {
            Delay = delay;
            Id = id;
            Method = method;

            Reset();
        }
        public void Reset()
        {
            m_isInvoke = false;
        }
        public void Invoke(float time)
        {
            if(time < Delay)
            {
                return;
            }
            if(!m_isInvoke && null != Method)
            {
                m_isInvoke = true;
                Method(Id);
            }
        }

    }
}
