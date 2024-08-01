using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEnemy : MonoBehaviour
{
    public GameObject player;
    public ObjectManager objectmanager;
    public GameManager gamemanager;

    Player playerLogic;
    Rigidbody2D rigid;
    int incount;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        Invoke("palyer", 0.1f);
        Invoke("AroundShot", 0.75f);
    }
    void palyer() {
        playerLogic = player.GetComponent<Player>();
    }

    void Circle() {
        GameObject bullet = objectmanager.MakeObj("EnemyCircle");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = new Vector2(10.5f, playerLogic.flipPos.y);
        bulletRigid.AddForce(Vector2.left * 5.0f, ForceMode2D.Impulse);
        bulletLogic.isCircle = false;
        incount++;
        if(incount < 10)Invoke("Circle", 0.75f);
        else
        {
            Invoke("Right", 1.5f);
        }
    }

    void Right()
    {
        GameObject eye = objectmanager.MakeObj("AntiEyes");
        eye.transform.position = new Vector2(9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = true;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        Invoke("Right2", 1.5f);
    }
    void Right2()
    {
        GameObject eye = objectmanager.MakeObj("Eyes");
        eye.transform.position = new Vector2(9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = true;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
        BoxCollider2D eyebox = eye.GetComponent<BoxCollider2D>();
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        Invoke("AroundShot", 2.6125f);
    }

    void Left() {
        GameObject eye = objectmanager.MakeObj("Eyes");
        eye.transform.position = new Vector2(-9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = false;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        Invoke("Left2", 1.5f);
    }
    void Left2()
    {
        GameObject eye = objectmanager.MakeObj("AntiEyes");
        eye.transform.position = new Vector2(-9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = false;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        Invoke("Rain", 2.0f);
    }

    void Rain() {
        for (int x = -3; x <= 3; x++)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyNiddleA");
            Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletlogic = bullet.GetComponent<Bullet>();
            bulletlogic.gamemanager = gamemanager;
            bulletrigid.velocity = new Vector2(x, 1).normalized * 8;
            bullet.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
            float zValue = Mathf.Atan2(bulletrigid.velocity.x, bulletrigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
            bullet.transform.Rotate(rotVec);
        }
        Invoke("Rain2", 1.25f);
    }
    void Rain2() {
        for (int x = -9; x <= 9; x++)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyNiddleA");
            Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
            Bullet bulletlogic = bullet.GetComponent<Bullet>();
            bulletlogic.gamemanager = gamemanager;
            bulletrigid.velocity = new Vector2(0, -8);
            bullet.transform.position = new Vector2(x, 5.55f);
            bullet.transform.Rotate(Vector3.forward * 270);
        }
        Invoke("Circle", 4.5f);
        incount = 0;
    }

    void AroundShot() {
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++)
            {
                if ((x == 0) && (y == 0)) continue;
                gamemanager.BombStart(new Vector2(transform.position.x + x, transform.position.y + y));
            }
        }
        Invoke("AroundShot2", 0.75f);
    }
    void AroundShot2()
    {
        for (int i = 0; i < 25; i++)
        {
            //선언
            GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * i / 25 * 2.0f), Mathf.Cos(Mathf.PI * i / 25 * 2.0f)).normalized;
            bullet.transform.position = player.transform.position + new Vector3(dirVec.x * 5.0f, dirVec.y * 5.0f, 0);
            bulletRigid.AddForce(-dirVec * 3.0f, ForceMode2D.Impulse);
            bullet.transform.Rotate(Vector3.back * 360 * i / 25 + Vector3.back * 90);
            bulletLogic.isExcel = true;
        }
        Invoke("Left", 1.75f);
    }

    void Update()
    {
        if (Time.timeScale != 0) transform.Rotate(Vector3.forward * 10.0f);
    }
}
