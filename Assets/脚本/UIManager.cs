using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public List<RectTransform> resumeImages;

    internal void Resume(int skill, float resumeTime)
    {
        
        resumeImages[skill-1].localScale = new Vector3(1, resumeTime / CharacterMovement.cds[skill-1], 1);
    }

    public void StartResume(int skill)
    {
        resumeImages[skill - 1].localScale = new Vector3(1, 1, 1);
    }

    //public void SwitchSkill(bool left)
    //{
    //    if(left)
    //    {
    //        for (int i = 0; i < curPos.Count; i++)
    //        {
    //            //if (curPos[i] < 0) curPos[i] = 2;
    //            //skillImages[i].parent = points[curPos[i]];
    //            if (curPos[i] == 0)
    //            {
    //                skillImages[i].position += new Vector3(120, 0, 0);
    //            }
    //            else
    //            {
    //                skillImages[i].position -= new Vector3(60, 0, 0);
    //            }

    //            curPos[i]--;
    //            if (curPos[i] < 0) curPos[i] = 2;

    //            if (curPos[i] == 1)
    //            {
    //                skillImages[i].SetAsLastSibling();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < curPos.Count; i++)
    //        {
    //            if (curPos[i] == 2)
    //            {
    //                skillImages[i].position -= new Vector3(120, 0, 0);
    //            }
    //            else
    //            {
    //                skillImages[i].position += new Vector3(60, 0, 0);
    //            }

    //            curPos[i]++;
    //            if (curPos[i] > 2) curPos[i] = 0;

    //            if (curPos[i] == 1)
    //            {
    //                skillImages[i].SetAsLastSibling();
    //            }
    //        }

    //    }
   // }

    private void Awake()
    {
        Instance = this;
    }


}