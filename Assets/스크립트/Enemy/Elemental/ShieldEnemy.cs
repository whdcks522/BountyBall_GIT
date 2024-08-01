using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{

    bool ready;
    public GameObject player;
    public GameManager gamemanager;
    Rigidbody2D rigid;
    float time;
    Vector3 playerPos;
    Vector3 onePos;
    public GameObject[] shields; 
    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        ready = false;
        time = 0.0f;
        shields[0].SetActive(false);
        shields[1].SetActive(false);
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
            if (playerPos.x < onePos.x)
            {
                shields[0].SetActive(false);
                shields[1].SetActive(true);
            }
            else
            {
                shields[0].SetActive(true);
                shields[1].SetActive(false);
            }
            rigid.velocity = (playerPos - onePos).normalized * 10.0f;
        }
        else if (time >= 3.0f)
        {
            time = 0.0f;
            ready = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if ((collision.gameObject.layer == 14)|| (collision.gameObject.layer == 12))//환상벽에 충돌시
            rigid.velocity = Vector2.zero; 

        else if (collision.gameObject.layer == 9)
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (
                 (shields[0].activeSelf) && (collision.transform.position.x < transform.position.x)
                     ||
                    (shields[1].activeSelf) && (collision.transform.position.x > transform.position.x)
              )
              //반사
            {
                gamemanager.SoundPlay("Glass");
                if (bullet.dmg == 10)
                {
                    collision.gameObject.transform.rotation = Quaternion.identity;
                    Rigidbody2D bulletrigid = collision.GetComponent<Rigidbody2D>();
                    bulletrigid.velocity = new Vector2(-1.0f * bulletrigid.velocity.x, bulletrigid.velocity.y);
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
    }
}
