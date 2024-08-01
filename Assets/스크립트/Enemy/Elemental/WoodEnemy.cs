using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodEnemy : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 nPos;
    Vector3 pPos;
    Vector3 gPos;
    int count;
    int ready;
    float time;
    public ObjectManager objectmanager;
    public GameObject player;
    public GameManager gamemanager;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        ready = 1;
        count = 1;
        time = 0.0f;
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void GenerateTree(){

        GameObject dummyEnemy = objectmanager.MakeObj("DummyEnemy");
        Enemy enemy = dummyEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        dummyEnemy.transform.position = nPos;
    }

    private void Update()
    {
        if (count == 1)
        {
            //대기중
            time += Time.deltaTime;
            if (time > 1.5f)
            {//돌진 직전
                time = 0.0f;
                count = 2;
                pPos = player.transform.position;
                nPos = (pPos + transform.position) / 2.0f;
                switch (ready) {
                    case 1:
                        ready = 2;
                        break;
                    case 3:
                        ready = 4;
                        gamemanager.GenerateStart(nPos);
                        Invoke("GenerateTree", 0.7f);
                        break;
                }
            }
        }
        //돌진
        else if (count == 2)
        {
            gPos = (nPos - transform.position).normalized * 8.0f;
            if ((gPos.x >= 0.0f) && (gPos.y >= 0.0f))rigid.velocity = new Vector2(gPos.y, -gPos.x);//1사분면
            else if ((gPos.x < 0.0f) && (gPos.y >= 0.0f))rigid.velocity = new Vector2(gPos.y, -gPos.x);//2사분면
            else if ((gPos.x < 0.0f) && (gPos.y < 0.0f)) rigid.velocity = new Vector2(gPos.y, -gPos.x);//3사분면
            else if ((gPos.x >= 0.0f) && (gPos.y < 0.0f)) rigid.velocity = new Vector2(gPos.y, -gPos.x);//4사분면

            if ((pPos-transform.position).magnitude <= 1.0f)
            {
                switch (ready)
                {
                    case 2:
                        ready = 3;
                        break;
                    case 4:
                        ready = 1;
                        break;
                }
                count = 1;
                rigid.velocity = Vector2.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)//환상벽에 충돌시
        {
            switch (ready)
            {
                case 2:
                    ready = 3;
                    break;
                case 4:
                    ready = 1;
                    break;
            }
            rigid.velocity = Vector2.zero;
            count = 1;
        }
    }
}

