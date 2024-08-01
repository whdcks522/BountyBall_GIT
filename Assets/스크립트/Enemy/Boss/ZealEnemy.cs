using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZealEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public GameObject player;
    public GameObject warningRay;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    SpriteRenderer spriteRenderer2;
    float length;
    int i, j, z, LeftRight, x;
    bool round2;
    Vector2 savepos;
    Vector2 savepos2;
    void OnEnable()
    {
        i = 0;
        j = 1;
        x = 0;
        round2 = false;
        rigid.velocity = Vector2.up;
        Invoke("CanonAttack", 0.5f);
        warningRay.SetActive(false);
    }

    private void Awake()
    {
        spriteRenderer = warningRay.GetComponent<SpriteRenderer>();
        spriteRenderer2 = GetComponent<SpriteRenderer>();
        anim = warningRay.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Wait()
    {
        i = 0;   
        switch (j)
        {
            case 2:
                Bomerang();
                break;
            case 3:
                warningRay.SetActive(false);
                Wave();
                break;
            case 4:
                savepos = new Vector2(transform.position.x + LeftRight * 2.0f, transform.position.y + 1);
                savepos2 = new Vector2(transform.position.x - LeftRight * 2.0f, transform.position.y + 1);
                gamemanager.GenerateStart(savepos);
                gamemanager.GenerateStart(savepos2);
                Invoke("Summon",0.7f);
                break;
            case 5:
                Zenoside();
                break;
        }
    }
    void Zenoside()
    {
        for (z = 0; z <= 2; z++)
        {
            GameObject enemybulletA = objectmanager.MakeObj("EnemyBulletB");
            Rigidbody2D enemybulletARigid = enemybulletA.GetComponent<Rigidbody2D>();
            Bullet enemybulletALogic = enemybulletA.GetComponent<Bullet>();
            enemybulletALogic.gamemanager = gamemanager;
            enemybulletA.transform.position = new Vector2(transform.position.x, transform.position.y + 3);
            gamemanager.DestroyStart("D", enemybulletA.transform.position);
            Vector2 dirvec = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - (transform.position.y + 3));
            if (z != 0) dirvec += new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            enemybulletARigid.velocity = dirvec.normalized * 10.0f;
            float zValue = Mathf.Atan2(enemybulletARigid.velocity.x, enemybulletARigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            enemybulletA.transform.Rotate(rotVec);
        }
        Invoke("Zenoside", 2.0f);
        ++x;
        if (x == 4)
        {
            x = 0;
            j = 4;
            Invoke("Wait", 1.0f);
        }
    }
    
    void Summon() {
        //쉴더
        GameObject shieldEnemy = objectmanager.MakeObj("ShieldEnemy");
        Enemy enemy = shieldEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ShieldEnemy shieldEnemyLogic = shieldEnemy.GetComponent<ShieldEnemy>();
        shieldEnemyLogic.player = player;
        shieldEnemyLogic.gamemanager = gamemanager;
        shieldEnemy.transform.position = savepos;
        //벌목자
        GameObject woodEnemy = objectmanager.MakeObj("WoodEnemy");
        Enemy enemy2 = woodEnemy.GetComponent<Enemy>();
        enemy2.gamemanager = gamemanager;
        WoodEnemy woodEnemyLogic = woodEnemy.GetComponent<WoodEnemy>();
        woodEnemyLogic.player = player;
        woodEnemyLogic.objectmanager = objectmanager;
        woodEnemyLogic.gamemanager = gamemanager;
        woodEnemy.transform.position = savepos2;
        j = 5;
        if(!round2) Invoke("Wait", 1.5f);
        round2 = true;
    }

    void Wave()
    {
        GameObject bullet = objectmanager.MakeObj("EnemyBulletA");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();

        sprite.flipX = (1 == LeftRight) ? true : false;
        bulletLogic.gamemanager = gamemanager;
        bullet.transform.position = transform.position;
        gamemanager.DestroyStart("D", bullet.transform.position);
        bulletRigid.velocity = Vector2.right * LeftRight * 5.0f;
        i++;
        if (i < 30) Invoke("Wave", 0.3f);
        else if (i >= 30)
        {
            j = 4;
            Invoke("Wait", 1.0f);
        }
    }
    void Bomerang()
    {
        for (z = 0; z<= i; z++) {
            GameObject bmr = objectmanager.MakeObj("EnemyBmrA");
            bmr.transform.position = new Vector2(transform.position.x, transform.position.y + Random.Range(-2, 3));
            gamemanager.DestroyStart("D", bmr.transform.position);
            Rigidbody2D bmrRigid = bmr.GetComponent<Rigidbody2D>();
            bmrRigid.velocity = new Vector2(
                player.transform.position.x - bmr.transform.position.x, player.transform.position.y - bmr.transform.position.y).normalized * 10.0f;
            Bullet bmrlogic = bmr.GetComponent<Bullet>();
            bmrlogic.gamemanager = gamemanager;
            bmrlogic.host = gameObject;
            bmrlogic.BmrPos = player.transform.position + new Vector3(bmrRigid.velocity.x, bmrRigid.velocity.y).normalized * 2.0f;
        }
        ++i;
        if (i <= 4)Invoke("Bomerang", 2.0f);
        else if (i >= 5)
        {
            j = 3;
            warningRay.SetActive(true);
            anim.SetTrigger("Action");
            Invoke("Wait", 2.5f);
        }
    }

    Vector3 canonVec;
    void CanonAttack()
    {
        GameObject canon = objectmanager.MakeObj("EnemyCanonA");
        if(i % 2 == 0) canon.transform.position = new Vector2(transform.position.x - 2, transform.position.y + 2.5f);
        else if (i % 2 == 1) canon.transform.position = new Vector2(transform.position.x + 2, transform.position.y + 2.5f);
        gamemanager.DestroyStart("D", canon.transform.position);
        Rigidbody2D bulletRigid = canon.GetComponent<Rigidbody2D>();
        Bullet cononLogic = canon.GetComponent<Bullet>();
        cononLogic.gamemanager = gamemanager;
        canonVec = (player.transform.position - canon.transform.position).normalized + Vector3.up;
        length = Mathf.Abs(player.transform.position.x - canon.transform.position.x);
        bulletRigid.velocity = canonVec * (length * 0.25f + 5);
        ++i;
        if (i <= 20 )
            Invoke("CanonAttack", 0.75f);
        else if (i > 20 )
        {
            j = 2;
            Invoke("Wait",1.5f);
        }
    }
    private void Update()
    {
        //높이 파악
        if(transform.position.y > 0.5f) rigid.velocity = Vector2.down;
        else if (transform.position.y < -2.5f) rigid.velocity = Vector2.up;

        //좌우 파악
        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            LeftRight = -1;
        }
        else if (player.transform.position.x >= transform.position.x)
        {
            spriteRenderer.flipX = false;
            LeftRight = 1;
        }
        spriteRenderer2.flipX = (1 == LeftRight) ? true : false;
    }
}
