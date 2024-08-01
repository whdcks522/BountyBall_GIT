using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public bool isWorld;
    public bool isFight;
    public int world;
    public int stage;
    public long goggleNum;
    public Vector2 WorldPos;
    public Text stagetext;
    public Player player;
    public GameManager gamemanager;
    public GameObject Flag;
    Rigidbody2D rigid;
    //적용
    GameObject[] generate;
    //박스용
    GameObject[] generate2;

    static private StageController instance;
    public static StageController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<StageController>();
                if (instance == null)
                {
                    instance = new GameObject("StageController").AddComponent<StageController>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        generate = new GameObject[20];
        generate2 = new GameObject[40];
        rigid = GetComponent<Rigidbody2D>();
    }

    public void BigBang()//에네미와 탄환 전부 파괴
    { 
        //불렛 파괴
        GameObject[] bullet = gamemanager.objectmanager.ReturnObjs("Bullet");
        for (int pb = 0; pb < bullet.Length; pb++)
        {
            if (bullet[pb] == null) break;
           bullet[pb].SetActive(false);
        }
        //에네미 파괴
        GameObject[] enemy = gamemanager.objectmanager.ReturnObjs("Enemy");
        for (int e = 0; e < enemy.Length; e++)
        {
            if (enemy[e] == null) break;
            enemy[e].SetActive(false);
        }
    }

    public void BigBang2()
    {
        //일회용 벽 파괴
        GameObject[] box = gamemanager.objectmanager.ReturnObjs("Box");
        for (int k = 0; k < box.Length; k++)
        {
            if (box[k] == null) break;
            box[k].SetActive(false);
        }
        //이펙트 파괴
        GameObject[] effect = gamemanager.objectmanager.ReturnObjs("Effect");
        for (int e = 0; e < effect.Length; e++)
        {
            if (effect[e] == null) break;
            effect[e].SetActive(false);
        }
    }
    public void ExitGame()//게임 나가기
    {
        //부활 위치 초기화
        player.RebornPos = WorldPos;
        //그레이 아웃 제거
        if (gamemanager.grayout.activeSelf) gamemanager.grayout.SetActive(false);
        //시간 초기화
        if (Time.timeScale == 0) Time.timeScale = 1;
        //순간이동 사운드
        gamemanager.SoundPlay("Telepote");
        if (gamemanager.StartStage.activeSelf)//스타트 스테이지에서
        {
            //스테이지 텍스트 초기화
            stagetext.text = "Home";
        }
        else if (gamemanager.GoggleStage.activeSelf)//구글 스테이지에서
        {
            //스테이지 텍스트 초기화
            stagetext.text = "Home";
            //스타트 스테이지 활성화
            gamemanager.StartStage.SetActive(true);
            //월드 셀렉트 스테이지 비활성화
            gamemanager.GoggleStage.SetActive(false);
            //불값 초기화
            isFight = false;
        }
        else if (gamemanager.WorldSelectStage.activeSelf)//월드 셀렉트 스테이지
        {

            //스테이지 텍스트 활성화
            stagetext.text = "Home";
            //리멤버 텍스트 초기화
            gamemanager.rememberAnim.SetBool("Bool", false);
            //스타트 스테이지 활성화
            gamemanager.StartStage.SetActive(true);
            //월드 셀렉트 스테이지 비활성화
            gamemanager.WorldSelectStage.SetActive(false);
        }
        else if (isWorld)//월드 스테이지에서
        {
            //플레이어 초기화
            player.RemoveCC();
            //월드 스테이지 비활성화
            OriginControl(0, false);
            //인수 초기화
            world = 0;
            //배경색 변경
            ChangeBackground(world);
            //스테이지 텍스트 초기화
            stagetext.text = "World Select";
            //월드 셀렉트 스테이지 활성화
            gamemanager.WorldSelectStage.SetActive(true);
        }
        else if (isFight)//경기중에서
        {
            //플레이어 초기화
            player.RemoveCC();
            //월드 스테이지 비활성화
            OriginControl(stage, false);
            //인수 초기화
            stage = 0;
            //스테이지 텍스트 초기화
            stagetext.text = "World " + world;
            //리멤버 텍스트 활성화
            if (!gamemanager.rememberAnim.GetBool("Bool")) gamemanager.rememberAnim.SetBool("Bool", true);
            //월드 셀렉트 스테이지 활성화=
            OriginControl(0, true);
            //불값 초기화
            isFight = false;
            //빅뱅 활성화
            BigBang();
            BigBang2();
        }
        //플레이어 위치 조정
        transform.position = WorldPos;
        //플레이어 속도 조정
        rigid.velocity = Vector2.down;
    }

    public void NextStage()
    {
        //순간이동 사운드
        gamemanager.SoundPlay("Telepote");
        //스테이지 비활성화
        OriginControl(stage, false);
        ++stage;
        //보스 클리어시
        if (stage == 21)
        {
            ++world;
            ChangeBackground(world);
            stage = 1;
            //변경시 참조--------------------------------------------------------------------------------------------------
            if (world == 14)
            {
                isWorld = true;
                isFight = false;
                gamemanager.rememberAnim.SetBool("Bool", true);
                gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;
                ExitGame();
                return;
            }
        }
        EnterStage();
    }

    public void SpawnEnemy() {//소환
        if (isWorld) return;
        for (int i = 0; i < 99; i++) {
            generate[i] = GameObject.Find(world + "-" + stage + "-" + i);
            if (generate[i] == null) break;
            Create effect = generate[i].GetComponent<Create>();
            effect.GenerateEffect();
        }
        for (int i = 0; i < 99; i++)
        {
            if (generate[i] == null) break;
            generate[i] = null;
        }

        for (int i = 0; i < 99; i++)
        {
            generate2[i] = GameObject.Find(world + "=" + stage + "=" + i);
            if (generate2[i] == null) break;
            Create effect = generate2[i].GetComponent<Create>();
            effect.GenerateBox();
        }
        for (int i = 0; i < 99; i++)
        {
            if (generate2[i] == null) break;
            generate2[i] = null;
        }
    }

    /*
    public void SpawnBox()
    {
        if (isWorld) return;
    }
    */
    void  OriginControl(int j, bool active)//오리진을 조정한다.
    {
        if (j == 0) {
            if (active) isWorld = true;
            else if (!active) isWorld = false;
        }
        switch (world)
        {
            case 1:
                gamemanager.World1[j].SetActive(active);
                break;
            case 2:
                gamemanager.World2[j].SetActive(active);
                break;
            case 3:
                gamemanager.World3[j].SetActive(active);
                break;
            case 4:
                gamemanager.World4[j].SetActive(active);
                break;
            case 5:
                gamemanager.World5[j].SetActive(active);
                break;
            case 6:
                gamemanager.World6[j].SetActive(active);
                break;
            case 7:
                gamemanager.World7[j].SetActive(active);
                break;
            case 8:
                gamemanager.World8[j].SetActive(active);
                break;
            case 9:
                gamemanager.World9[j].SetActive(active);
                break;
            case 10:
                gamemanager.World10[j].SetActive(active);
                break;
            case 11:
                gamemanager.World11[j].SetActive(active);
                break;
            case 12:
                gamemanager.World12[j].SetActive(active);
                break;
            case 13:
                gamemanager.World13[j].SetActive(active);
                break;
        }
    }

    void ChangeBackground(int type)//배경색 변경
    {
        switch (type)
        {
            case 0:
                Camera.main.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
                break;
            case 1:
                Camera.main.backgroundColor = new Color(0.5f, 0.3f, 0.3f);
                break;
            case 2:
                Camera.main.backgroundColor = new Color(0.8f, 0.6f, 0.4f);
                break;
            case 3:
                Camera.main.backgroundColor = new Color(0.35f, 0.5f, 0.3f);
                break;
            case 4:
                Camera.main.backgroundColor = new Color(0.4f, 0.25f, 0.2f);
                break;
            case 5:
                Camera.main.backgroundColor = new Color(0.53f, 0.7f, 0.85f);
                break;
            case 6:
                Camera.main.backgroundColor = new Color(0.41f, 0.45f, 0.51f);
                break;
            case 7:
                Camera.main.backgroundColor = new Color(0.45f, 0.3f, 0.5f);
                break;
            case 8:
                 Camera.main.backgroundColor = new Color(0, 0, 0);
                break;
            case 9:
                Camera.main.backgroundColor = new Color(0.4f, 0.4f, 0);
                break;
            case 10:
                Camera.main.backgroundColor = new Color(0.83f, 0.63f, 0.83f);
                break;
            case 11:
                Camera.main.backgroundColor = new Color(0.69f, 0.84f, 0.62f);
                break;
            case 12:
                Camera.main.backgroundColor = new Color(0.84f, 0.68f, 0.37f);
                break;
            case 13:
                Camera.main.backgroundColor = new Color(0.28f, 0.22f, 0.32f);
                break;
        }
    }

    void ChangePlayerPos(int type)//플레이어 위치 조정
    {
        switch (type)
        {
            case 1:
                transform.position = new Vector2(-7.5f, 2.5f);
                break;
            case 2:
                transform.position = new Vector2(0, 2.5f);
                break;
            case 3:
                transform.position = new Vector2(7.5f, 2.5f);
                break;
            case 4:
                transform.position = new Vector2(-7.5f, 0);
                break;
            case 5:
                transform.position = new Vector2(0, 0);
                break;
            case 6:
                transform.position = new Vector2(7.5f, 0);
                break;
            case 7:
                transform.position = new Vector2(-7.5f, -2.5f);
                break;
            case 8:
                transform.position = new Vector2(0, -2.5f);
                break;
            case 9:
                transform.position = new Vector2(7.5f, -2.5f);
                break;
            case 10:
                transform.position = new Vector2(0, 0);
                break;
            case 11:
                transform.position = new Vector2(-7.5f, 2.5f);
                break;
            case 12:
                transform.position = new Vector2(0, 2.5f);
                break;
            case 13:
                transform.position = new Vector2(7.5f, 2.5f);
                break;
            case 14:
                transform.position = new Vector2(-7.5f, 0);
                break;
            case 15:
                transform.position = new Vector2(0, 0);
                break;
            case 16:
                transform.position = new Vector2(7.5f, 0);
                break;
            case 17:
                transform.position = new Vector2(-7.5f, -2.5f);
                break;
            case 18:
                transform.position = new Vector2(0, -2.5f);
                break;
            case 19:
                transform.position = new Vector2(7.5f, -2.5f);
                break;
            case 20:
                transform.position = new Vector2(0, 0);
                break;
        }
    }

    public void EnterStage()
    {
        //스테이지 활성화
        OriginControl(stage, true);
        //스테이지 텍스트 변화
        stagetext.text = "World " + world + " - " + stage;
        gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;
        //생성 활성화
        SpawnEnemy();
        //SpawnBox();
        //플레이어 위치 이동
        ChangePlayerPos(stage);
        //플레이어 위치 저장
        player.RebornPos = transform.position;
        //플레이어 상태 정상화
        player.RemoveCC();
        //다중이동 금지용
        Invoke("ReturnIsbreak", 0.1f);
    }
    void ReturnIsbreak() { gamemanager.isbreak = false; }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            //순간이동 사운드
            gamemanager.SoundPlay("Telepote");
            if (collision.gameObject.name.Contains("Bottom"))//하측 충돌시
            {
                //아래 가속
                rigid.velocity = Vector2.down;
                if (gamemanager.StartStage.activeSelf)//스타트 활성화 상태에서 충돌
                {
                    //저장 불러오기
                    if (PlayerPrefs.HasKey("world"))
                    {
                        int a = PlayerPrefs.GetInt("world");
                        int b = PlayerPrefs.GetInt("stage");
                        gamemanager.rememberW = a;
                        gamemanager.rememberS = b;
                    }
                    //세이브 보여주기
                    gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;
                    gamemanager.rememberAnim.SetBool("Bool", true);
                    //스타트 스테이지 비활성화
                    gamemanager.StartStage.SetActive(false);
                    //월드 스테이지 활성화
                    if ((-2 < transform.position.x) && (transform.position.x < 2))
                    {
                        //월드셀렉트 활성화
                        gamemanager.WorldSelectStage.SetActive(true);
                        //스테이지 텍스트 변화
                        stagetext.text = "World Select";
                        transform.position = WorldPos;
                    }

                }
                else if (gamemanager.WorldSelectStage.activeSelf)//월드 셀렉트 활성화 상태에서 충돌
                {
                    if (transform.position.y < -3.5f) {
                        if ((-9 < transform.position.x) && (transform.position.x < -8)) world = 1;
                        else if ((-7 < transform.position.x) && (transform.position.x < -6)) world = 2;
                        else if ((-5 < transform.position.x) && (transform.position.x < -4)) world = 3;
                        else if ((-3 < transform.position.x) && (transform.position.x < -2)) world = 4;
                        else if ((2 < transform.position.x) && (transform.position.x < 3)) world = 5;
                        else if ((4 < transform.position.x) && (transform.position.x < 5)) world = 6;
                        else if ((6 < transform.position.x) && (transform.position.x < 7)) world = 7;
                        else if ((8 < transform.position.x) && (transform.position.x < 9)) world = 8;
                    }
                    else if (transform.position.y < -0.5f)
                    {
                        if ((-9 < transform.position.x) && (transform.position.x < -8)) world = 9;
                        else if ((-7 < transform.position.x) && (transform.position.x < -6)) world = 10;
                        else if ((-5 < transform.position.x) && (transform.position.x < -4)) world = 11;
                        else if ((-3 < transform.position.x) && (transform.position.x < -2)) world = 12;
                        else if ((2 < transform.position.x) && (transform.position.x < 3)) world = 13;
                        else if ((4 < transform.position.x) && (transform.position.x < 5)) world = 14;
                        else if ((6 < transform.position.x) && (transform.position.x < 7)) world = 15;
                        else if ((8 < transform.position.x) && (transform.position.x < 9)) world = 16;
                    }
                    if (gamemanager.rememberW < world)
                    {
                        world = 0;
                        transform.position = WorldPos;
                        return;
                    }
                    //배경색 변경
                    ChangeBackground(world);
                    //스타트 스테이지 비활성화
                    gamemanager.WorldSelectStage.SetActive(false);
                    //월드 스테이지 선택
                    OriginControl(0, true);
                    //플레이어 위치 설정
                    transform.position = WorldPos;
                    //스테이지 텍스트 변화
                    stagetext.text = "World " + world;
                }
                else if (isWorld)//월드 활성화 상태에서 충돌
                {
                    if ((-9 < transform.position.x) && (transform.position.x < -8) && (transform.position.y < -3)) stage = 1;
                    else if ((-7 < transform.position.x) && (transform.position.x < -6) && (transform.position.y < -3)) stage = 2;
                    else if ((-5 < transform.position.x) && (transform.position.x < -4) && (transform.position.y < -3)) stage = 3;
                    else if ((-3 < transform.position.x) && (transform.position.x < -2) && (transform.position.y < -3)) stage = 4;
                    else if ((2 < transform.position.x) && (transform.position.x < 3) && (transform.position.y < -3)) stage = 5;
                    else if ((4 < transform.position.x) && (transform.position.x < 5) && (transform.position.y < -3)) stage = 6;
                    else if ((6 < transform.position.x) && (transform.position.x < 7) && (transform.position.y < -3)) stage = 7;
                    else if ((8 < transform.position.x) && (transform.position.x < 9) && (transform.position.y < -3)) stage = 8;
                    else if ((-9 < transform.position.x) && (transform.position.x < -8)
                        && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 9;
                    else if ((-7 < transform.position.x) && (transform.position.x < -6)
                    && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 10;
                    else if ((-5 < transform.position.x) && (transform.position.x < -4)
                    && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 11;
                    else if ((4 < transform.position.x) && (transform.position.x < 5)
                    && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 12;
                    else if ((6 < transform.position.x) && (transform.position.x < 7)
                        && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 13;
                    else if ((8 < transform.position.x) && (transform.position.x < 9)
                        && (transform.position.y < -0.5f) && (transform.position.y > -2)) stage = 14;
                    else if ((-9 < transform.position.x) && (transform.position.x < -8) && (transform.position.y > 1)) stage = 15;
                    else if ((-7 < transform.position.x) && (transform.position.x < -6) && (transform.position.y > 1)) stage = 16;
                    else if ((-5 < transform.position.x) && (transform.position.x < -4) && (transform.position.y > 1)) stage = 17;
                    else if ((4 < transform.position.x) && (transform.position.x < 5) && (transform.position.y > 1)) stage = 18;
                    else if ((6 < transform.position.x) && (transform.position.x < 7) && (transform.position.y > 1)) stage = 19;
                    else if ((8 < transform.position.x) && (transform.position.x < 9) && (transform.position.y > 1)) stage = 20;
                    if ((gamemanager.rememberW <= world) && (gamemanager.rememberS < stage))
                    {
                        stage = 0;
                        transform.position = WorldPos;
                        return;
                    }
                    //불값 활성화
                    Invoke("ReturnIsfight", 0.1f);
                    //월드 스테이지 비활성화
                    OriginControl(0, false);
                    //던전 입장
                    EnterStage();
                    //리멤버 비활성화
                    gamemanager.rememberAnim.SetBool("Bool", false);
                }//World 에서 충돌
            }//아래 충돌
            if (collision.gameObject.name.Contains("Left"))transform.position = new Vector2(12, transform.position.y);
            else if (collision.gameObject.name.Contains("Right")) transform.position = new Vector2(-12, transform.position.y);
        }//레이어 확인
        else if ((collision.gameObject.layer == 12))//스타트 스테이지에서 충돌
        {
            //스타트 스테이지가 켜져 있다면
            if (gamemanager.StartStage.activeSelf) {
                if (collision.gameObject.name.Contains("Left"))//빠른 시작
                {
                    //순간이동 사운드
                    gamemanager.SoundPlay("Telepote");
                    //저장 불러오기
                    if (PlayerPrefs.HasKey("world"))
                    {
                        int a = PlayerPrefs.GetInt("world");
                        int b = PlayerPrefs.GetInt("stage");
                        gamemanager.rememberW = a;
                        gamemanager.rememberS = b;
                    }
                    //변경시 참조---------------------------------------------------------------------------------------------------
                    if (gamemanager.rememberW == 14)
                    {
                        transform.position = WorldPos;
                        return;
                    }
                    //무기파괴
                    BigBang();
                    //월드 수치 조정
                    world = gamemanager.rememberW;
                    stage = gamemanager.rememberS;
                    //불값 활성화
                    Invoke("ReturnIsfight", 0.1f);
                    //스타트 스테이지 비활성화
                    gamemanager.StartStage.SetActive(false);
                    //배경색 변화
                    ChangeBackground(world);
                    //던전 입장
                    EnterStage();
                }
                else if (collision.gameObject.name.Contains("Right")&&rigid.velocity.x > 0)//구글 스테이지 들어가기
                {
                    //순간이동 사운드
                    gamemanager.SoundPlay("Telepote");
                    stagetext.text = "Data";
                    //무기파괴
                    BigBang();
                    //저장 불러오기
                    if (PlayerPrefs.HasKey("world"))
                    {
                        int a = PlayerPrefs.GetInt("world");
                        int b = PlayerPrefs.GetInt("stage");
                        gamemanager.rememberW = a;
                        gamemanager.rememberS = b;
                    }
                    //세이브 보여주기
                    gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;
                    gamemanager.rememberAnim.SetBool("Bool", true);
                    //스타트 스테이지 비활성화
                    transform.position = new Vector2(-transform.position.x, transform.position.y);
                    gamemanager.StartStage.SetActive(false);
                    gamemanager.GoggleStage.SetActive(true);
                    gamemanager.isCreating = true;
                    player.GetComponent<Player>().mana = 0;
                    goggleNum = 100 * gamemanager.rememberW + gamemanager.rememberS;
                }
            }
            else if (gamemanager.GoggleStage.activeSelf)//구글 스테이지 나가기
            {
                //순간이동 사운드
                gamemanager.SoundPlay("Telepote");
                if (collision.gameObject.name.Contains("Left") && rigid.velocity.x < 0)//좌측 충돌
                {
                    gamemanager.rememberAnim.SetBool("Bool", false);
                    //메인 텍스트 초기화
                    stagetext.text = "Home";
                    transform.position = new Vector2(-transform.position.x, transform.position.y);
                    gamemanager.StartStage.SetActive(true);
                    gamemanager.GoggleStage.SetActive(false);
                    gamemanager.isCreating = false;
                }
                else if (collision.gameObject.name.Contains("Bottom"))//하측 충돌
                {
                    player.GetComponent<Player>().mana = 0; 
                    if ((7 < transform.position.x) && (transform.position.x < 8))//개발자 비밀
                    {
                        gamemanager.rememberW = 13;
                        gamemanager.rememberS = 20;
                        PlayerPrefs.SetInt("world", gamemanager.rememberW);
                        PlayerPrefs.SetInt("stage", gamemanager.rememberS);
                        PlayerPrefs.Save();
                    }
                    transform.position = WorldPos;
                    //세이브 보여주기
                    gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;
                }
            }
        }//레이어
    }//트리거 충돌

    public void IncreaseWorld() //월드 증가 후 저장
    {
        gamemanager.rememberW++;
        if (gamemanager.rememberW >= 14)
        {
            gamemanager.rememberW = 1;
        }
        gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;

        PlayerPrefs.SetInt("world", gamemanager.rememberW);
        PlayerPrefs.SetInt("stage", gamemanager.rememberS);
        PlayerPrefs.Save();
    }
    public void IncreaseStage()//스테이지 증가 후 저장
    {
        gamemanager.rememberS++;
        if (gamemanager.rememberS >= 21)
        {
            gamemanager.rememberS = 1;
        }
        gamemanager.RememberText.text = "Save) World " + gamemanager.rememberW + " - Stage " + gamemanager.rememberS;

        PlayerPrefs.SetInt("world", gamemanager.rememberW);
        PlayerPrefs.SetInt("stage", gamemanager.rememberS);
        PlayerPrefs.Save();
    }
    void ReturnIsfight() { isFight = true; }
}//클래스



