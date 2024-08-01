using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public bool isElemental;
    public Image BossHp;
    int maxHealth;
    public int health;
    public string type;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    public GameManager gamemanager;
    
    void Awake()
    {
        maxHealth = health;
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        if (BossHp != null) BossHp = GetComponent<Image>();
    }

    void OnDisable()
    {
        transform.rotation = Quaternion.identity;
        if (BossHp != null) BossHp.fillAmount = 0;
    }

    private void OnEnable()
    {
        health = maxHealth;
        Invoke("SetbossHP", 0.01f);
    }

    void SetbossHP() {
        if (BossHp != null) BossHp.fillAmount = 1;
    }

     void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.layer == 9)//총탄 피격시
        {
            if ((type != "S2") && (type != "BO") && (type != "M"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                HealthControl(bullet.dmg);
                collision.gameObject.SetActive(false);
            }
            if (type == "BR")
            {
                GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyCanonA");
                bullet.transform.position = transform.position;
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                bulletRigid.velocity = new Vector2(0, 8);
            }
            if (BossHp != null) BossHp.fillAmount = health * 1.0f / maxHealth;
        }
        else if (collision.gameObject.layer == 10){//총탄 피격시
            Bullet BigBulletLogic = collision.GetComponent<Bullet>();
            if ((BigBulletLogic.isInjection)&&(BigBulletLogic.host != gameObject))
            {
                collision.gameObject.SetActive(false);
                gamemanager.DestroyStart("EnemyInjection", collision.transform.position);
                HealthControl(collision.GetComponent<Bullet>().dmg);
                if (BossHp != null)
                    BossHp.fillAmount = health * 1.0f / maxHealth;
            }       
        }
     }

    public void HealthControl(int dmg)
    {
        health -= dmg;
        if (health > maxHealth) health = maxHealth;
        else if (health <= 0){
            gamemanager.DestroyStart(type, transform.position);
            gameObject.SetActive(false);
            gamemanager.StageClear();
        }
        else
        {
            spriterenderer.color = new Color(0, 0, 0, 0.5f);
            Invoke("ColorBack", 0.2f);
        }
    }

    void ColorBack()
    {
        spriterenderer.color = new Color(1, 1, 1, 1);
    }

    
}