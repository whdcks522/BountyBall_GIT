using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashEnemy : MonoBehaviour
{
    int i;
    int j;
    float h;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public GameObject player;
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriterenderer;
    
    void OnEnable()
    {
        CancelInvoke();
    i = 1;
        j = 1;
        h = 0;
        //애니메이션 재생
        anim.SetTrigger("Swap");
        //작동 시작
        Invoke("Telepote",0.15f);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        DOTween.Clear();
        spriterenderer.flipX = false;
        transform.localScale = new Vector2(0.5f, 0.5f);
        CancelInvoke();
    }
    void LRAttack()
    {
        
        //위치 이동
        if (j == 1)transform.position = new Vector2(0, 2);
        //좌우 흔들기
        spriterenderer.flipX = (spriterenderer.flipX == true) ? false : true;
        //인수 더하기
        ++j;
        //발사
        switch (j%7)
        {
            case 1:
                h = -7;
                break;
            case 2:
                h = -4;
                break;
            case 3:
                h = -2;
                 break;
            case 4:
                GameObject fakecanon = objectmanager.MakeObj("EnemyCanonA");
                fakecanon.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
                Rigidbody2D fakebulletRigid = fakecanon.GetComponent<Rigidbody2D>();
                Bullet fakecononlogic = fakecanon.GetComponent<Bullet>();
                fakecononlogic.gamemanager = gamemanager;
                fakebulletRigid.velocity = new Vector2(-0.3f, 3);

                h = 0.3f;
                GameObject bulletL = objectmanager.MakeObj("EnemyBulletA");
                GameObject bulletR = objectmanager.MakeObj("EnemyBulletA");
                Bullet bulletLLogic = bulletL.GetComponent<Bullet>();
                Bullet bulletRLogic = bulletR.GetComponent<Bullet>();
                Rigidbody2D bulletLRigid = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D bulletRRigid = bulletR.GetComponent<Rigidbody2D>();

                SpriteRenderer spriteL = bulletL.GetComponent<SpriteRenderer>();
                spriteL.flipX = false;
                SpriteRenderer spriteR = bulletR.GetComponent<SpriteRenderer>();
                spriteR.flipX = true;
                bulletLLogic.gamemanager = gamemanager;
                bulletRLogic.gamemanager = gamemanager;
                bulletL.transform.position = transform.position;
                bulletR.transform.position = transform.position;
                bulletLRigid.velocity = Vector2.left * 5.0f;
                bulletRRigid.velocity = Vector2.right * 5.0f;
                break;
            case 5:
                h = 2;
                break;
            case 6:
                h = 4;
                break;
            case 0:
                h = 7;
                break;
        }
        GameObject canon = objectmanager.MakeObj("EnemyCanonA");
        canon.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        Rigidbody2D bulletRigid = canon.GetComponent<Rigidbody2D>();
        Bullet cononLogic = canon.GetComponent<Bullet>();
        cononLogic.gamemanager = gamemanager;
        bulletRigid.velocity = new Vector2(h, 3);
        if (j < 28) Invoke("LRAttack", 0.3f);
        else if (j >= 28)
        {
            ++i;
            anim.SetTrigger("Swap");
            Telepote();
        }
    }

    void HAttack()
    {
        
            GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            //힘,방향,위치
            bullet.transform.position = new Vector2(transform.position.x - 1.5f, transform.position.y-0.1f);
            Vector2 dirVec = new Vector2(-1, (Mathf.Cos(Mathf.PI * j / 30 * 2.0f)) * 2/3);
            bulletRigid.AddForce(dirVec.normalized * 5.0f, ForceMode2D.Impulse);
            float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
            bullet.transform.Rotate(rotVec);

        j++;
        if (j < 30) Invoke("HAttack", 0.2f);
        else if (j >= 30)
        {
            ++i;
            transform.DORotate(Vector3.forward * 180, 1.5f);
            Invoke("Spin",2);
        }
    }

    void NAttack()
    {
        if (-10 <= transform.position.x)
        {
            GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
            Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
            Bullet niddleLogic = niddle.GetComponent<Bullet>();
            niddleLogic.gamemanager = gamemanager;
            niddle.transform.position = new Vector2(transform.position.x, transform.position.y - 1.5f);
            niddleRigid.velocity = Vector2.down * 15.0f;
            float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
            niddle.transform.Rotate(rotVec);
            Invoke("NAttack", 0.4f);//0.8
        }
        else if ((-12 < transform.position.x) && (transform.position.x < -10)) Invoke("NAttack", 0.8f);
        else if (transform.position.x <= -12)
        {
            ++i;
            transform.DORotate(Vector3.back * 30, 1.5f);
            transform.DOMoveY(-1,1.5f);
            Invoke("Telepote",1.5f);
        }
    }

    void CAttack()
    {
        transform.DOMoveX(-8,2);
        ++i;
        Invoke("Telepote",2);
    }

    void RAttack()
    {
 
               GameObject rocketR = objectmanager.MakeObj("EnemyRocketA");
               Bullet rocketRLogic = rocketR.GetComponent<Bullet>();
               rocketRLogic.gamemanager = gamemanager;
               rocketRLogic.player = player;
               rocketR.transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1.5f);
               Rigidbody2D rocketRRigid = rocketR.GetComponent<Rigidbody2D>();
               rocketRRigid.velocity = new Vector2(1,0.5f);
        if (j < 4)
        {
            ++j;
            Invoke("RAttack", 1f);
        }
        else if (j >= 4)
        {
            transform.DORotate(Vector2.zero, 0.5f);
            //작동 시작
            Invoke("Spin", 4);
        }
    }

    void Spin()
    {
        switch (i)
        {
            case 2:
                transform.DORotate(Vector3.forward * 90, 1.5f);
                transform.position = new Vector2(8,0);
                Invoke("HAttack",2f);
                break;
            case 3:
                anim.SetTrigger("Swap");
                Invoke("Telepote", 0.3f);
                break;
            case 5:
                anim.SetTrigger("Swap");
                i = 1;
                Invoke("Telepote", 0.3f);
                break;
        }

        
    }
    void Telepote()
    {
        //인수 초기화
        j = 1;
        //애니메이션 재생
        if((i != 4)&&(i != 5)) anim.SetTrigger("Unswap");
        //실질적 시작
        switch (i)
        {
            case 1:
                Invoke("LRAttack", 0.15f);
                break;
            case 2:
                spriterenderer.flipX = true;
                Invoke("Spin",0.15f);
                break;
            case 3:
                transform.position = new Vector2(8, 4);
                transform.DOMoveX(-12, 6);
                Invoke("NAttack", 0.15f);
                break;
            case 4:
                transform.DOMoveX(8, 1);
                Invoke("CAttack",1);
                break;
            case 5:
                Invoke("RAttack",1);
                break;
        }
    }
    
}
