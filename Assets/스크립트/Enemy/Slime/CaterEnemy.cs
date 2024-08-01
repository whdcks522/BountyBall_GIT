using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterEnemy : MonoBehaviour
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

    void Update()
    {
        //방향 파악
        if (player.transform.position.x < transform.position.x) LeftRight = -1;
        else if (player.transform.position.x >= transform.position.x) LeftRight = 1;
        spriterenderer.flipX = (1 == LeftRight) ? false : true;
        //장전
            time += Time.deltaTime;
         
            if (time < 1.5f) return;
            time = 0.0f;
            anim.SetTrigger("Action");

            GameObject bullet = objectmanager.MakeObj("EnemyCanonA");
            bullet.transform.position = transform.position;
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            bulletRigid.velocity = new Vector2( LeftRight * 3, 3) * 2.0f;
    }
}
