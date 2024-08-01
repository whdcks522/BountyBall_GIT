using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineEnemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    public GameManager gamemanager;
    int goal;
    public GameObject[] shields;
    float time;
    void Awake()
    {
        anim = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigid.velocity = Vector2.down * 0.1f;
        time = 0.0f;
        goal = -1;
        shields[0].SetActive(true);
        shields[1].SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 4.0f) Shot();
        else if (time > 3.3f) anim.SetTrigger("Action");
        if (goal == -1)
        {
            shields[0].SetActive(true);
            shields[1].SetActive(false);
            spriterenderer.flipX = true;
        }
        else if (goal == 1)
        {
            shields[0].SetActive(false);
            shields[1].SetActive(true);
            spriterenderer.flipX = false;
        }
        rigid.velocity = new Vector2(goal, rigid.velocity.y);
        Vector2 vec = new Vector2(transform.position.x + rigid.velocity.normalized.x * 0.5f, transform.position.y - 1);//!
        Debug.DrawRay(transform.position, new Vector2(rigid.velocity.x, 0)* 0.8f, new Color(1, 0, 0));
        Debug.DrawRay(vec, Vector2.zero, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, Vector2.down * 0.6f, new Color(1, 0, 0));
        RaycastHit2D rayU = Physics2D.Raycast(transform.position, new Vector2(rigid.velocity.x, 0), 0.8f, LayerMask.GetMask("Box"));
        RaycastHit2D rayD = Physics2D.Raycast(vec, Vector2.zero, 1, LayerMask.GetMask("Box"));
        RaycastHit2D rayC = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, LayerMask.GetMask("Box"));
        //회전은 아래가 비거나 위에가 있을때
        if (rayC.collider != null) {
            if ((rayD.collider == null) || (rayU.collider != null)) goal *= -1;
        }
    }

    void Shot() {
        gamemanager.BombStart(transform.position + new Vector3(goal * 0.5f, 0));
        time = 0.0f;
        GameObject rocketL = gamemanager.objectmanager.MakeObj("EnemyRocketA");
        Bullet rocketLLogic = rocketL.GetComponent<Bullet>();
        rocketLLogic.gamemanager = gamemanager;
        rocketLLogic.player = gamemanager.player;
        rocketL.transform.position = transform.position;
        Rigidbody2D rocketLRigid = rocketL.GetComponent<Rigidbody2D>();
        rocketLRigid.velocity = new Vector2(goal * 3, 0.5f);
        goal = 0;
        Invoke("goalReset", 0.5f);
    }

    void goalReset() {
        if (spriterenderer.flipX == true) goal = -1;
        else if (spriterenderer.flipX == false) goal = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12))//환상벽에 충돌시
            goal *= -1;

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
        else if (collision.gameObject.layer == 11)
        {
            gameObject.SetActive(false);
            gamemanager.StageClear();
        }
    }
}
