using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Enemy enemy;
    SpriteRenderer spriterenderer;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    float time;
    int x;
    void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>(); ;
        rigid = GetComponent<Rigidbody2D>();
        enemy = gameObject.GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        spriterenderer.flipX = false;
        x = 1;
        time = 0.0f;
    }

    private void OnDisable()
    {
        if (enemy.health <= 0)
        {
            GameObject stoneL = objectmanager.MakeObj("EnemyCanonA");
            GameObject stoneC = objectmanager.MakeObj("EnemyCanonA");
            GameObject stoneR = objectmanager.MakeObj("EnemyCanonA");
            Bullet stoneLogicL = stoneL.GetComponent<Bullet>();
            Bullet stoneLogicC = stoneC.GetComponent<Bullet>();
            Bullet stoneLogicR = stoneR.GetComponent<Bullet>();
            Rigidbody2D stoneRigidL = stoneL.GetComponent<Rigidbody2D>();
            Rigidbody2D stoneRigidC = stoneC.GetComponent<Rigidbody2D>();
            Rigidbody2D stoneRigidR = stoneR.GetComponent<Rigidbody2D>();
            stoneLogicL.gamemanager = gamemanager;
            stoneLogicC.gamemanager = gamemanager;
            stoneLogicR.gamemanager = gamemanager;
            stoneL.transform.position = transform.position;
            stoneC.transform.position = transform.position;
            stoneR.transform.position = transform.position;
            stoneRigidL.velocity = Vector2.left * 3.0f;
            stoneRigidC.velocity = Vector2.down * 1.0f;
            stoneRigidR.velocity = Vector2.right * 3.0f;

        }
    }

    void Update()
    {
        rigid.velocity = Vector2.right * x * 3.0f;
        time += Time.deltaTime;
        if (time > 1.5f)
        {
            time = 0.0f;
            //돌 생성
            GameObject stone = objectmanager.MakeObj("EnemyCanonA");
            Bullet stoneLogic = stone.GetComponent<Bullet>();
            stoneLogic.gamemanager = gamemanager;
            stone.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 12)|| (collision.gameObject.layer == 14))//벽에 충돌시
        {
            x = (x == 1) ? -1 : 1;
            spriterenderer.flipX = (x == 1) ? false : true;


        }
    }
}
