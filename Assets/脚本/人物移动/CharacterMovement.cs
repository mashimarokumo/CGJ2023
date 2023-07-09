using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class CharacterMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    private 技能 skill;

    public static List<float> cds = new List<float>() { 5, 1, 2 };
    public float resumeTime1;
    public float resumeTime2;
    public float resumeTime3;
    public int curIndex = 1;

    public Animator animator;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        skill = GetComponent<技能>();
    }


    void Update()
    {
        Movement();

        if(resumeTime1 > 0)
        {
            resumeTime1 -= Time.deltaTime;
            UIManager.Instance.Resume(1, resumeTime1);
        }
        if (resumeTime2 > 0)
        {
            resumeTime2 -= Time.deltaTime;
            UIManager.Instance.Resume(2, resumeTime2);
        }
        if (resumeTime3 > 0)
        {
            resumeTime3 -= Time.deltaTime;
            UIManager.Instance.Resume(3, resumeTime3);
        }

    }


    void Movement()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
            animator.SetBool("isRunning", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("isRunning", false);

        }

        //if (Input.GetKeyDown(KeyCode.JoystickButton5))
        //{
        //    if (curIndex + 1 > 3)
        //    {
        //        curIndex = 1;
        //    }
        //    else
        //    {
        //        curIndex++;
        //    }

        //    //UIManager.Instance.SwitchSkill(false);
        //    Debug.Log(curIndex);
        //}
        //else if (Input.GetKeyDown(KeyCode.JoystickButton4))
        //{
        //    if (curIndex - 1 < 1)
        //        curIndex = 3;
        //    else
        //        curIndex--;
        //    //UIManager.Instance.SwitchSkill(true);

        //    Debug.Log(curIndex);
        //}

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            switch (curIndex)
            {
                case 1:
                    if (resumeTime1 > 0) break;
                    switch (skill.num)
                    {
                        case 1:
                            skill.RecordTransfom();
                            break;
                        case 2:
                            skill.Transport();
                            resumeTime1 = cds[0];
                            UIManager.Instance.StartResume(1);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    if (resumeTime2 > 0) break;
                    skill.Skill2();
                    resumeTime2 = cds[1];
                    UIManager.Instance.StartResume(2);

                    break;
                case 3:
                    if (resumeTime3 > 0) break;
                    skill.Skill3();
                    resumeTime3 = cds[2];
                    UIManager.Instance.StartResume(3);

                    break;
                default: break;
            }
        }



    }

    bool stairs;
    private void OnCollisionStay2D(Collision2D collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        foreach (var c in collision.contacts)
        {
            if (c.collider.gameObject.layer == 12)
            {
                stairs = true;
                collision.collider.isTrigger = false;
                float angle = Vector3.Angle(normal, Vector3.up);
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.x);

                animator.SetBool("isUping", true);

                if (Input.GetAxis("Vertical") < -0.1f)
                {
                    collision.collider.isTrigger = true;
                }

                return;
            }
        }

        animator.SetBool("isUping", false);
        stairs = false;
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (!stairs)
    //    {
    //        if (collision.gameObject.layer == 12)
    //        {
    //            Debug.Log("exit");

    //            collision.collider.isTrigger = true; 
    //        }
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            collision.isTrigger = false;
        }
    }

}
