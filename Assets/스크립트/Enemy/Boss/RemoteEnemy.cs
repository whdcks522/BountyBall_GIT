using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public ObjectManager objectmanager;
    public GameObject player;
    public GameObject host;
    public GameObject[] Fire;
    Vector2 Vec;
    bool ignite;
    int justMovecount;
    int[] Tpos = { 0, 0 };
    int rannum;

    private void Update()
    {
        transform.position = host.transform.position;
        if (!host.gameObject.activeSelf) gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnEnable()
    {
        FireOff();
        ignite = false;
        justMovecount = 0;
    }

    void FireOff() {
        Fire[0].SetActive(false);
        Fire[1].SetActive(false);
        Fire[2].SetActive(false);
        Fire[3].SetActive(false);
    }

    void RedShot()
    {
        for (float i = 0; i<=1.0f;i += 0.25f)
        {
            ThrowBullet("EnemyBulletB", Vector2.Lerp(Vec, new Vector2(-Vec.y, Vec.x), i));
        }
        for (float i = 1; i > 0.0f; i -= 0.25f)
        {
            ThrowBullet("EnemyBulletB", Vector2.Lerp(Vec, new Vector2(Vec.y, -Vec.x), i));
        }
    }
    void GoldShot()
    {
        for (float i = 0; i <= 1.0f; i += 0.25f)
        {
            ThrowBullet("EnemyBulletC", Vector2.Lerp(Vec, new Vector2(-Vec.y, Vec.x), i));
        }
        for (float i = 1; i > 0.0f; i -= 0.25f)
        {
            ThrowBullet("EnemyBulletC", Vector2.Lerp(Vec, new Vector2(Vec.y, -Vec.x), i));
        }
    }
    void NiddleShot()
    {
        for (float i = -0.5f; i <= 0.5f; i++)
        {
            for (float j = -0.5f; j <= 0.5f; j++)
            {
                GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
                Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
                Bullet niddleLogic = niddle.GetComponent<Bullet>();
                niddleLogic.gamemanager = gamemanager;
                niddle.transform.position = new Vector2(transform.position.x + i, transform.position.y + j);
                Vector2 dirvec = new Vector2(player.transform.position.x - niddle.transform.position.x, player.transform.position.y - niddle.transform.position.y);
                niddleRigid.velocity = dirvec.normalized * 8.0f;
                float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
                niddle.transform.Rotate(rotVec);
            }
        }
    }
    void RocketShot(int r)
    {
        GameObject rocket = objectmanager.MakeObj("EnemyRocketA");
        Bullet rocketLogic = rocket.GetComponent<Bullet>();
        rocketLogic.gamemanager = gamemanager;
        rocketLogic.player = player;
        rocket.transform.position = new Vector3(transform.position.x, transform.position.y + r);
        Rigidbody2D rocketRigid = rocket.GetComponent<Rigidbody2D>();
        rocketRigid.velocity = new Vector2(0, r * 1.75f);
    }
    void ThrowBullet(string type, Vector2 vec)
    {
        GameObject bullet = objectmanager.MakeObj(type);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        if(type == "EnemyBulletB") bulletRigid.velocity = new Vector2(vec.x, vec.y).normalized * 7.0f;
        else if(type == "EnemyBulletC") bulletRigid.velocity = new Vector2(vec.x, vec.y).normalized * 3.0f;
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
        bullet.transform.Rotate(rotVec);
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        if(type == "EnemyBulletC") bulletLogic.isExcel = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 9)&&(!ignite))//플레이어 총탄 피격시
            Telepote();
    }
    void Telepote() {
        while (justMovecount != 4) {
            Tpos[0] = Random.Range(-8, 8);
            Tpos[1] = Random.Range(-4, 4);
            Vector3 TposEnd = new Vector3(Tpos[0], Tpos[1]);
            if ( ( (player.transform.position - TposEnd).magnitude > (4.0f + justMovecount / 2.0f)))
            {
                gamemanager.GenerateStart(transform.position);
                host.transform.position = new Vector2(Tpos[0], Tpos[1]);
                transform.position = host.transform.position;
                break;
            }
        }
        Vec = (player.transform.position - transform.position).normalized;
        justMovecount++;
        if (justMovecount >= 5)
        {
            if (-Vec.y > 0f)
            {
                rannum = Random.Range(1, 6);
            }
            else rannum = Random.Range(1, 4);

            if (rannum == 1) RedShot();
            else if (rannum == 2) GoldShot();
            else if (rannum == 3)
            {
                RocketShot(1);
                RocketShot(-1);
            }
            else if ((rannum == 4)||(rannum == 5)) NiddleShot();
            justMovecount = 0;
            FireOff();
        }
        if(justMovecount != 0)Fire[justMovecount-1].SetActive(true);
        ignite = true;
        Invoke("ReturnIgnite", 0.2f);
    }
    void ReturnIgnite() { ignite = false; }
}
