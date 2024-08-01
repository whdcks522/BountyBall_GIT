using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    float time;
    public GameObject player;
    public ObjectManager objectmanager;
    int LeftRight;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    public GameManager gamemanager;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        time = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)//환상벽에 충돌시
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.normalized.y * -3);
        else if (collision.gameObject.layer == 12)//적 벽에 충돌시
            rigid.velocity = Vector2.zero;
    }

    void Update()
    {
        //방향 파악
        if (player.transform.position.x < transform.position.x) LeftRight = -1;
        else if (player.transform.position.x >= transform.position.x) LeftRight = 1;
        spriterenderer.flipX = (1 == LeftRight) ? false : true;
        //장전
        time += Time.deltaTime;
        //위아래 이동
        Vector2 dirVec = new Vector2(0, player.transform.position.y - transform.position.y);
        rigid.AddForce(2.0f * dirVec);//3
        //위아래 최대속도 조절
        if(Mathf.Abs(rigid.velocity.y) > 4.0f) {
            if (rigid.velocity.y > 0)
                rigid.velocity = new Vector2(rigid.velocity.x, 4.0f);
        if (rigid.velocity.y < 0)
            rigid.velocity = new Vector2(rigid.velocity.x, -4.0f);
    }
        //공격
        if ((player.transform.position.y - 0.3f <= transform.position.y) && (player.transform.position.y + 0.3f >= transform.position.y))
        {
            if (time < 1.0f) return;
            time = 0.0f;
            
            anim.SetTrigger("Action");
            GameObject bullet = objectmanager.MakeObj("EnemyBulletA");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();

            sprite.flipX = (1 == LeftRight) ? true : false;
            bulletLogic.gamemanager = gamemanager;
            bullet.transform.position = transform.position;
            
            bulletRigid.velocity =  Vector2.right * LeftRight * 5.0f;
        }
    }
}
