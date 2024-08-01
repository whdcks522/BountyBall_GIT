using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TripleEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameObject player;
    public GameManager gamemanager;

    Enemy enemylogic;
    SpriteRenderer spriterenderer;
    Rigidbody2D rigid;
    public string type;
    public GameObject[] partner;
    float time;
    Vector2 vec;
    GameObject venom;
    void Update()
    {
        time += Time.deltaTime;
        if (type == "H") {
            if (time > 0.4f) {
                time = 0.0f;
                transform.DORotate(Vector3.back * 45, 0.15f);
                GameObject bomb = objectmanager.MakeObj("EnemyBombA");
                Bullet bombLogic = bomb.GetComponent<Bullet>();
                bombLogic.gamemanager = gamemanager;
                bomb.transform.position = player.transform.position;
            }
            else if (time > 0.2f)
            {
                transform.DORotate(Vector3.forward * 90, 0.15f);
            }
        }
        else if (type == "S")
        {
            if (Time.timeScale != 0)
            {
                rigid.velocity = (player.transform.position - transform.position).normalized *  1.5f;
                transform.Rotate(Vector3.back * 10);
            }
            if (time > 3.5f)
            {
                time = 0.0f;
                for (float z = -0.3f; z <= 0.3f; z+= 0.3f)
                {
                    GameObject bmr = objectmanager.MakeObj("EnemyBmrA");
                    bmr.transform.position = transform.position;
                    Rigidbody2D bmrRigid = bmr.GetComponent<Rigidbody2D>();
                    Vector2 vec = player.transform.position - transform.position;
                    if(z == -0.3f)bmrRigid.velocity = Vector2.Lerp(vec, new Vector2(vec.y, -vec.x), -z).normalized * 10;
                    else if (z == 0.0f) bmrRigid.velocity = vec.normalized * 10;
                    else if (z == 0.3f) bmrRigid.velocity = Vector2.Lerp(vec, new Vector2(-vec.y, vec.x), z).normalized * 10;
                    Bullet bmrlogic = bmr.GetComponent<Bullet>();
                    bmrlogic.gamemanager = gamemanager;
                    bmrlogic.host = gameObject;
                    bmrlogic.BmrPos = transform.position + new Vector3(bmrRigid.velocity.x, bmrRigid.velocity.y).normalized * 2.5f;
                }
            }
        }
        else if (type == "L")
        {
            if ((time >= 0.0f) && (time < 1.95f))
            {
                vec = (player.transform.position - transform.position).normalized;
                float zValue = Mathf.Atan2(vec.x, vec.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue;
                transform.DORotate(rotVec, 0.1f);

                venom.transform.localScale = new Vector2(time * 0.25f, time * 0.25f);
            }
            else if ((time >= 1.95f) && (time < 2.0f)) {
                transform.rotation = Quaternion.identity;
                vec = (player.transform.position - transform.position).normalized;
                DOTween.Clear();
                float zValue = Mathf.Atan2(vec.x, vec.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue;
                transform.Rotate(rotVec);

                if (venom != null) {
                    venom.transform.localScale = new Vector2(0.5f, 0.5f);
                    venom.GetComponent<Effect>().time = 4;
                    venom.GetComponent<Collider2D>().attachedRigidbody.velocity = -vec.normalized * 8.0f;
                    venom = null;
                }
            }
            else if ((time >= 2.0f) && (time < 3.0f))
            {
                rigid.velocity = vec.normalized * 10.0f;
            }
            else if (time >= 3.0f)
            {
                rigid.velocity = Vector2.zero;
                time = 0.0f;

                venom = objectmanager.MakeObj("Venom");
                venom.GetComponent<Effect>().time = 4;
                venom.transform.position = transform.position;
                venom.transform.localScale = new Vector2(0, 0);
            }
        }
    }


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        partner = new GameObject[2];
        enemylogic = GetComponent<Enemy>();
    }
    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        DOTween.Clear();
        time = 0.0f;
        if (type == "L") time = 3;
    }

    private void OnDisable()
    {
        if (type == "L")
        {
            if(venom != null)venom.SetActive(false);
            CancelInvoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 12) && type == "L")
        {
            rigid.velocity = Vector2.zero;
            time = 0.0f;

            venom = objectmanager.MakeObj("Venom");
            venom.GetComponent<Effect>().time = 4;
            venom.transform.position = transform.position;
            venom.transform.localScale = new Vector2(0, 0);
        }
        else if (collision.gameObject.layer == 9 && enemylogic.health > 0)//총탄 피격시
        {
                for (int i = 0; i <= 1; i++)
                {
                    if (!partner[i].activeSelf) partner[i] = player;
                    GameObject bullet = objectmanager.MakeObj("EnemyInjection");
                    Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    bullet.transform.position = transform.position; ;
                    Vector2 parVec = partner[i].transform.position - transform.position;
                    bulletRigid.velocity = parVec.normalized * 8.0f;
                    float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
                    Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
                    bullet.transform.Rotate(rotVec);
                    Bullet bulletLogic = bullet.GetComponent<Bullet>();
                    bulletLogic.gamemanager = gamemanager;
                    bulletLogic.host = gameObject;
                }
            
        }
    }
}
