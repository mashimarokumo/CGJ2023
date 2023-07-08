using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum GroundType
{
    GRASS,

}
public class VibrateControler : MonoBehaviour
{
    public float vigorLimit;
    public float vigor;
    [SerializeField]
    private float resumeSpeed;
    [SerializeField]
    private float consumeSpeed;
    private bool isResuming = false;
    private bool isCreeping = false;

    GroundType curGroundType;
    public bool isEnemyApproching;
    public bool isBeansApproching;

    TimeLine IronPad1;
    private TimeLine IronPad2;
    private TimeLine woodPad;
    private TimeLine glass;
    private TimeLine dust1;
    private TimeLine dust2;

    //public float low;
    //public float high;

    // Start is called before the first frame update
    void Start()
    {
        vigor = vigorLimit;
        IronPad1 = CreateVibrate(3, new bool[] { true, false, false }, new float[] { 0.3f, 0.3f, 0.2f });

        IronPad2 = CreateVibrate(3, new bool[] { false, false, true }, new float[] {0.2f, 0.3f, 0.4f});
      
        woodPad = CreateVibrate(1, new bool[] { true }, new float[] { 0.3f }, true);

        glass = CreateVibrate(1, new bool[] { false }, new float[] { 0.1f });
        dust1 = CreateVibrate(2, new bool[] { false, true }, new float[] { 0.4f, 0.4f });
        dust2 = CreateVibrate(2, new bool[] { true, false }, new float[] { 0.4f, 0.4f });
    }

    // Update is called once per frame
    void Update()
    {
        Vibrate();
        TimeLineManager.instance.Loop(Time.deltaTime);
    }

    public void Vibrate()
    {
        if (Gamepad.current.rightTrigger.isPressed && !isResuming)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 6))
            {
                Debug.Log("Grass");
                //0.01 20 0.6
                GamepadVibrate(0.01f, 20f);
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
            else if (Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 7))
            {
                Debug.Log("Stone");
                GamepadVibrate(20f, 40);
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
            else if (Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 8))
            {
                if (isEnemyApproching)
                {
                    if (!IronPad2.m_isStart) IronPad2.Start();
                }
                else
                {
                    if (!IronPad1.m_isStart) IronPad1.Start();
                }
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
            else if(Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 9))
            {
                if (!woodPad.m_isStart) woodPad.Start();
               // GamepadVibrate(60f, 60);
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
            else if (Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 10))
            {
                if (!glass.m_isStart) glass.Start();
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
            else if (Physics2D.Raycast(transform.position, Vector2.down, 5, 1 << 11))
            {
                if (isBeansApproching)
                {
                    if (!dust1.m_isStart) dust1.Start();
                }
                else
                {
                    if (!dust2.m_isStart) dust2.Start();
                }
                vigor -= Time.deltaTime * consumeSpeed;
                isCreeping = true;
            }
        }
        else
        {
            Gamepad.current.PauseHaptics();
            isCreeping = false;
        }

        if (vigor < 0)
        {
            isResuming = true;
            vigor = 0;
        }
        else if (vigor < vigorLimit && !isCreeping)
        {
            vigor += Time.deltaTime * resumeSpeed;
        }
        else if(vigor > vigorLimit)
        {
            isResuming = false;
        }
    }

    public TimeLine CreateVibrate(int times, bool[] high, float[] interval, bool superHigh = false)
    {
        TimeLine timeLine = TimeLineManager.instance.CreateTimeLine();
        float time = 0;
        for (int i = 0; i < times; i++)
        {
            if (high[i])
            {
                if (superHigh)
                {
                    timeLine.AddEvent(time, 1, (a) =>
                    {
                        GamepadVibrate(60, 60);
                    });
                }
                else
                {
                    timeLine.AddEvent(time, 1, (a) =>
                    {
                        GamepadVibrate(20, 40);
                    });
                }
            }
            else
            {
                timeLine.AddEvent(time, 1, (a) =>
                {
                    GamepadVibrate(0.01f, 20);
                });
            }
            timeLine.AddEvent(time += interval[i], 2, (a) =>
            {
                Gamepad.current.PauseHaptics();
            });
            time += interval[i];
        }
        
        timeLine.AddEvent(time += interval[interval.Length-1], 3, (a) =>
        {
            timeLine.m_isStart = false;
        });
        return timeLine;
    }

    public void GamepadVibrate(float low, float high)
    {
        if (!isCreeping) return;
        //StartCoroutine(IEGamepadVibrate(low, high, time));
        Gamepad.current.SetMotorSpeeds(low, high);
        Gamepad.current.ResumeHaptics();
    }

    public IEnumerator IEGamepadVibrate(float low, float high, float time)
    {
        //防止因未连接手柄造成的 DebugError
        if (Gamepad.current == null)
            yield break;

        //设置手柄的 震动速度 以及 恢复震动 , 计时到达之后暂停震动
        Gamepad.current.SetMotorSpeeds(low, high);
        Gamepad.current.ResumeHaptics();
        var endTime = Time.time + time;

        while (Time.time < endTime)
        {
            Gamepad.current.ResumeHaptics();
            yield return null;
        }

        if (Gamepad.current == null)
            yield break;

        Gamepad.current.PauseHaptics();
    }
}
