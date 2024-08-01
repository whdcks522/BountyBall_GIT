using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneEnemy : MonoBehaviour
{
    public GameObject player;
    public ObjectManager objectmanager;
    public GameManager gamemanager;

    Vector2 breakPos;
    public GameObject obj;
    bool ignite;
    float s;
    Rigidbody2D rigid;
    int incount;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        if (obj != null)
        {
            if (obj.name.Contains("Venom"))
            {
                obj.transform.localScale = new Vector2(0.25f, 0.25f);
                obj.SetActive(false);
            }
            obj = null;
        }
        CancelInvoke();
    }

    private void OnEnable()
    {
        incount = 0;
        ignite = false;
        Invoke("Gas",0.8f);
        Invoke("Circle", 1.2f);
    }

    void Rocket(){
        if (incount == 0)
        {
            breakPos = transform.position;
        }
        else
        {
            obj.GetComponent<Bullet>().time = 6.0f;
            breakPos = obj.transform.position;
            GameObject VV = gamemanager.objectmanager.MakeObj("Venom");
            VV.transform.position = breakPos;
            VV.GetComponent<Effect>().time = 3.5f;
            VV.GetComponent<Collider2D>().attachedRigidbody.velocity = obj.GetComponent<Rigidbody2D>().velocity.normalized * -1;
        }
        obj = objectmanager.MakeObj("EnemyRocketA");
        Bullet rocketRLogic = obj.GetComponent<Bullet>();
        rocketRLogic.gamemanager = gamemanager;
        rocketRLogic.player = player;
        obj.transform.position = breakPos;
        Rigidbody2D rocketRRigid = obj.GetComponent<Rigidbody2D>();
        rocketRRigid.velocity = ((Vector2)player.transform.position - breakPos).normalized * 2.0f;
        incount++;
        if (incount < 4)
        {
            Invoke("Rocket", 2.0f);
        }
        else
        {
            incount = 0;
            obj.GetComponent<Bullet>().time = 4.0f;
            Invoke("Gas", 2.8f);
            Invoke("Circle", 3.2f);
        }
    }

    void VenomReady() {
        ignite = true;
        rigid.velocity = Vector2.zero;
        obj = objectmanager.MakeObj("Venom");
        obj.GetComponent<Effect>().time = 4;
        obj.transform.position = transform.position;
        s = 0;
        obj.transform.localScale = new Vector2(0, 0);
        VenomCharge();
    }

    void VenomCharge()
    {
        obj.transform.localScale = new Vector2(s, s);
        s += 0.1f;
        if (s < 1.5f) Invoke("VenomCharge", 0.1f);
        else VenomAttack();
    }

    void VenomAttack()
    {
        obj.transform.localScale = new Vector2(1.5f, 1.5f);
        obj.GetComponent<Collider2D>().attachedRigidbody.velocity = new Vector2(player.transform.position.x - transform.position.x, 
            player.transform.position.y - transform.position.y).normalized * 2.0f;
        ignite = false;
        obj = null;
        Invoke("Rocket", 5.0f);
    }

    void Side() {
        Gas();
        Yellow(new Vector2(1,1));
        Yellow(new Vector2(1, 0));
        Yellow(new Vector2(0, 1));
        Yellow(new Vector2(-1, 1));
        Yellow(new Vector2(-1, 0));
        Invoke("YellowL", 1.5f);
        Invoke("YellowR", 3.3f);
        Invoke("VenomReady", 4.5f);
    }
    
    void Yellow(Vector2 vec) {
        GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y) + vec * 0.6f;
        bulletRigid.AddForce(vec.normalized * 3.0f, ForceMode2D.Impulse);
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
        bullet.transform.Rotate(rotVec);
        bulletLogic.isExcel = true;
    }

    void YellowL() {
        for (int i = -4; i <= 4; i += 2) {
            GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = new Vector2(-10, i);
            bulletRigid.AddForce(Vector2.right * 3.0f, ForceMode2D.Impulse);
            float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            bullet.transform.Rotate(rotVec);
            bulletLogic.isExcel = true;
        }
    }
    void YellowR()
    {
        for (int i = -3; i <= 5; i += 2)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = new Vector2(10, i);
            bulletRigid.AddForce(Vector2.left * 3.0f, ForceMode2D.Impulse);
            float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            bullet.transform.Rotate(rotVec);
            bulletLogic.isExcel = true;
        }
    }

    void Circle()
    {
        GameObject bullet = objectmanager.MakeObj("EnemyCircle");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y) + Vector2.down * 2;
        bulletRigid.AddForce(Vector2.down * 5.0f, ForceMode2D.Impulse);
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 90;
        bullet.transform.Rotate(rotVec);
        bulletLogic.time = 2.05f;
        bulletLogic.isCircle = true;
        incount++;
        if (incount % 2 == 0) Invoke("Circle", 0.8f);
        else if (incount == 11)
        {
            incount = 0;
            Invoke("Side", 2.0f);
        }
        else Invoke("Circle", 0.2f);
    }

    void Gas() {
        for (int i = -3; i <= 3; i += 2) {
            GameObject G = objectmanager.MakeObj("EnemyGas");
            Rigidbody2D GR = G.GetComponent<Rigidbody2D>();
            Bullet GB = G.GetComponent<Bullet>();
            Enemy GE = G.GetComponent<Enemy>();
            GE.gamemanager = gamemanager;
            GB.isRot = true;
            GB.gamemanager = gamemanager;
            GR.transform.position = transform.position;
            GR.velocity = Vector2.right * i + Vector2.up;
            GR.gravityScale = 1;
        }
        gamemanager.DestroyStart("D", (Vector2)transform.position + new Vector2(1, 1));
        gamemanager.DestroyStart("D", (Vector2)transform.position + new Vector2(1, -1));
        gamemanager.DestroyStart("D", (Vector2)transform.position + new Vector2(-1, 1));
        gamemanager.DestroyStart("D", (Vector2)transform.position + new Vector2(-1, -1));
    }

    void Update()
    {
        if (!ignite)
        {
            //좌우 이동
            Vector2 dirVec = new Vector2(-player.transform.position.x - transform.position.x, 0);
            rigid.AddForce(3.0f * dirVec.normalized);
            //위아래 최대속도 조절
            if (Mathf.Abs(rigid.velocity.x) > 7.0f)
            {
                if (rigid.velocity.x > 0)
                    rigid.velocity = new Vector2(7.0f, 0);
                else if (rigid.velocity.x < 0)
                    rigid.velocity = new Vector2(-7.0f, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14)|| (collision.gameObject.layer == 12))//벽들에 충돌시
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * -5, 0);
    }
}
