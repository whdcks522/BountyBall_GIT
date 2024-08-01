using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string rememberType;
    public float rememberGravity;
    public bool isPC;
    //상태이상
    public bool isWater;
    public bool isCold;
    public bool isDead;
    bool isTree;
    bool isTree2;
    public float isVenom;
    public bool isVenom2;
    public GameObject D3sword;
    public Vector2 D3Vec;
    //모바일버튼
    bool isH;
    bool isL;
    bool isR;
    bool isL2;//터치 순간
    bool isR2;//터치 순간
    bool isStop;
    //UI
    public Image[] Buttons;
    public Scrollbar[] ButtonSize;
    public StageController stagecontroller;
    public Vector2 RebornPos;
    public int mana;
    public bool jump;
    public bool ignite;
    //그외
    Vector2 swordVec;
    public Vector2 flipPos;
    GameObject closeEnemy;
    GameObject[] closeEnemys;
    float h;
    public GameManager gamemanager;
    public ObjectManager objectmanager;
    Rigidbody2D rigid;
    
    void Start()
    {
        closeEnemys = new GameObject[20];
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (8.1f<rigid.velocity.y)HighRay();
        else Ray();   
        Stop();
        if ((!isH)&& (!gamemanager.toggles[1].isOn))
        { 
                Buttons[0].color = new Color(1, 1, 1, 0.7f);
                Buttons[1].color = new Color(1, 1, 1, 0.7f);
        }
        HorizontalRay();
        if (isVenom > 0.0f)
        {
            if(Time.timeScale != 0)isVenom -= 0.4f;
            if (isVenom < 0.0f) isVenom = 0.0f;
        }
        if (D3sword != null)
        {
            if (!D3sword.activeSelf)
            {
                D3sword = null;
                return;
            }
            if(Time.timeScale != 0)D3sword.transform.Rotate(Vector3.back * 6);
            D3sword.transform.position = transform.position;
            if ((Input.GetButtonDown("Jump"))&&!ignite)
            {
                gamemanager.SoundPlay("3D");
                Bullet BswordLogic = D3sword.GetComponent<Bullet>();
                BswordLogic.host = gamemanager.player;
                Vector2 swordvec2 = Vector2.Lerp(D3sword.transform.up, new Vector2(-D3sword.transform.up.y, D3sword.transform.up.x), 0.5f);
                D3Vec = swordvec2.normalized;
                Rigidbody2D bulletRigid = D3sword.GetComponent<Rigidbody2D>();
                bulletRigid.velocity = swordvec2.normalized * 15.0f;
                D3sword = null;
            }
        }
    }

    void HorizontalRay()//나무타기용
    {
        if (isTree2 || ignite) return;
        Debug.DrawRay(transform.position, Vector2.left, new Color(0, 1, 0));
        Debug.DrawRay(transform.position, Vector2.right, new Color(0, 1, 0));
        RaycastHit2D rayL = Physics2D.Raycast(transform.position, Vector2.left, 0.35f, LayerMask.GetMask("Box"));
        RaycastHit2D rayR = Physics2D.Raycast(transform.position, Vector2.right, 0.35f, LayerMask.GetMask("Box"));
        if (((rayL.collider != null)) && (rayL.collider.tag == "Tree")&&(rigid.velocity.x <= 0))
        {//좌측점착
            rigid.gravityScale = 0;
            isTree = true;
            isCold = false;
            rigid.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow)||(isL2))
            {
                gamemanager.SoundPlay("TreeJump");
                isTree = false;
                isTree2 = true;
                rigid.gravityScale = rememberGravity;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || (isR2))
            {
                Attack("S");
                if (isVenom2) gamemanager.SoundPlay("VenomIn");
                else if (isWater) gamemanager.SoundPlay("WaterIn");
                else if (rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
                else gamemanager.SoundPlay("StrongJump");
                rigid.velocity = new Vector2(3, 10 * (rememberGravity == -2? -1 : 1));
                isTree = false;
                rigid.gravityScale = rememberGravity;
            }
        }
        else if (((rayR.collider != null)) && (rayR.collider.tag == "Tree") && (rigid.velocity.x >= 0))
        {//우측점착
            rigid.gravityScale = 0;
            isTree = true;
            isCold = false;
            rigid.velocity = Vector2.zero;
            if (Input.GetKeyDown(KeyCode.RightArrow) || (isR2))
            {
                gamemanager.SoundPlay("TreeJump");
                isTree = false;
                isTree2 = true;
                rigid.gravityScale = rememberGravity;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || (isL2)) {
                Attack("S");
                if (isVenom2) gamemanager.SoundPlay("VenomIn");
                else if (isWater) gamemanager.SoundPlay("WaterIn");
                else if (rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
                else gamemanager.SoundPlay("StrongJump");
                rigid.velocity = new Vector2(-3, 10 * (rememberGravity == -2 ? -1 : 1));
                isTree = false;
                rigid.gravityScale = rememberGravity;
            }
        }
        else return; 
    }

    void HighRay()
    {
        Vector2 VecL = new Vector2(transform.position.x - 0.23f, transform.position.y);//23
        Vector2 VecR = new Vector2(transform.position.x + 0.23f, transform.position.y);//23
        Debug.DrawRay(VecL, new Vector2(0, 1 * (rememberGravity == -2 ? -1 : 1)) * 0.4f , new Color(0, 1, 0));
        Debug.DrawRay(VecR, new Vector2(0, 1 * (rememberGravity == -2 ? -1 : 1)) * 0.4f, new Color(0, 1, 0));
        RaycastHit2D rayL = Physics2D.Raycast(VecL, new Vector2(0, 1 * (rememberGravity == -2 ? -1 : 1)), 0.4f, LayerMask.GetMask("Box"));
        RaycastHit2D rayR = Physics2D.Raycast(VecR, new Vector2(0, 1 * (rememberGravity == -2 ? -1 : 1)), 0.4f, LayerMask.GetMask("Box"));
        //한쪽만 있을시
        if (
            ((rayL.collider != null) && (rayR.collider == null) && (0.2f < rayL.distance))
            || 
           ( (rayL.collider == null) && (rayR.collider != null) && (0.2f < rayR.distance))
            )
            rigid.velocity = new Vector2(rigid.velocity.x, 0);     
        //모두 있을시
        else if ((rayL.collider != null) && (rayR.collider != null)) return;
    }

    float raylength = 0.4f;
    void Ray(){
        Vector2 VecL = new Vector2(transform.position.x - 0.2f, transform.position.y);//20
        Vector2 VecC = transform.position;
        Vector2 VecR = new Vector2(transform.position.x + 0.2f, transform.position.y);//20
        Debug.DrawRay(VecL, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)) * raylength, new Color(0, 1, 0));
        Debug.DrawRay(VecC, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)) * raylength, new Color(0, 1, 0));
        Debug.DrawRay(VecR, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)) * raylength, new Color(0, 1, 0));
        RaycastHit2D rayL = Physics2D.Raycast(VecL, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)), raylength, LayerMask.GetMask("Box"));
        RaycastHit2D rayC = Physics2D.Raycast(VecC, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)), raylength, LayerMask.GetMask("Box"));
        RaycastHit2D rayR = Physics2D.Raycast(VecR, new Vector2(0, -1 * (rememberGravity == -2 ? -1 : 1)), raylength, LayerMask.GetMask("Box"));
        //중익
        if ((rayC.collider != null) && (raylength * 0.5f <= rayC.distance))
        {
            Box box = rayC.collider.gameObject.GetComponent<Box>();
            box.Sort();
        }
        //좌익
        else if ((rayL.collider != null) && (raylength * 0.5f <= rayL.distance))
        {
            Box box = rayL.collider.gameObject.GetComponent<Box>();
            box.Sort();
        }
        //우익
        else if ((rayR.collider != null) && (raylength * 0.5f <= rayR.distance))
        {
            Box box = rayR.collider.gameObject.GetComponent<Box>();
            box.Sort();
        }
        else isCold = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
                //10불렛, 12경계, 15에펙트, 16라이트 21아이즈 22안티아이즈 23박스2
            if (collision.gameObject.tag == "Enemy")//적
            {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    gamemanager.DestroyStart(enemy.type, collision.gameObject.transform.position);
                    collision.gameObject.SetActive(false);
                    playerDead();
            }
            else if (collision.gameObject.layer == 10)//적 투사체
            {
                if (collision.gameObject.GetComponent<Bullet>().isWind) {
                    gamemanager.SoundPlay("Wind");
                    rigid.AddForce(collision.gameObject.GetComponent<Rigidbody2D>().velocity * 5, ForceMode2D.Impulse);
                    collision.gameObject.SetActive(false);
                    return;
                }
                collision.gameObject.SetActive(false);
                playerDead();
            }
            else if ((collision.gameObject.layer == 12 && stagecontroller.isFight) ||
                collision.gameObject.layer == 15 || collision.gameObject.layer == 16) playerDead();//적 경계, 라이트, 이펙트
            else if (collision.gameObject.layer == 17)//베놈
            {
                if (collision.gameObject.transform.localScale.x == 1.5f) isVenom += 200;//12짜리
                else if (collision.gameObject.transform.localScale.x == 0.5f) isVenom += 99;//4짜리
                else isVenom += collision.gameObject.transform.localScale.x * 60;//작은거
                if ((isVenom > 100.0f) && (gameObject.layer != 2))//무적상태가 아니라면
                    playerVenomDead();
            }
            else if (collision.gameObject.layer == 21)//아이즈
            {
                if ((rigid.velocity.x >= 0) && (collision.gameObject.transform.position.x > transform.position.x)) playerDead();
                else if ((rigid.velocity.x <= 0) && (collision.gameObject.transform.position.x < transform.position.x)) playerDead();
            }
            else if (collision.gameObject.layer == 22)//안티아이즈
            {
                if ((rigid.velocity.x >= 0) && (collision.gameObject.transform.position.x < transform.position.x)) playerDead();
                else if ((rigid.velocity.x <= 0) && (collision.gameObject.transform.position.x > transform.position.x)) playerDead();
            }
        }
        if (collision.gameObject.layer == 23)//텔레포트
        {
                if (collision.gameObject.tag == "Exit") {
                    gamemanager.SoundPlay("Telepote");
                    transform.position = collision.transform.GetChild(0).transform.position;
                }
                else if (collision.gameObject.tag == "Space")
                {
                gamemanager.SoundPlay("SpaceIn");
                rememberGravity = -2;
                }
        }  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 18)
        {
            isVenom += 1.5f;
            if ((isVenom > 100.0f) && (gameObject.layer != 2)) playerVenomDead();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 23)
        {
            if (collision.gameObject.tag == "Space")
            {
                gamemanager.SoundPlay("SpaceOut");
                rememberGravity = 2;
            }
        }
    }

    void playerVenomDead() {
        gamemanager.ManaControl();
        GameObject VV = objectmanager.MakeObj("Venom");
        VV.transform.position = transform.position;
        VV.GetComponent<Effect>().time = 2.5f;
        VV.GetComponent<Collider2D>().attachedRigidbody.velocity = new Vector2(-rigid.velocity.normalized.x, 1); 
        playerDead();
    }

    void playerDead() {//플레이어 사망시
        if (D3sword != null)
        {
            D3sword.SetActive(false);
            D3sword = null;
        }
        gamemanager.SoundPlay("Dead");
        gamemanager.DestroyStart("P", transform.position);
        Invoke("Reborn", 2.0f);
        gameObject.SetActive(false);
        isDead = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerDead();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            gamemanager.DestroyStart(enemy.type, collision.gameObject.transform.position);
            collision.gameObject.SetActive(false);
        }
    }

    void Stop() {//정지
        //중력 제어
        if (((Input.GetButton("Jump") || (isStop) || (isTree))) && (!ignite))//점화시
        {
            rigid.gravityScale = 0;
            rigid.velocity = Vector2.zero;
            mana -= 4;
        }
        else//평소에
        {
            //중력조절
            rigid.gravityScale = rememberGravity;
            //마나 조절
            if (Time.timeScale == 1)
            {
                if ((mana < 200)) mana += 2;
                if ((ignite)) mana -= 1;
            }
        }
        //마나 만땅
        if (mana >= 200)
        {
            mana = 200;
            ignite = false;
            //과부화 텍스트 제어
            gamemanager.IgniteText.gameObject.SetActive(false);
        }
        //과부화 가동
        else if (mana <= 2)
        {
            //과부화 텍스트 제어
            gamemanager.IgniteText.gameObject.SetActive(true);
            ignite = true;
            isTree = false;
            isTree2 = true;
        }
        gamemanager.ManaControl();//UI 마나 통제
        
    }

    void Move()//이동 함수
    {
        if (isPC)
        {//PC x축 이동
            h = Input.GetAxisRaw("Horizontal");
            if (Input.GetButton("Jump") && (!ignite)) h = 0.0f;
        }
        if (!isPC)//모바일 x축 이동
        {
            if ((isR) && (isH)) h = 1;
            else if ((isL) && (isH)) h = -1;
            else h = 0;
            if ((isStop) && (!ignite)) h = 0.0f;
        }
        if (isTree) h = 0.0f;

        //좌우이동
        if (!isCold) rigid.AddForce(new Vector2(h * 12.0f, 0));
        else if(isCold) rigid.AddForce(new Vector2(h * 24.0f, 0));
        //방향전환시 감속
        if (Input.GetButtonUp("Horizontal")) rigid.velocity = new Vector2(rigid.velocity.x * 0.6f, rigid.velocity.y);
        //x축 최대 속도 제한
        if (!isCold)
        {
            if (Mathf.Abs(rigid.velocity.x) > 5.0f)
            {
                if (rigid.velocity.x > 0)
                    rigid.velocity = new Vector2(5.0f, rigid.velocity.y);
                if (rigid.velocity.x < 0)
                    rigid.velocity = new Vector2(-5.0f, rigid.velocity.y);
            }
        }
        else if (isCold)
        {
            if (Mathf.Abs(rigid.velocity.x) > 10.0f)
            {
                if (rigid.velocity.x > 0)
                    rigid.velocity = new Vector2(10.0f, rigid.velocity.y);
                if (rigid.velocity.x < 0)
                    rigid.velocity = new Vector2(-10.0f, rigid.velocity.y);
            }
        }
    }

    public void Attack(string type)//공격
    {
        if (type == "G")//단발
        {
            for (int i = -1; i < 2; i += 2)
            {
                for (int j = -1; j < 2; j += 2) ThrowSword(new Vector2(i, j), "R", transform.position);
            }
            return;
        }
        else if (type == "3D" || type == "A") return;
        //배열 비우기
        for (int index = 0; index < closeEnemys.Length; index++)
        {
            closeEnemys[index] = null;
        }
        closeEnemy = null;
        //스타트 스테이지 일 경우
        if (gamemanager.StartStage.activeSelf)
        {
            if (stagecontroller.Flag.activeSelf)closeEnemy = stagecontroller.Flag;
            else closeEnemy = null;
        }
        //전투시에
        else if (stagecontroller.isFight)
        {
            closeEnemys = objectmanager.ReturnObjs("Enemy");
            //거리 계산
            float index = 99;
            for (int i = 0; i < closeEnemys.Length; i++)
            {
                if (closeEnemys[i] == null) break;
                else
                {
                    float dir = (transform.position - closeEnemys[i].transform.position).magnitude;
                    if (index > dir) {
                        index = dir;
                        closeEnemy = closeEnemys[i];
                    }
                }
            }

        }
        if (closeEnemy == null) return;
        swordVec = closeEnemy.transform.position - transform.position;
        if ((type == "N") || (type == "H"))//단발
            ThrowSword(swordVec, "R", transform.position);
        else if (type == "S")//세 발
        {
            //좌탄
            if (swordVec.x >= 0.0f && swordVec.y >= 0.0f)//1사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.up, 0.5f), "R", transform.position);
            else if ((swordVec).x < 0.0f && (swordVec).y >= 0.0f)//2사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.left, 0.5f), "R", transform.position);
            else if ((swordVec).x < 0.0f && (swordVec).y < 0.0f)//3사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.down, 0.5f), "R", transform.position);
            else if ((swordVec).x >= 0.0f && (swordVec).y < 0.0f)//4사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.right, 0.5f), "R", transform.position);
            ThrowSword(swordVec, "R", transform.position);//중탄
            //우탄
            if (swordVec.x >= 0.0f && swordVec.y >= 0.0f)//1사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.right, 0.5f),"R", transform.position);
            else if ((swordVec).x < 0.0f && (swordVec).y >= 0.0f)//2사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.up, 0.5f), "R", transform.position);
            else if ((swordVec).x < 0.0f && (swordVec).y < 0.0f)//3사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.left, 0.5f), "R", transform.position);
            else if ((swordVec).x >= 0.0f && (swordVec).y < 0.0f)//4사분면
                ThrowSword(Vector2.Lerp(swordVec, Vector2.down, 0.5f), "R", transform.position);
        }
    }

    public void ThrowSword(Vector2 Vec, string swordType, Vector2 Shotpos)
    {
        if (swordType == "R") swordType = "PlayerBullet";
        GameObject bullet = objectmanager.MakeObj(swordType);
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = Shotpos;
        bulletRigid.velocity = new Vector2(Vec.x, Vec.y).normalized * 15.0f;
            float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
            Vector3 rotVec = Vector3.back * zValue + Vector3.back * 45.0f;
            bullet.transform.Rotate(rotVec);
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
    }

    public void Jump(string type){//점프
        if (jump) return;
        //불값 초기화
        jump = true;
        Invoke("JumpReset", 0.2f);
        //공격
        Attack(type);
        //위치 저장
        flipPos = new Vector2(transform.position.x, transform.position.y - 0.35f * (rememberGravity == -2 ? -1 : 1));
        //점프값 입력
            if (type != "H") rigid.velocity = new Vector2(rigid.velocity.x, 8.0f * (rememberGravity == -2 ? -1 : 1));
            else if (type == "H") rigid.velocity = new Vector2(rigid.velocity.x, 15.0f * (rememberGravity == -2 ? -1 : 1));

    }
    
    public void JumpReset()//점프 불값 초기화
    {
        jump = false;
        isTree = false;
        isTree2 = false;
    }

    void Reborn()//부활
    {
        //상태이상 회복
        RemoveCC();
        //플레이어 활성화
        gameObject.SetActive(true);
            //위치 초기화
            transform.position = RebornPos;
            //빅뱅
            stagecontroller.BigBang();
            stagecontroller.BigBang2();
            //소환
            stagecontroller.SpawnEnemy();
    }

    public void RemoveCC()//상태이상 회복
    {
        //플레이어 초기화
        isWater = false;
        isTree = false;
        isTree2 = false;
        isCold = false;
        isDead = true;
        isVenom = 0.0f;
        isVenom2 = false;
        D3Vec = new Vector2(-1, 1).normalized;
        if (D3sword != null)
        {
            D3sword.SetActive(false);
            D3sword = null;
        }
        rememberGravity = 2;
        //마나 회복
        mana = 200;
        //중력 저장
        rememberGravity = 2;
        rigid.velocity = Vector2.zero;
    }

    public void StopButtonDown()//스톱 버튼 누름
    {
        if (Time.timeScale == 1)
        {
            isStop = true;
            if (D3sword != null)
            {
                if (!D3sword.activeSelf)
                {
                    D3sword = null;
                    return;
                }
                if (!ignite)
                {
                    gamemanager.SoundPlay("3D");
                    Bullet BswordLogic = D3sword.GetComponent<Bullet>();
                    BswordLogic.host = gamemanager.player;
                    Vector2 swordvec2 = Vector2.Lerp(D3sword.transform.up, new Vector2(-D3sword.transform.up.y, D3sword.transform.up.x), 0.5f);
                    D3Vec = swordvec2.normalized;
                    Rigidbody2D bulletRigid = D3sword.GetComponent<Rigidbody2D>();
                    bulletRigid.velocity = swordvec2.normalized * 15.0f;
                    D3sword = null;
                }
            }
        }
    }

    public void StopButtonStay()//스톱 버튼 뗌
    {
        if((ignite) || (Time.timeScale == 0))
        isStop = false;
    }

    public void StopButtonUp()//스톱 버튼 뗌
    {
        isStop = false;
    }
    public void HButtonDown(string arrow)//좌우 버튼 누름
    {
        isH = true;
        switch (arrow)
        {
            case "L":
                isL2 = true;
                Invoke("returnL2",0.1f);
                break;
            case "R":
                isR2 = true;
                Invoke("returnR2", 0.1f);
                break;
        }
    }
    void returnL2() { isL2 = false;}
    void returnR2() { isR2 = false; }
    public void HButtonUp(string arrow)//좌우 버튼 뗌
    {
        isH = false;
        if (!isPC) { rigid.velocity = new Vector2(rigid.velocity.x * 0.6f, rigid.velocity.y); }
    }
    public void HButtonEnter(string arrow)
    {
        switch (arrow)
        {
            case "L":
                isL = true;
                isR = false;
                if (!gamemanager.toggles[1].isOn)
                {
                    Buttons[0].color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
                    Buttons[1].color = new Color(1, 1, 1, 0.7f);
                } break;
            case "R":
                isL = false;
                isR = true;
                if (!gamemanager.toggles[1].isOn)
                {
                    Buttons[0].color = new Color(1, 1, 1, 0.7f);
                    Buttons[1].color = new Color(0.5f, 0.5f, 0.5f, 0.7f);
                }
                break;
        }
    }
}
