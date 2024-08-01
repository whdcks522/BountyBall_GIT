using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomasEnemy : MonoBehaviour
{
    int x;
    int y;
    int j;
    Rigidbody2D rigid;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public GameObject player;
    float time;
    bool showtime;
    int pattern;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void OnEnable()
    {
        x = 1;
        y = 1;
        showtime = false;
        j = 1;
        time = 0.0f;
        pattern = 1;
        rigid.velocity = new Vector2(x, y) * 5.0f;
    }

    void Update()
    {
        //자전
        if((!showtime)&&(Time.deltaTime != 0)) transform.Rotate(Vector3.forward * 3.0f);
        //시간 채우기
        time += Time.deltaTime;
        //쇼타임
        if ((time > 5.0f)&&(!showtime)) ShowTime();
    }

    void ShowTime(){//공격
        //시간 초기화
        time = 0.0f;
        //정지
        rigid.velocity = Vector2.zero;
        //쇼타임 활성화
        showtime = true;
        //패턴 활성화
        if (pattern == 1) Invoke("SpinAttack", 1.0f);
        else if (pattern == 2) Invoke("RocketAttack", 1.0f);
    }

    void RocketAttack()//로켓패턴
    {
        //로켓 생성
        GameObject rocketLL = objectmanager.MakeObj("EnemyRocketA");
        GameObject rocketL = objectmanager.MakeObj("EnemyRocketA");
        GameObject rocketR = objectmanager.MakeObj("EnemyRocketA");
        GameObject rocketRR = objectmanager.MakeObj("EnemyRocketA");
        Bullet rocketLLLogic = rocketLL.GetComponent<Bullet>();
        Bullet rocketLLogic = rocketL.GetComponent<Bullet>();
        Bullet rocketRLogic = rocketR.GetComponent<Bullet>();
        Bullet rocketRRLogic = rocketRR.GetComponent<Bullet>();
        rocketLLLogic.gamemanager = gamemanager;
        rocketLLogic.gamemanager = gamemanager;
        rocketRLogic.gamemanager = gamemanager;
        rocketRRLogic.gamemanager = gamemanager;
        rocketLLLogic.player = player;
        rocketLLogic.player = player;
        rocketRLogic.player = player;
        rocketRRLogic.player = player;
        rocketLL.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.3f);
        rocketL.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y - 0.3f);
        rocketR.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y - 0.3f);
        rocketRR.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.3f);
        Rigidbody2D rocketLLRigid = rocketLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rocketLRigid = rocketL.GetComponent<Rigidbody2D>();
        Rigidbody2D rocketRRigid = rocketR.GetComponent<Rigidbody2D>();
        Rigidbody2D rocketRRRigid = rocketRR.GetComponent<Rigidbody2D>();
        rocketLLRigid.velocity = new Vector2(-3, 3);
        rocketLRigid.velocity = new Vector2(-3, -3);
        rocketRRigid.velocity = new Vector2(3, -3);
        rocketRRRigid.velocity = new Vector2(3, 3);
        //초기화
        Invoke("ShowtimeReturn", 1.0f);
    }

    void SpinAttack( ){//회전패턴
        {
            //난사용 반복문
            for (int i = 0; i < 25; i++)
            {
                //선언
                GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
                Bullet bulletLogic = bullet.GetComponent<Bullet>();
                bulletLogic.gamemanager = gamemanager;
                Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                //힘,방향,위치
                bullet.transform.position = transform.position;
                Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * i/ 25 * 2.0f), Mathf.Cos(Mathf.PI * i/ 25 * 2.0f));
                bulletRigid.AddForce(dirVec.normalized * 5.0f,ForceMode2D.Impulse);
                bullet.transform.Rotate(Vector3.back * 360 *i/ 25 + Vector3.back * 270);
            }
        }
        if (j > 0)
        {
            --j;
            Invoke("SpinAttack", 0.5f);
        }
        else if (j == 0) Invoke("ShowtimeReturn", 1.0f); 
    }

    void ShowtimeReturn()//대기시간
    {
        j = 1;
        showtime = false;
        rigid.velocity = new Vector2(x, y) * 5.0f;
        pattern = (pattern == 1) ? 2 : 1;
    }

    void OnTriggerEnter2D(Collider2D collision)//벽에 충돌시
    {

        if ((collision.gameObject.layer == 12))
        {
            //정지
            rigid.velocity = Vector2.zero;
            //분류
            if (collision.gameObject.name.Contains("Top")) y = (y == 1) ? -1 : 1; //상
            else if (collision.gameObject.name.Contains("Bottom")) y = (y == 1) ? -1 : 1;//하
            else if (collision.gameObject.name.Contains("Left"))x = (x == 1) ? -1 : 1;//좌
            else if (collision.gameObject.name.Contains("Right")) x = (x == 1) ? -1 : 1;//우
            //힘 입력
            rigid.velocity = new Vector2(x, y) * 5.0f;
        }
    }
}
