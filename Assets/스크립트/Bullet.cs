using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float time;
    public bool isRot;
    public bool isFollow;
    public bool isNiddle;
    public bool isBomb;
    //부메랑
    public bool isBmr;
    public GameObject host;
    public Vector3 BmrPos;

    public bool isExcel;
    public bool isCircle;
    public bool isGas;
    public bool isBall;
    public bool isInjection;
    public bool isIce;
    public bool isWind;
    public bool isMeteo;
    bool isUnbreakable;

    public int dmg;
    int i;
    public GameObject player;
    Player playerlogic;
    SpriteRenderer spriterenderer;
    public GameManager gamemanager;
    Rigidbody2D rigid;
    void Start()
    {
        playerlogic = gamemanager.player.GetComponent<Player>();
        spriterenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        time = 0.0f;
        if (isBmr)
        {
            i = 0;
            CancelInvoke();
        }
        else if (dmg == 30)
        {
            if (playerlogic == null) return;
            if (playerlogic.D3sword != null && playerlogic.D3sword == gameObject)
            {
                Vector2 swordvec2 = Vector2.Lerp(playerlogic.D3sword.transform.up, new Vector2(-playerlogic.D3sword.transform.up.y, playerlogic.D3sword.transform.up.x), 0.5f);
                playerlogic.D3Vec = swordvec2.normalized;
                host = null;
                gamemanager.SoundPlay("3D");
            }
        }
        else if (isWind) isUnbreakable = false;
        transform.rotation = Quaternion.identity;
    }
    void ReturnBmr() { i = 2; }
    void Update()
    {
        if (isRot && (Time.timeScale != 0))//회전
            transform.Rotate(Vector3.forward * 10.0f);
        if (isFollow)
        {//추적
            if (Time.timeScale == 0) return;
            time += Time.deltaTime;
            if ((time > 6.0f) && (isFollow))
            {
                gamemanager.DestroyStart("D", transform.position);
                gameObject.SetActive(false);
            }
            transform.rotation = Quaternion.identity;
            Vector2 A = transform.position;
            Vector2 B = player.transform.position;
            rigid.velocity += (B - A).normalized * 5.5f * Time.deltaTime;
            float zValue = Mathf.Atan2(rigid.velocity.x, rigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
            transform.Rotate(rotVec);
        }
        //가시
        else if (isNiddle)
        {
            time += Time.deltaTime;
            if (time > 4.0f) {
                gamemanager.DestroyStart("D", transform.position);
                gameObject.SetActive(false);
            }
        }
        //폭발물
        else if (isBomb)
        {
            time += Time.deltaTime;
            if (time > 1)
            {
                gamemanager.BombStart(transform.position);
                gameObject.SetActive(false);
            }
        }
        else if (isBmr)
        {//부메랑
            if (Time.timeScale != 0) transform.Rotate(Vector3.forward * 20.0f);
            //가는 중
            if (i == 0)
            {
                if ((BmrPos - transform.position).magnitude < 0.5f) i = 1;
            }
            else if (i == 1)
            {//멈추는 중
                rigid.velocity = Vector2.zero;
                Invoke("ReturnBmr", 0.5f);
            }
            //오는 중
            else if (i == 2)
            {
                rigid.velocity = new Vector2(host.transform.position.x - transform.position.x, host.transform.position.y - transform.position.y).normalized * 7.0f;
                if ((host.transform.position - transform.position).magnitude < 0.5f)
                {
                    if (host.gameObject.name.Contains("Indian Slime")) host.GetComponent<IndianEnemy>().Action();
                    gameObject.SetActive(false);
                }
            }
        }
        else if (isExcel)
        {//엑셀
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                isExcel = false;
                gamemanager.BombStart(transform.position);
                Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y) * 5.0f;
            }
        }
        else if (isCircle)
        {
            if (time > 0.5f)
            {
                Debug.DrawRay(transform.position, rigid.velocity.normalized * time, new Color(1, 0, 0));
                RaycastHit2D ray = Physics2D.Raycast(transform.position, rigid.velocity.normalized, time, LayerMask.GetMask("Box"));
                if (ray.collider != null)
                {
                    GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyCircle");
                    Bullet bulletLogic = bullet.GetComponent<Bullet>();
                    bulletLogic.gamemanager = gamemanager;
                    bulletLogic.isCircle = false;
                    Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    //힘,방향,위치
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                    bulletRigid.velocity = new Vector2(-rigid.velocity.y, -rigid.velocity.x);

                    rigid.velocity = new Vector2(rigid.velocity.y, rigid.velocity.x);
                    isCircle = false;
                }
            }

            else if (time == 0.5f)
            {
                Debug.DrawRay(player.transform.position, rigid.velocity.normalized * time, new Color(1, 0, 0));
                Debug.DrawRay(host.transform.position, rigid.velocity.normalized * time, new Color(1, 0, 0));
                RaycastHit2D rayP = Physics2D.Raycast(player.transform.position, rigid.velocity.normalized, time, LayerMask.GetMask("Box"));
                RaycastHit2D rayH = Physics2D.Raycast(host.transform.position, rigid.velocity.normalized, time, LayerMask.GetMask("Box"));
                if ((rayP.collider != null) && (rayH.collider != null))
                {
                    GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyCircle");
                    Bullet bulletLogic = bullet.GetComponent<Bullet>();
                    bulletLogic.gamemanager = gamemanager;
                    bulletLogic.isCircle = false;
                    Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    //힘,방향,위치
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                    bulletRigid.velocity = new Vector2(-rigid.velocity.y, -rigid.velocity.x);

                    rigid.velocity = new Vector2(rigid.velocity.y, rigid.velocity.x);
                    isCircle = false;
                }
            }
        }
        else if (isGas)
        {
            time += Time.deltaTime;
            if (time < 0.3f) return;
            time = 0.0f;
            GameObject VV = gamemanager.objectmanager.MakeObj("Venom");
            VV.transform.position = transform.position;
            while (true)
            {
                dmg = Random.Range(-3, 4);
                i = Random.Range(-3, 4);
                if ((dmg != 0) && (i != 0)) break;
            }
            VV.GetComponent<Effect>().time = Random.Range(4, 7) / 2.0f;
            VV.GetComponent<Collider2D>().attachedRigidbody.velocity = new Vector2(dmg / 3.0f, i / 3.0f) * 2.0f;
        }
        else if (isBall)
        {
            time += Time.deltaTime;
            if (time > 4.0f)
            {
                gamemanager.DestroyStart("D", transform.position);
                gameObject.SetActive(false);
            }
            Debug.DrawRay((Vector2)transform.position + Vector2.left * 0.75f, Vector2.down * 0.75f, new Color(1, 0, 0));
            Debug.DrawRay((Vector2)transform.position + Vector2.right * 0.75f, Vector2.down * 0.75f, new Color(1, 0, 0));
            RaycastHit2D rayL = Physics2D.Raycast((Vector2)transform.position + Vector2.left * 0.75f, Vector2.down * 0.75f, 0.75f, LayerMask.GetMask("Box"));
            RaycastHit2D rayR = Physics2D.Raycast((Vector2)transform.position + Vector2.right * 0.75f, Vector2.down * 0.75f, 0.75f, LayerMask.GetMask("Box"));
            if ((rayL.collider != null) || (rayR.collider != null))
            {
                if (rigid.velocity.x > 0) rigid.velocity = new Vector2(dmg, 10);
                else rigid.velocity = new Vector2(dmg, 8);
                gameObject.GetComponent<Animator>().SetTrigger("Action");
            }
        }
        else if (isIce)
        {//얼음
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = 0.25f;
                gamemanager.BombStart(transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //총탄벽
        if (collision.gameObject.layer == 11 && !isUnbreakable)
        {
            gameObject.SetActive(false);
        }
        //절대벽
        else if (collision.gameObject.name.Contains("Absolute"))
        {
            if (dmg == 30) {
                gamemanager.DestroyStart("F", (Vector2)transform.position + rigid.velocity.normalized);
            }
            else gamemanager.DestroyStart("F", transform.position);
            gameObject.SetActive(false);
        }
        else if (gameObject.layer == 10)
        {//가시
            if ((collision.gameObject.layer == 8) && (isNiddle))
            {
                host = collision.gameObject;
                rigid.velocity = Vector2.zero;
            }
            else if (collision.gameObject.layer == 20)
            {//검이 되어라
                gameObject.SetActive(false);
                Vector2 dir = (Vector2)(transform.position - collision.transform.position);
                playerlogic.ThrowSword((Vector2)(gameObject.transform.position - collision.gameObject.transform.position), "R", transform.position);
                gamemanager.DestroyStart("D", transform.position);
            }
            else if ((collision.gameObject.layer == 8) && (isGas))
            {//가스
                rigid.gravityScale = 0;
                isRot = false;
                rigid.velocity = Vector2.zero;
            }
            else if ((collision.gameObject.layer == 12) && (isIce))
            {//얼음
                gamemanager.SoundPlay("Glass");
                Vector2 icevec = -rigid.velocity.normalized;
                gameObject.SetActive(false);
                for (float i = -0.6f; i <= 0.6f; i += 0.3f) {
                    GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyBulletD");
                    Bullet bulletLogic = bullet.GetComponent<Bullet>();
                    bulletLogic.gamemanager = gamemanager;
                    bulletLogic.isIce = false;
                    Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    if (i < 0) { bulletRigid.velocity = Vector2.Lerp(icevec, new Vector2(-icevec.y, icevec.x), -i).normalized * 8; }
                    else if (i == 0) bulletRigid.velocity = icevec * 8;
                    else if (i > 0) { bulletRigid.velocity = Vector2.Lerp(icevec, new Vector2(icevec.y, -icevec.x), i).normalized * 8; }
                    //힘,방향,위치
                    bullet.transform.position = transform.position;
                    float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
                    Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
                    bullet.transform.Rotate(rotVec);
                }
            }
            else if ((collision.gameObject.layer == 9 || collision.gameObject.layer == 10) && isMeteo) {
                collision.gameObject.SetActive(false);
                gamemanager.DestroyStart("D", collision.transform.position);
            }
        }
        else if (gameObject.layer == 9)
        {
             if (collision.gameObject.layer == 4)
             { //물
                rigid.velocity = new Vector2(rigid.velocity.x * 1 / 3f, rigid.velocity.y * 1 / 3);
             }
        else if (collision.gameObject.tag == "Bee")
        {
                string color = "";
                int colorcount = 0;
                if (dmg == 10)
                {
                    gamemanager.SoundPlay("Bee");
                    colorcount = 3;
                    color = "R";
                }
                else if ((dmg == 30)&&(host == gamemanager.player))
                {
                    gamemanager.SoundPlay("Bee");
                    colorcount = 2;
                    color = "B";
                }
                while (colorcount > 0) {
                    colorcount--;
                        GameObject bullet = null;
                        if (color == "R") bullet = gamemanager.objectmanager.MakeObj("EnemyBulletB");
                        else if (color == "B") bullet = gamemanager.objectmanager.MakeObj("EnemyBulletD");
                        Bullet bulletLogic = bullet.GetComponent<Bullet>();
                        bulletLogic.gamemanager = gamemanager;
                    if (color == "B")
                    {
                        bulletLogic.isIce = false;
                        bulletLogic.IceReturn();
                    }
                        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    if (dmg == 10) {
                        bullet.transform.position = transform.position;
                        gamemanager.DestroyStart("D", transform.position);
                    }
                    else if (dmg == 30) {
                        bullet.transform.position = (Vector2)transform.position + rigid.velocity.normalized;
                        gamemanager.DestroyStart("D", (Vector2)transform.position + rigid.velocity.normalized);
                    }
                        int r = Random.Range(0, 51);
                        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * r * 2.0f / 50), Mathf.Cos(Mathf.PI * r * 2.0f / 50));
                        bulletRigid.velocity = dirVec.normalized * 8.0f;
                        bullet.transform.Rotate(Vector3.back * 360 * r / 50 + Vector3.forward * 90);
                }  
        }
        if (((collision.gameObject.layer == 12) || (collision.gameObject.layer == 8)) && (dmg == 30) && (host == player)&&(player.activeSelf))
        {//와이어  
                Vector2 realVec = (Vector2)transform.position + rigid.velocity.normalized;
                gamemanager.DestroyStart("D", realVec);
                Vector2 wireVec = (realVec - (Vector2)player.transform.position).normalized;
                player.GetComponent<Rigidbody2D>().velocity = wireVec * 16;
                Player playerLogic = player.GetComponent<Player>();
                playerLogic.ThrowSword(Vector2.Lerp(-wireVec, new Vector2(-wireVec.y, wireVec.x), 0.1f), "R", realVec);
                playerLogic.ThrowSword(Vector2.Lerp(-wireVec, new Vector2(wireVec.y, -wireVec.x), 0.1f), "R", realVec);
                gameObject.SetActive(false);
        }
        }
    }
    void IceReturn() { Invoke("IceReturn2",0.1f); }
    void IceReturn2() { isIce = true; }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 4) && (gameObject.layer == 9))//물
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y) * 3;
        }
    }

    public void Breakable() {
        isUnbreakable = true;
        Invoke("Breakable2", 1);
    }

    void Breakable2()
    {
        isUnbreakable = false;
    }
}
