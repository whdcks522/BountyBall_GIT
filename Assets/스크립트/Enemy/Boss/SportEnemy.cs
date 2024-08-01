using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject player;
    public ObjectManager objectmanager;
    public Player playerLogic;

    Rigidbody2D rigid;

    public GameObject[] fire;
    public GameObject leftWeapon;
    public GameObject rightWeapon;
    Vector2[] flipPos;
    bool Jingite;
    bool[] ignite;
    int arrow;
    int jumpCount;
    int smallCount;

    void Stone() {
        for (int i = -1; i <= 1; i+= 2) {
            GameObject bullet = objectmanager.MakeObj("EnemyCanonA");
            bullet.transform.position = new Vector2(flipPos[0].x + (i * (smallCount + 2)), -6);
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            bulletRigid.velocity = Vector2.up * 15;
        }
        smallCount++;
        if(smallCount < 5)Invoke("Stone", 0.3f);
    }

    void New() {
        for (int i = -1; i <= 1; i += 2)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyBall");
            bullet.transform.position = (Vector2)transform.position + i * Vector2.right;
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            bulletLogic.dmg =  i * 3;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            bulletRigid.velocity = new Vector2( i * 3, 0);
            SpriteRenderer sp = bullet.GetComponent<SpriteRenderer>();
            if (i == 1) sp.flipX = false;
            else sp.flipX = true;
            gamemanager.DestroyStart("D", (bullet.transform.position)); 
        }
    }

    void Bmr() {
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                if ((i == 0) && (j == 0)) continue;
                GameObject bmr = objectmanager.MakeObj("EnemyBmrA");
                bmr.transform.position = transform.position;
                Rigidbody2D bmrRigid = bmr.GetComponent<Rigidbody2D>();
                Bullet bmrlogic = bmr.GetComponent<Bullet>();
                bmrlogic.gamemanager = gamemanager;
                bmrlogic.host = gameObject;
                bmrRigid.velocity = new Vector2(i, j).normalized * 10;
                bmrlogic.BmrPos = (Vector2)transform.position + bmrRigid.velocity.normalized * 6;
                gamemanager.DestroyStart("D" ,(Vector2)transform.position + bmrRigid.velocity.normalized * 1);
            } 
        }
    }

    void Electric() {
        leftWeapon = objectmanager.MakeObj("ElectricLine");
        rightWeapon = objectmanager.MakeObj("ElectricLine");

        leftWeapon.transform.position = new Vector2(-9.25f, 6);
        rightWeapon.transform.position = new Vector2(9.25f, 6);

        leftWeapon.transform.rotation = Quaternion.identity;
        rightWeapon.transform.rotation = Quaternion.identity;

        leftWeapon.transform.Rotate(Vector3.back * 90);
        rightWeapon.transform.Rotate(Vector3.back * 90);

        leftWeapon.transform.localScale = new Vector2(3, 1);
        rightWeapon.transform.localScale = new Vector2(3, 1);
        Invoke("Electric2", 0.25f);
    }

    void Electric2()
    {
        bool L = false;
        bool R = false;
        if (Mathf.Abs(leftWeapon.transform.position.x - flipPos[1].x) >= 1.5f)
        {
            L = true;
            leftWeapon.transform.position = (Vector2)leftWeapon.transform.position + Vector2.right * 0.15f;
        }
        if (Mathf.Abs(rightWeapon.transform.position.x - flipPos[1].x) >= 1.5f)
        {
            R = true;
            rightWeapon.transform.position = (Vector2)rightWeapon.transform.position + Vector2.left * 0.15f;
        }
        if (!L && !R) Invoke("Electric3", 0.5f);
        else Invoke("Electric2", 0.03f);
    }

    void Electric3()
    {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
    }

    void CheckPos() {
        if (player.transform.position.x < transform.position.x) arrow = -1;
        else arrow = 1;
    }

    private void OnEnable()
    {
        fire[0].SetActive(false);
        fire[1].SetActive(false);
        Jingite = false;
        jumpCount = 0;
        Invoke("CheckPos", 0.1f);
    }

    void jump(string type) {
        if (Jingite) return;
        Jingite = true;
        Invoke("JigniteReturn", 0.1f);
        rigid.velocity = new Vector2(arrow * 4, 15);
        jumpCount++;
        if (jumpCount == 3)
        {
            fire[0].SetActive(false);
            fire[1].SetActive(false);
            jumpCount = 0;
            smallCount = 0;
             while (true)
             {
                int r = Random.Range(1, 5);
                if ((r == 1) && (!ignite[0]))
                {
                    smallCount = 0;
                    flipPos[0] = transform.position;
                    Stone();
                    Clear();
                    ignite[0] = true;
                    break;
                }
                else if ((r == 2) && (!ignite[1]))
                {
                    Bmr();
                    Clear();
                    ignite[1] = true;
                    break;
                }
                else if ((r == 3) && (!ignite[2]))
                {
                    flipPos[1] = transform.position;
                    Electric();
                    Clear();
                    ignite[2] = true;
                    break;
                }
                else if ((r ==4) && (!ignite[3]))
                {
                    New();
                    Clear();
                    ignite[3] = true;
                    break;
                }
        }
    }
        else if (jumpCount == 2) {
            fire[0].SetActive(true);
            fire[1].SetActive(true);
        }
        else if (jumpCount == 1) {
            if (type == "Real")
            {
                GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyBulletA");
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.position = new Vector2(transform.position.x, playerLogic.flipPos.y);
                bulletRigid.velocity = new Vector2(arrow, 0) * 5.0f;
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
                sprite.flipX = (1 == arrow) ? true : false;
            }
            fire[0].SetActive(true);
            fire[1].SetActive(false);
        }
    }
    void JigniteReturn() { Jingite = false; }

    void Update()
    {
        if (Time.timeScale != 0) transform.Rotate(Vector3.forward * 10.0f);
        if (rigid.velocity.y < 0) {
            Debug.DrawRay((Vector2)transform.position + Vector2.left, Vector2.down, new Color(1, 0, 0));
            Debug.DrawRay(transform.position, Vector2.down, new Color(1, 0, 0));
            Debug.DrawRay((Vector2)transform.position + Vector2.right, Vector2.down, new Color(1, 0, 0));
            RaycastHit2D rayL = Physics2D.Raycast((Vector2)transform.position + Vector2.left, Vector2.down, 1.5f, LayerMask.GetMask("Box"));
            RaycastHit2D rayC = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, LayerMask.GetMask("Box"));
            RaycastHit2D rayR = Physics2D.Raycast((Vector2)transform.position + Vector2.right, Vector2.down, 1.5f, LayerMask.GetMask("Box"));
            if ((rayL.collider != null) || (rayC.collider != null) || (rayR.collider != null))
            {
                jump("Real");
            }
        }
        fire[0].transform.position = ((Vector2)transform.position + new Vector2(-1.5f, -1.5f));
        fire[1].transform.position = ((Vector2)transform.position + new Vector2(1.5f, -1.5f));
        fire[0].transform.rotation = Quaternion.identity;
        fire[1].transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12) {
            if (collision.gameObject.name.Contains("Left"))
            {
                arrow = 1;
                jump("UnReal");
            }
            else if (collision.gameObject.name.Contains("Right"))
            {
                arrow = -1;
                jump("UnReal");
            }
        }  
    }

    void Clear() {
        ignite[0] = false;
        ignite[1] = false;
        ignite[2] = false;
        ignite[3] = false;
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ignite = new bool[4];
        flipPos = new Vector2[2];
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (leftWeapon != null)
        {
            leftWeapon.SetActive(false);
            rightWeapon.SetActive(false);
        }
        Clear();
    }
}
