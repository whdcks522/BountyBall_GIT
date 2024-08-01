using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeeEnemy : MonoBehaviour
{
    float time;
    public GameObject player;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    int LeftRight;
    bool ignite;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    GameObject eye;


    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        ignite = false;
        if(eye != null)eye.SetActive(false);
        time = 1.3f;
    }
    void Update()
    {
        //장전
        time += Time.deltaTime;
        if (time > 2.5f)
        {
            time = 0.0f;
            anim.SetTrigger("Action");

            eye = objectmanager.MakeObj("Eyes");
            eye.transform.position = new Vector3(transform.position.x + LeftRight * 0.5f, transform.position.y, transform.position.z);
            SpriteRenderer eyesp = eye.GetComponent<SpriteRenderer>();
            BoxCollider2D eyebox = eye.GetComponent<BoxCollider2D>();
            if (LeftRight == 1)
            {
                eyesp.flipX = false;
                eyebox.offset = new Vector2(1, 0);
            }
            else
            {
                eyesp.flipX = true;
                eyebox.offset = new Vector2(-1, 0);
            }
            Animator eyean = eye.GetComponent<Animator>();
            eyean.SetTrigger("Action");
            ignite = false;
        }
        else if (time > 1.3f)
        {
            if (time > 2.0f)
            {
                if (ignite) return;
                gamemanager.BombStart(transform.position);
                ignite = true;
            }
            //방향 파악
            if (player.transform.position.x < transform.position.x) LeftRight = -1;
            else if (player.transform.position.x >= transform.position.x) LeftRight = 1;
            spriterenderer.flipX = (1 == LeftRight) ? false : true;
        }
    }
}
