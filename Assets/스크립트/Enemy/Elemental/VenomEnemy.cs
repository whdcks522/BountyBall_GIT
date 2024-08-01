using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomEnemy : MonoBehaviour
{
    
    public GameObject player;
    public ObjectManager objectmanager;
    Rigidbody2D rigid;
    float time;
    Vector3 playerPos;
    Vector3 onePos;
    bool ready;
    int x, y;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ready = false;
        time = 0.0f;
        Invoke("MakeVenom", 0.3f);
    }
    
    private void OnDisable()
    {
        CancelInvoke();
    }
    void Update()
    {
        time += Time.deltaTime;
        if ((time >= 0.0f) && (time < 2.0f) && (!ready))
        {
            rigid.velocity = new Vector2(0, 0);
        }
        else if ((time >= 2.0f) && (time < 3.0f) && (!ready))
        {
            ready = true;
            playerPos = player.transform.position;
            onePos = transform.position;
            rigid.velocity = (playerPos - onePos).normalized * 10.0f;
        }
        else if (time >= 3.0f)
        {
            time = 0.0f;
            ready = false;
        }
    }
    void MakeVenom() {
        GameObject VV = objectmanager.MakeObj("Venom");
        VV.transform.position = transform.position;
        while (true) {
            x = Random.Range(-3, 4);
            y = Random.Range(-3, 4);
            if ((x != 0) && (y != 0)) break;
        }
        VV.GetComponent<Effect>().time = Random.Range(4, 7) / 2.0f;
        VV.GetComponent<Collider2D>().attachedRigidbody.velocity = new Vector2(x / 3.0f, y / 3.0f) * 2.0f;
        Invoke("MakeVenom", 0.3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12))//벽들에 충돌시
        {
            rigid.velocity = Vector2.zero;
        }
    }
}
