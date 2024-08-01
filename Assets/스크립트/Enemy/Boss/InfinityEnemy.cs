using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InfinityEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameObject player;
    public GameManager gamemanager;
    SpriteRenderer spriterenderer;
    Rigidbody2D rigid;
    int i;
    int j;
    float dir;
    bool ready;
    
    private void OnDisable()
    {
        spriterenderer.flipX = false;
        DOTween.Clear();
        CancelInvoke();
    }

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    
    ready = true;
        j = 0;
        Invoke("TT",1);
        Big();
    }

    private void FixedUpdate()
    {
        dir = player.transform.position.x - transform.position.x;
        if ((0 <= dir) && (dir < 5) && (-8 < transform.position.x)&&(ready))
        {
            Invoke("ReBool", 0.5f);
            ready = false;
            rigid.velocity = Vector2.left;
            spriterenderer.flipX = false;
        }
        else if ((-5 <= dir) && (dir < 0) && (transform.position.x < 8) && (ready))
        {
            Invoke("ReBool", 0.5f);
            ready = false;
            rigid.velocity = Vector2.right;
            spriterenderer.flipX = true;
        }
        else if(((dir < -5) || (5 <= dir)) && (ready))
        {
            Invoke("ReBool", 0.5f);
            ready = false;
            rigid.velocity = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            if(rigid.velocity.x >= 0) spriterenderer.flipX = true;
            else if(rigid.velocity.x < 0) spriterenderer.flipX = false;
        }
    }

    void ReBool(){ ready = true;}

    void NN()
    {
        for (i = 0; i < 2; i++)
        {
            GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
            Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
            Bullet niddleLogic = niddle.GetComponent<Bullet>();
            niddleLogic.gamemanager = gamemanager;
            niddle.transform.position = new Vector2(transform.position.x + (i - 0.5f), transform.position.y - 1);
        Vector2 dirvec = new Vector2(player.transform.position.x - (transform.position.x + (i - 0.5f)), player.transform.position.y - (transform.position.y-1));
            niddleRigid.velocity = dirvec.normalized * 10.0f;
            float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            niddle.transform.Rotate(rotVec);
       }
        j++;
        if (j < 3) Invoke("NN", 0.8f);
        else if (j >= 3)
        {
            j = 0;
            Invoke("TT", 6.0f);
            Invoke("Big", 5);
        }
    }

    private void TT()
    {
        GameObject bomb = objectmanager.MakeObj("EnemyBombA");
        Bullet bombLogic = bomb.GetComponent<Bullet>();
        bombLogic.gamemanager = gamemanager;
        bomb.transform.position = player.gameObject.transform.position;
        j++;
        if (j < 30) Invoke("TT", 0.25f);
        else if (j >= 30)
        {
            j = 0;
            Invoke("NN", 6.0f);
            Invoke("Big", 5);
        }
    }
    void Big()
    {
        transform.DOScaleX(0.11f, 0.5f);
        transform.DOScaleY(0.08f, 0.5f);
        Invoke("Small",1);
    }
    void Small()
    {
        transform.DOScaleX(0.1f, 0.2f);
        transform.DOScaleY(0.1f, 0.2f);
    }
}
