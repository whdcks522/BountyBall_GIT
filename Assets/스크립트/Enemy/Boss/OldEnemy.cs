using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldEnemy : MonoBehaviour
{
    
    public ObjectManager objectmanager;
    public GameObject player;
    public GameObject Hshield;
    public GameManager gamemanager;
    SpriteRenderer spriterenderer;
    Rigidbody2D rigid;
    int x;
    int i;
    int j;

    private void OnDisable()
    {
        CancelInvoke();
        Hshield.SetActive(false);
    }

    private void OnEnable()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        j = -10;
        i = 0;
        x = 1;
        LRMove();
    }
    
    void LRMove()
    {
        x = (x == 1) ? -1 : 1;
        spriterenderer.flipX = (x == 1) ? true : false;
        rigid.velocity = new Vector2(x * 1.0f, 0);
        Hshield.SetActive(true);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if ((Hshield.activeSelf) && (collision.transform.position.y >= transform.position.y))
            //반사
            {
                    gamemanager.SoundPlay("Glass");
                if (bullet.dmg == 10)
                {
                    collision.gameObject.transform.rotation = Quaternion.identity;
                    Rigidbody2D bulletrigid = collision.GetComponent<Rigidbody2D>();
                    bulletrigid.velocity = new Vector2(bulletrigid.velocity.x, -1.0f * bulletrigid.velocity.y);
                    float zValue = Mathf.Atan2(bulletrigid.velocity.x, bulletrigid.velocity.y) * 180 / Mathf.PI;
                    Vector3 rotVec = Vector3.back * zValue + Vector3.back * 45.0f;
                    bullet.transform.Rotate(rotVec);
                }
                else if (bullet.dmg == 30 && collision.GetComponent<Bullet>().host != null)
                {
                    collision.transform.position = (Vector2)collision.transform.position + rigid.velocity.normalized;
                    collision.gameObject.transform.rotation = Quaternion.identity;
                    Rigidbody2D bulletrigid = collision.GetComponent<Rigidbody2D>();
                    bulletrigid.velocity = new Vector2(-1.0f * bulletrigid.velocity.x, bulletrigid.velocity.y);
                    float zValue = Mathf.Atan2(bulletrigid.velocity.x, bulletrigid.velocity.y) * 180 / Mathf.PI;
                    Vector3 rotVec = Vector3.back * zValue + Vector3.back * 45.0f;
                    bullet.transform.Rotate(rotVec);
                }
            }
            else//피격
            {
                //체력 감소
                Enemy enemy = GetComponent<Enemy>();
                enemy.HealthControl(bullet.dmg);
                collision.gameObject.SetActive(false);
            }
        }
        if (collision.gameObject.layer == 14)//가상벽
        {
            Hshield.SetActive(false);
            rigid.velocity = Vector2.zero;
            if (x == 1)
            {
                j = -10;
                Right();
            }
            else if (x == -1)
            {
                i = 0;
                Left();
            }
        }
        else if (collision.gameObject.layer == 12)//적 벽
        {
            Hshield.SetActive(false);
            rigid.velocity = Vector2.zero;
            if (x == 1)
            {
                j = -10;
                Right();
            }
            else if (x == -1)
            {
                i = 0;
                Left();
            }
        }
    }

    void Left()
    {
        if (!gameObject.activeSelf) return;
            GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
            Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletlogic = bullet.GetComponent<Bullet>();
            bulletlogic.gamemanager = gamemanager;
            bulletrigid.velocity = new Vector2(Mathf.Sin(Mathf.PI * 2.0f * i/20),-1) * 5.0f;
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        float zValue = Mathf.Atan2(bulletrigid.velocity.x, bulletrigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
        bullet.transform.Rotate(rotVec);
        if (i == 10) Center();
        ++i;
        if (i < 20) Invoke("Left", 0.1f);
        else if (i >= 20)
        {
            LRMove();
        }
    }

    void Right()
    {
        for ( i = 0; i < 2; i ++) {
            GameObject bullet = objectmanager.MakeObj("EnemyRocketA");
            Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletlogic = bullet.GetComponent<Bullet>();
            bulletlogic.gamemanager = gamemanager;
            bulletlogic.player = player;
            bulletrigid.velocity = new Vector2(-1,i + 0.8f);
            bullet.transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y); 
        }
        Invoke("LRMove", 2);
    }

    void Center()
    {
        GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
        Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
        Bullet bulletlogic = bullet.GetComponent<Bullet>();
        bulletlogic.gamemanager = gamemanager;
        bulletrigid.velocity = new Vector2(0, 8);
        bullet.transform.position = new Vector2(j, -5.5f);
        bullet.transform.Rotate(Vector3.forward * 90);
        ++j;
        if (j < 11) Invoke("Center", 0.45f);
        else if (j >= 10) return;
    }
}
