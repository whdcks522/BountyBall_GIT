using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebilEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public GameObject player;
    SpriteRenderer spriterenderer;
    Rigidbody2D rigid;
    Animator anim;
    int move;
    int reload;
    bool ready;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        reload = 3;
        move = 0;
        ready = true;
        if (gameObject.activeSelf) Invoke("Action", 2.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Action()
    {
        anim.SetTrigger("Action");
        Invoke("Attack",1.0f);
    }
    void Attack()
    {
        GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
        Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
        Bullet niddleLogic = niddle.GetComponent<Bullet>();
        niddleLogic.gamemanager = gamemanager;
        niddle.transform.position = new Vector2(transform.position.x, transform.position.y);
        Vector2 dirvec = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        niddleRigid.velocity = dirvec.normalized * 8.0f;
        float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
        niddle.transform.Rotate(rotVec);
        --reload;
        if (reload >= 1) Invoke("Attack", 0.1f);
        else if (reload <= 0)
        {
            reload = 3;
            Invoke("Action", 2.0f);
        }
    }

    void Update()
    {
        //좌우 방향 조정
        if (player.transform.position.x < transform.position.x) spriterenderer.flipX = true;
        else if (player.transform.position.x >= transform.position.x) spriterenderer.flipX = false;
        //벡터 출력
        switch (move)
        {
            case 0:
                rigid.velocity = Vector2.right;
                break;
            case 1:
                rigid.velocity = Vector2.down;
                break;
            case 2:
                rigid.velocity = Vector2.left;
                break;
            case 3:
                rigid.velocity = Vector2.up;
                break;
        }
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y) * 5.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.gameObject.layer == 12) || (collision.gameObject.layer == 14)) &&(ready))//벽에 충돌시
        {
            //위치 조정
            switch (move)
            {
                case 0://우측 충돌
                    transform.position = new Vector2(9-(5-transform.position.y), 5.5f);
                    break;
                case 1://하측 충돌
                    transform.position = new Vector2(9.5f, -5+(9-transform.position.x));
                    break;
                case 2://좌측 충돌
                    transform.position = new Vector2(-9-(-5-transform.position.y), -5.5f);
                    break;
                case 3://상측 충돌
                    transform.position = new Vector2(-9.5f, 5+(-9-transform.position.x));
                    break;
            }
            //불값 조정
            ready = false;
            Invoke("breakReady", 0.1f);
            //수치 조정
            move += 1;
            if (move == 4) move = 0;
        }
    }
    void breakReady() { ready = true;}
}
