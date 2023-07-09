using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 技能 : MonoBehaviour
{
    private Vector3 recordTransform;
    public int num = 1;
    [Header("静止隐身持续时间")]
    public float consistTime;
    public float nowTime;
    private void Update()
    {
        nowTime += Time.deltaTime;
        if(nowTime<= consistTime)
        {
            gameObject.layer = 1;
        }
        else
        {
            gameObject.layer = 3;
        }
    }
    public void RecordTransfom()
    {
        recordTransform = gameObject.transform.position;
        num = 2;
    }
    public void Transport()
    {
        gameObject.transform.position  = recordTransform ;
        num = 1;
    }
    public void Skill2()
    {

    }
   public void Skill3()
    {
        nowTime = 0;
    }
}
