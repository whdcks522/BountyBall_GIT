using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public ObjectManager objectmanager;
    public GameObject player;

    Rigidbody2D rigid;
    Animator anim;

    float time;
    bool ready;
    bool[] ignite;

    Vector2 playerVec;
    GameObject EL;
    int i;

    private void Awake()
    {
        ignite = new bool[3];
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ignite[0] = false;
        ignite[1] = false;
        ignite[2] = false;
        time = 0.0f;
        ready = true;
        i = 0;
    }
    private void OnDisable()
    {
        if(EL != null) EL.SetActive(false);
        CancelInvoke();
    }
    void Update()
    {
        if (!ready) return;
        time += Time.deltaTime;
        playerVec = player.transform.position - transform.position;
        if (Mathf.Abs(playerVec.x) > Mathf.Abs(playerVec.y)) { rigid.velocity = Vector2.right * playerVec.normalized.x * 2; }
        else if (Mathf.Abs(playerVec.x) <= Mathf.Abs(playerVec.y)) { rigid.velocity = Vector2.up * playerVec.normalized.y * 2; }

        if (time > 2.0f)
        {
            int r;
            while (true)
            {
                r = Random.Range(1, 4);
                if ((r == 1) && (!ignite[0]))
                {
                    Electric();
                    break;
                }
                else if ((r == 2) && (!ignite[1]))
                {
                    Catter();
                    break;
                }
                else if ((r == 3)&& (!ignite[2]))
                {
                    Circle();
                    break;
                }
            }
            time = 0.0f;
            ready = false;
            rigid.velocity = Vector2.zero;
        }
    }

    void Circle() {
        Invoke("Circle2", 0.5f);
        ignite[0] = false;
        ignite[1] = false;
        ignite[2] = true;
    }

    void Circle2() {
        CircleShot(Vector2.left);
        CircleShot(Vector2.right);
        CircleShot(Vector2.down);
        Invoke("Circle3", 1.0f); ;
    }

void Circle3() { ready = true; }

void CircleShot(Vector2 vec) {
        gamemanager.DestroyStart("D",new Vector2(transform.position.x, transform.position.y) + vec * 2.0f);
        GameObject bullet = objectmanager.MakeObj("EnemyCircle");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y) + vec;
        bulletRigid.AddForce(vec * 5.0f, ForceMode2D.Impulse);
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 180;
        bullet.transform.Rotate(rotVec);
        if (Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI % 180 == 0)
        {
            if ((bulletRigid.velocity.x != 0) && (bulletRigid.velocity.y == 0))//기본상태로 좌우
                bulletLogic.time = 1.2f;
            else if ((bulletRigid.velocity.x == 0) && (bulletRigid.velocity.y != 0))//기본상태로 상하
                bulletLogic.time = 0.5f;
        }
        else if ((Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI % 180 == 90)||
            (Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI % 180 == -90))
        {
            if ((bulletRigid.velocity.x != 0) && (bulletRigid.velocity.y == 0))//선 상태로 좌우
                bulletLogic.time = 0.5f;
            else if ((bulletRigid.velocity.x == 0) && (bulletRigid.velocity.y != 0))//선 상태로 상하
                bulletLogic.time = 1.2f;
        }
        bulletLogic.isCircle = true;
    }

    void Catter() {
        ignite[0] = false;
        ignite[1] = true;
        ignite[2] = false;
        if (player.transform.position.x >= transform.position.x)
        {
            i = 1;
            anim.SetTrigger("Right");
        }
        else if (player.transform.position.x < transform.position.x)
        {
           i = -1;
            anim.SetTrigger("Left");
        }
        Invoke("Catter2", 0.25f); ;
    }
    void Catter2() {
        GameObject canon = objectmanager.MakeObj("EnemyCanonA");
        canon.transform.position = new Vector2(transform.position.x + i, transform.position.y);
        Rigidbody2D bulletRigid = canon.GetComponent<Rigidbody2D>();

        Vector2 canonVec = (player.transform.position - canon.transform.position).normalized + Vector3.up;
        float length = Mathf.Abs(player.transform.position.x - canon.transform.position.x);
        bulletRigid.velocity = canonVec * (length * 0.25f + 5);

        Bullet cononLogic = canon.GetComponent<Bullet>();
        cononLogic.gamemanager = gamemanager;

        for (int j = -1; j<= 1; j += 2) {
            GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid2 = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = new Vector2(player.transform.position.x + j, 5.5f);

            bulletRigid2.AddForce(Vector2.down * 8.0f, ForceMode2D.Impulse);
            float zValue = Mathf.Atan2(bulletRigid2.velocity.x, bulletRigid2.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
            bullet.transform.Rotate(rotVec);
        }
        Invoke("Catter3", 0.75f); ;
    }
    void Catter3() { ready = true; }

    

    void Electric() {
        ignite[0] = true;
        ignite[1] = false;
        ignite[2] = false;
        EL = objectmanager.MakeObj("ElectricLine");
        EL.transform.position = new Vector2(transform.position.x, transform.position.y) + Vector2.up * 0.6f;
        EL.SetActive(false);
        Invoke("Electric15", 0.5f);
        Invoke("Electric2", 1.0f);
    }
    void Electric15() {
        EL.SetActive(true);
        EL.transform.rotation = Quaternion.identity;
        EL.transform.localScale = new Vector2(7, 1);
        EL.transform.Rotate(Vector3.back * 270);
    }
    void Electric2()
    {
        if (i <= 200)
        {
            if (i % 10 == 0) {
                GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                //힘,방향,위치
                bullet.transform.position = new Vector2(transform.position.x, transform.position.y) + Vector2.up * 0.6f;
                Vector2 dirVec = -1 * new Vector2(Mathf.Sin(Mathf.PI * i / 200 * 2.0f), Mathf.Cos(Mathf.PI * i / 200 * 2.0f));//2------------
                bulletRigid.AddForce(dirVec.normalized * 5.0f, ForceMode2D.Impulse);
                bullet.transform.Rotate(Vector3.back * 360 * i / 200 + Vector3.back * 90);//1---------------
                bulletLogic.isExcel = true;
            }
            EL.transform.rotation = Quaternion.identity;
            EL.transform.Rotate(Vector3.back * 360 * i / 200 + Vector3.back * 270);//1-------------
            i++;
            Invoke("Electric2", 0.05f);
        }
        else if (i >= 250)
        {
            i = 0;
            EL.SetActive(false);
            ready = true;
        }
        else if (i >= 201)
        {
            i++;
            Invoke("Electric2", 0.05f);
        }
        
    }
}
