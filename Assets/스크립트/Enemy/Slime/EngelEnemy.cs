using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public GameObject player;
    SpriteRenderer spriterenderer;
    Rigidbody2D rigid;
    int move;
    void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rigid.velocity = Vector2.left * 3;
        move = 0;
        Invoke("Attack", 2.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Attack() { 
        for (int i = -1; i <= 1; i += 2) {
            GameObject bullet = objectmanager.MakeObj("EnemyInjection");
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            bullet.transform.position = transform.position;
            if (i == -1) {
                if (move == 0 || move == 2)
                {
                    bullet.transform.position += Vector3.left;
                }
                else bullet.transform.position += Vector3.up;
            }
            else {
                if (move == 0 || move == 2)
                {
                    bullet.transform.position += Vector3.right;
                }
                else bullet.transform.position += Vector3.down;
            }
            Vector2 dirvec = (Vector2)(player.transform.position - bullet.transform.position);
            bulletRigid.velocity = dirvec.normalized * 8.0f;
            float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            bullet.transform.Rotate(rotVec);
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            bulletLogic.host = gameObject;
            CancelInvoke();
            Invoke("Attack", 2.0f);
        }
    }

    void Update()
    {
        //좌우 방향 조정
        if (player.transform.position.x < transform.position.x) spriterenderer.flipX = true;
        else if (player.transform.position.x >= transform.position.x) spriterenderer.flipX = false;
        //벡터 출력
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12) {
            if ((collision.gameObject.name.Contains("Left")) && move == 0) {
                Telepote(move);
            }
            else if ((collision.gameObject.name.Contains("Bottom")) && move == 1) {
                Telepote(move);
            }
            else if ((collision.gameObject.name.Contains("Right")) && move == 2)
            {
                Telepote(move);
            }
            else if ((collision.gameObject.name.Contains("Top")) && move == 3)
            {
                Telepote(move);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
         if (collision.gameObject.layer == 14)//벽에 충돌시
         {
            //위치 조정
            Telepote(move);
         }
    }
    void Telepote(int count) {
        switch (count)
        {
            case 0://좌측 충돌
                transform.position = new Vector2(-9 + (5 - transform.position.y), 5.5f);
                rigid.velocity = Vector2.down * 3;
                break;
            case 1://하측 충돌
                transform.position = new Vector2(-9.5f, -5 - (-9 - transform.position.x));
                rigid.velocity = Vector2.right * 3;
                break;
            case 2://우측 충돌
                transform.position = new Vector2(9 + (-5 - transform.position.y), -5.5f);
                rigid.velocity = Vector2.up * 3;
                break;
            case 3://상측 충돌
                transform.position = new Vector2(9.5f, 5 - (9 - transform.position.x));
                rigid.velocity = Vector2.left * 3;
                break;
        }
        move += 1;
        if (move == 4) move = 0;
    }
}