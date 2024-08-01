using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndianEnemy : MonoBehaviour
{
    public GameObject player;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    int LeftRight;
    Vector2 Vec;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){Action();}

    public void Action()
    {
        CancelInvoke();
        Invoke("trueAction", 1.5f);
        Invoke("Attack", 2.0f);
    }
    void trueAction() { anim.SetTrigger("Action"); }
    void OnDisable()
    {
        CancelInvoke();
    }
    void Update()
    { 
        //방향 파악
        if (player.transform.position.x < transform.position.x) LeftRight = -1;
        else if (player.transform.position.x >= transform.position.x) LeftRight = 1;
        spriterenderer.flipX = (1 == LeftRight) ? false : true;
    }

    void Attack() {
        if (!gameObject.activeSelf) return;
        GameObject bmr = objectmanager.MakeObj("EnemyBmrA");
        bmr.transform.position = transform.position;
        Rigidbody2D bmrRigid = bmr.GetComponent<Rigidbody2D>();
        
        Bullet bmrlogic = bmr.GetComponent<Bullet>();
        bmrlogic.gamemanager = gamemanager;
        bmrlogic.host = gameObject;
        bmrlogic.BmrPos = player.transform.position;

        Vec = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        bmrRigid.velocity = Vec.normalized * 10.0f;
        gamemanager.GenerateStart(transform.position);
        Debug.DrawRay(transform.position, Vec.normalized * 20.0f, new Color(1, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vec.normalized, 20.0f, LayerMask.GetMask("EnemyBorder"));
        transform.position = ray.point - Vec.normalized * 1.0f;
        gamemanager.DestroyStart("D", transform.position);
        Invoke("Action", 3.5f);
    }
}
