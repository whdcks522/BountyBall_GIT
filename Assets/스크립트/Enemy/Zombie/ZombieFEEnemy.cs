using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFEEnemy : MonoBehaviour
{
    Vector2 createPos;
    int hitcount;
    bool isF;
    bool ready;
    public GameObject player;
    public GameManager gamemanager;
    Rigidbody2D rigid;
    float time;
    Vector3 playerPos;
    Vector3 onePos;

    private void OnDisable()
    {
        isF = true;
        hitcount = 0;
        ready = false;
        time = 0.0f;
        if (GetComponent<Enemy>().health <= 0)
        {
            for (int x = -1; x <= 1; x += 2) {
                GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyBulletA");
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.position = transform.position;
                bulletRigid.velocity = new Vector2(x, 0)* 5.0f;
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();

                sprite.flipX = (1 == x) ? true : false;
            }
        }
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        time += Time.deltaTime;
        playerPos = player.transform.position;
        onePos = transform.position;
        if (rigid.velocity.x < 0) GetComponent<SpriteRenderer>().flipX = false;
        else GetComponent<SpriteRenderer>().flipX = true;
        if ((time >= 0.0f) && (time < 2.0f) && (!ready))
        {
            rigid.velocity = (playerPos - onePos).normalized * 1.0f;
        }
        else if ((time >= 2.0f) && (time < 2.5f) && (!ready))
        {
            ready = true;
            rigid.velocity = (playerPos - onePos).normalized * 5.0f;
        }
        else if (time >= 2.5f)
        {
            rigid.velocity = new Vector2(0, 0);
            time = 0.0f;
            ready = false;
        }
    }

    void MakeF()
    {
        GameObject[] Enemys = gamemanager.objectmanager.ReturnObjs("Enemy");
        if (Enemys[0] == null) return;

        GameObject followEnemy = gamemanager.objectmanager.MakeObj("FollowEnemy");
        Enemy enemy = followEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        FollowEnemy followEnemyLogic = followEnemy.GetComponent<FollowEnemy>();
        followEnemyLogic.player = gamemanager.player;
        followEnemy.transform.position = createPos;
    }

    void MakeE()
    {
        GameObject[] Enemys = gamemanager.objectmanager.ReturnObjs("Enemy");
        if (Enemys[0] == null) return;

        GameObject excelEnemy = gamemanager.objectmanager.MakeObj("ExcelEnemy");
        Enemy enemy = excelEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ExcelEnemy excelEnemyLogic = excelEnemy.GetComponent<ExcelEnemy>();
        excelEnemyLogic.player = gamemanager.player;
        excelEnemy.transform.position = createPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12))//환상벽에 충돌시
        {
            rigid.velocity = Vector2.zero;
            ready = false;
        }
        else if (collision.gameObject.layer == 9)//총탄 피격시
        {
            ++hitcount;
            if (hitcount % 4 == 0)
            {
                createPos = transform.position;
                gamemanager.GenerateStart(createPos);
                if (isF)
                {
                    isF = false;
                    Invoke("MakeF", 0.8f);
                }
                else if (!isF)
                {
                    isF = true;
                    Invoke("MakeE", 0.8f);
                }
            }
        }     
    }
}
