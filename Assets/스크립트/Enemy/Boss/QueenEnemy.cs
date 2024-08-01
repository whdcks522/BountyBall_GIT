using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public ObjectManager objectmanager;
    public GameObject player;

    Rigidbody2D rigid;
    Vector2 playerVec;
    GameObject bee;
    int count;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerVec = Vector2.zero;
        Invoke("Wind", 1.0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Update()
    {
        playerVec = (player.transform.position - transform.position).normalized;
        rigid.velocity += playerVec * 0.1f;
        if (rigid.velocity.magnitude > 4.0f) rigid.velocity = playerVec * 4;

        if (player.transform.position.x < transform.position.x) transform.localScale = new Vector2(0.15f, 0.15f);
        else transform.localScale = new Vector2(-0.15f, 0.15f);
    }

    void Bmr() {
        GameObject bmr = objectmanager.MakeObj("EnemyBmrA");
        bmr.transform.position = transform.position;
        gamemanager.DestroyStart("D", bmr.transform.position);
        Rigidbody2D bmrRigid = bmr.GetComponent<Rigidbody2D>();
        bmrRigid.velocity = -rigid.velocity.normalized * 10;
        Bullet bmrlogic = bmr.GetComponent<Bullet>();
        bmrlogic.gamemanager = gamemanager;
        bmrlogic.host = gameObject;
        bmrlogic.BmrPos = transform.position + new Vector3(bmrRigid.velocity.x, bmrRigid.velocity.y).normalized * 4.5f;
        count++;
        if (count > 2) Invoke("Sword", 2.0f);
        else Invoke("Bmr", 0.25f);
    }

    void Gas() {
        GameObject G = objectmanager.MakeObj("EnemyGas");
        Rigidbody2D GR = G.GetComponent<Rigidbody2D>();
        Bullet GB = G.GetComponent<Bullet>();
        Enemy GE = G.GetComponent<Enemy>();
        GE.gamemanager = gamemanager;
        GB.isRot = true;
        GB.gamemanager = gamemanager;
        G.transform.position = (Vector2)transform.position + Vector2.down;
        GR.velocity = Vector2.zero;
        GR.gravityScale = 1;
        bee = G;
        gamemanager.DestroyStart("D", G.transform.position);
        Invoke("Gas2", 3.5f);
        Invoke("Wind", 2.0f);
    }

    void Gas2() {
        if (!bee.activeSelf) return; 
        gamemanager.GenerateStart(bee.transform.position);
        Invoke("Gas3", 0.8f);
    }

    void Gas3() {
        if (!bee.activeSelf) return;
        for (int i = -1; i <= 1; i+= 2 ) {
            GameObject mainEnemy = gamemanager.objectmanager.MakeObj("BeeEnemy");
            Enemy enemy = mainEnemy.GetComponent<Enemy>();
            enemy.gamemanager = gamemanager;
            BeeEnemy logic = mainEnemy.GetComponent<BeeEnemy>();
            logic.player = player.GetComponent<Player>();
            logic.objectmanager = objectmanager;
            logic.gamemanager = gamemanager;
            //위치 이동
            mainEnemy.transform.position = (Vector2)bee.transform.position + new Vector2(i * 0.3f, 0);
        }
        for (int i = -1; i <= 1; i+=2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                GameObject bullet = objectmanager.MakeObj("EnemyBulletC");
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                bulletLogic.host = gameObject;
                bulletLogic.isExcel = true;
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                //힘,방향,위치
                bullet.transform.position = bee.transform.position;
                bulletRigid.velocity = new Vector2(i, j).normalized * 5;
                float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
                bullet.transform.Rotate(rotVec);
            }
        }
        bee.SetActive(false);
        gamemanager.DestroyStart("D", bee.transform.position);
    }

    void Wind() {
        for (int i = 0; i < 15; i++)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyWind");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = transform.position;
            gamemanager.DestroyStart("D", bullet.transform.position);
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * i * 2.0f / 15), Mathf.Cos(Mathf.PI * i * 2.0f / 15));
            bulletRigid.velocity = dirVec.normalized * 8;
            bullet.transform.Rotate(Vector3.back * 360 * i / 15 + Vector3.back * 270);
        }
        Invoke("Wind2", 0.5f);
    }

    void Wind2() {
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyInjection");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            bulletLogic.host = gameObject;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = transform.position;
            gamemanager.DestroyStart("D", bullet.transform.position);
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * i * 2.0f / 10), Mathf.Cos(Mathf.PI * i * 2.0f / 10));
            bulletRigid.velocity = dirVec.normalized * 8;
            bullet.transform.Rotate(Vector3.back * 360 * i / 10 + Vector3.back * 270);
        }
        count = 0;
        Invoke("Bmr", 2.0f);
    }

    void Sword() {
        Sword2(Vector2.up);
        Invoke("Gas", 1.5f);
    }

    void Sword2(Vector2 Shotpos) {
        GameObject bullet = objectmanager.MakeObj("PlayerBulletB");
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = (Vector2)transform.position + Shotpos * 1.5f;
        gamemanager.DestroyStart("D", bullet.transform.position);
        bulletRigid.velocity = Shotpos.normalized * 15.0f;
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 45.0f;
        bullet.transform.Rotate(rotVec);
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        bulletLogic.player = player;
        bulletLogic.host = player;
    }
}
