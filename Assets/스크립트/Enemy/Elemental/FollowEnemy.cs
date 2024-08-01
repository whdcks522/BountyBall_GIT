using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public bool ready2;
    public bool ready;
    public GameObject player;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ready2 = true;
        ready = true;
    }

    void Update()
    {
        if ((ready)&&(ready2))
            rigid.velocity = (player.transform.position - transform.position).normalized * 2;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14)||(collision.gameObject.layer == 12))//벽들에 충돌시
        {
            rigid.velocity = Vector2.zero;
            ready = false;
        }

    }
}
