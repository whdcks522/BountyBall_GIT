using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject player;
    public ObjectManager objectmanager;
    
    SpriteRenderer spriterenderer;
    Animator anim;
    Rigidbody2D rigid;
    GameObject obj;
    float s;
    bool ignite;
    bool thrown;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        ignite = false;
        Invoke("Action", 1.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
        if((obj != null)&&(!thrown))obj.SetActive(false);
    }

    void Action() {
        anim.SetTrigger("Action");
        obj = objectmanager.MakeObj("Venom");
        obj.GetComponent<Effect>().time = 4;
        obj.transform.position = transform.position;
        s = 0;
        obj.transform.localScale = new Vector2(0, 0);
        thrown = false;
        Charge();
    }

    void Charge() {
            obj.transform.localScale = new Vector2(s * 0.125f, s * 0.125f);
            s += 0.25f;
        if (s < 4.0f) Invoke("Charge", 0.1f);
        else Attack();
    }

    void Attack() {
        thrown = true;
        anim.SetTrigger("Action2");
        obj.transform.localScale = new Vector2(0.5f , 0.5f);
        obj.GetComponent<Effect>().time = 4;
        Vector2 playerPos = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        obj.GetComponent<Collider2D>().attachedRigidbody.velocity = playerPos.normalized * 8.0f;
        if (!ignite)
        {
            rigid.velocity = -playerPos.normalized * 15.0f;
            Invoke("Stop", 0.2f);
        }
        Invoke("Action", 1.0f);
    }

    void Stop() {
        rigid.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (player.transform.position.x < transform.position.x) spriterenderer.flipX = true;
        else if (player.transform.position.x >= transform.position.x) spriterenderer.flipX = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12))//벽들에 충돌시
        {
            ignite = true;
            rigid.velocity = Vector2.zero;
        }
    }
}
