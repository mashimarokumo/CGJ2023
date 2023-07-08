using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
   
    private bool isFindPlayer = false;
    private Rigidbody2D rb;
    private Vector2 finalPosition;
    private Collider2D[] coll = new Collider2D[1];
    [Header("Ñ²ÂßÎ»ÖÃ")]
    public GameObject pos1, pos2;
    [Header("ËÑË÷·¶Î§")]    
    public float findRange;
    [Header("ËÙ¶È")]
    public float speed;
    public LayerMask playerLayer;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(FindWay(pos1));
        rb.velocity = FindWay(pos1) * speed;
    }

  
    void Update()
    {
        FindCharacter();
        NormalMove();
       
    }
    void FindCharacter()
    {
        if(Physics2D.OverlapCircleNonAlloc(transform.position,findRange,coll,playerLayer)==0&&!isFindPlayer)
        {
            Debug.Log(77);       
        }
        else if(isFindPlayer)
        {
            Debug.Log(89);
            if(Mathf.Abs(gameObject.transform.position.x - finalPosition.x) + Mathf.Abs(gameObject.transform.position.x - finalPosition.x)<=0.3)
            {
                rb.velocity = FindWay(pos1) * speed;
                isFindPlayer = false;
            }
        }
        else if(!isFindPlayer)
        {

            Debug.Log(5);
            finalPosition = coll[0].gameObject.transform.position;
            isFindPlayer = true;
            rb.velocity = FindWay(coll[0].gameObject) * speed;
        }
    }
    void NormalMove()
    {
        float pos1Distance = Mathf.Abs(gameObject.transform.position.x - pos1.transform.position.x) + Mathf.Abs(gameObject.transform.position.x - pos1.transform.position.x);
        float pos2Distance = Mathf.Abs(gameObject.transform.position.x - pos2.transform.position.x) + Mathf.Abs(gameObject.transform.position.x - pos2.transform.position.x);
        if (!isFindPlayer)
        {
            if(pos1Distance<=0.3)
            {
                Debug.Log(1);
                rb.velocity = FindWay(pos2) * speed;

            }
            else if(pos2Distance <= 0.3)
            {
                Debug.Log(2);
                rb.velocity = FindWay(pos1) * speed;
            }
        }
    }
    public Vector2 FindWay(GameObject a)
    {
        Vector2 finalWay = new Vector2(a.transform.position.x - gameObject.transform.position.x, a.transform.position.y - gameObject.transform.position.y).normalized;
        return finalWay;
    }
}
