using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcelEnemy : MonoBehaviour
{
    bool ready;
    public GameObject player;
    Rigidbody2D rigid;
    float time;
    Vector3 playerPos;
    Vector3 onePos;

    private void OnEnable()
    {
        ready = false;
        time = 0.0f;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if ((time >= 0.0f) && (time < 2.0f) && (ready))
        {
            rigid.velocity = new Vector2(0,0);
        }
        else if ((time >= 2.0f) && (time < 3.0f) && (!ready))
        {
            ready = true;
            playerPos = player.transform.position;
            onePos = transform.position;
            rigid.velocity = (playerPos - onePos).normalized * 10.0f;
        }
        else if(time >= 3.0f)
        {
            time = 0.0f;
            ready = false; rigid.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14)|| (collision.gameObject.layer == 12))//환상벽에 충돌시
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
