using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //스테이지----------
    //다중이동 금지용(ExitStage)
    public bool isbreak;  
    //Effect,Player 참조
    public bool isCreating;
    public bool isCreating2;
    public GameObject StartStage;
    public GameObject GoggleStage;
    //오리진
    public GameObject WorldSelectStage;
    public GameObject[] World1;
    public GameObject[] World2;
    public GameObject[] World3;
    public GameObject[] World4;
    public GameObject[] World5;
    public GameObject[] World6;
    public GameObject[] World7;
    public GameObject[] World8;
    public GameObject[] World9;
    public GameObject[] World10;
    public GameObject[] World11;
    public GameObject[] World12;
    public GameObject[] World13;
    public ObjectManager objectmanager;
    public StageController stagecontroller;
    //UI----------------
    public Image realMana;
    public Text IgniteText;
    public Text RememberText;
    public Animator CreatingText;
    public GameObject grayout;
    public Animator clearstageanim;
    public int rememberW;
    public int rememberS;
    public Animator rememberAnim;
    public GameObject positionsee;
    public Toggle[] toggles;
    int autoStartnum;
    int PBnum;
    public Image RealVenomWord;
    public Image VenomWord;
    //플레이어-------------
    public GameObject player;
    Player playerLogic;
    //오디오
    public AudioSource[] soundPlayer;

    void Start()
    {
        playerLogic = player.GetComponent<Player>();
        //전투시 부활 위치
        positionsee.SetActive(false);
        //시작 스테이지 자동 시작
        StartStage.SetActive(true);
        if (PlayerPrefs.HasKey("AutoStartNum"))
        {
            autoStartnum = PlayerPrefs.GetInt("AutoStartNum");
            if (autoStartnum == 1) toggles[0].isOn = true;
            else if (autoStartnum == 0) toggles[0].isOn = false;
        }
        if (PlayerPrefs.HasKey("PBNum"))
        {
            PBnum = PlayerPrefs.GetInt("PBNum");
            if (PBnum == 1) toggles[1].isOn = true;
            else if (PBnum == 0) toggles[1].isOn = false;
        }
        if (PlayerPrefs.HasKey("LRSize"))
        {
            playerLogic.ButtonSize[0].value = PlayerPrefs.GetFloat("LRSize");;
            LRSize("S");
        }
        if (PlayerPrefs.HasKey("SSize"))
        {  
            playerLogic.ButtonSize[1].value = PlayerPrefs.GetFloat("SSize");
            SSize();
        }
        Invoke("CompleteShock", 10f);
    }

    private void CompleteShock() {
        if (GooglePlayServiceManager.Instance.isAuthenticated)
        {
            GooglePlayServiceManager.Instance.Completeshock_and_awe();
        }
    }

    private void Awake()
    {
        GooglePlayServiceManager.Instance.Login();
    }

    private void OnApplicationPause(bool pause)
    {
        if ((pause == true)&&!isCreating)
        {
            //시간 비활성화
            Time.timeScale = 0;
            //그레이 아웃 활성화
            grayout.SetActive(true);
        }
    }

    public void BreakEnemy()//적을 모두 격추하고 나감
    {
        //중복 제거 + 스테이지 컨트롤러 맨 밑
        if (isbreak) return;
            isbreak = true;
        stagecontroller.BigBang2();
        isCreating = false;
        //통상 클리어
        if ((stagecontroller.stage >= rememberS) && (stagecontroller.world == rememberW))
        {
            rememberS = stagecontroller.stage + 1;
        }
        //보스 클리어
        if (rememberS == 21)
        {
            rememberW = ++rememberW;
            rememberS = 1;
        }
        //저장
        PlayerPrefs.SetInt("world",rememberW);
        PlayerPrefs.SetInt("stage", rememberS);
        PlayerPrefs.Save();
        //탈출 분기점
        if (toggles[0].isOn) stagecontroller.NextStage();
        else if (!toggles[0].isOn)
        {//리멤버 활성화
            rememberAnim.SetBool("Bool", true);
            RememberText.text = "Save) World " + rememberW + " - Stage " + rememberS;
            stagecontroller.ExitGame();
        }
    }

    public void DestroyStart(string type, Vector3 pos)
    {//파괴
        GameObject destroy = objectmanager.MakeObj("Destroy");
        Effect effect = destroy.GetComponent<Effect>();
        destroy.transform.position = pos;
        effect.DestroyEffect(type);
    }

    public void BombStart(Vector3 pos)
    {//폭발
        GameObject bombeffect = objectmanager.MakeObj("BombEffect");
        Animator bombanim = bombeffect.GetComponent<Animator>();
        bombanim.SetTrigger("Action");
        bombeffect.transform.position = pos;
        bombeffect.transform.Rotate(Vector3.forward * Random.Range(0, 361));
    }

    public void GenerateStart(Vector3 pos, int size = 3)
    {//생성
        GameObject generateeffect = objectmanager.MakeObj("GenerateEffect");
        Animator generateanim = generateeffect.GetComponent<Animator>();
        generateanim.SetTrigger("Generate");
        generateeffect.transform.position = pos;
        generateeffect.transform.localScale = new Vector2(size, size);
    }

    public void ManaControl()//UI 제어
    {
        //베놈 조정
        VenomWord.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + new Vector2(0, 1.5f * (playerLogic.rememberGravity == -2 ? -1 : 1));
        RealVenomWord.transform.position = new Vector2(player.transform.position.x, player.transform.position.y) + new Vector2(0, 1.5f *(playerLogic.rememberGravity == -2 ? -1 : 1));
        if (playerLogic.isVenom <= 0.0f) VenomWord.fillAmount = 0;
        else VenomWord.fillAmount = 1;
        RealVenomWord.fillAmount = playerLogic.isVenom / 100.0f;

        realMana.fillAmount = playerLogic.mana / 200.0f;
        //마나 색 변화
        if(playerLogic.mana<=50)realMana.color = new Color(0.5f,0.5f,0.5f);
        else realMana.color = new Color(1, 1, 1);
    }

    public void Pause()//정지 조작
    {
        if (isCreating)
        {
            CreatingText.SetTrigger("Creating");
            return;
        }
        if (!grayout.activeSelf)
        {
            //시간 비활성화
            Time.timeScale = 0;
            //그레이 아웃 활성화
            grayout.SetActive(true);  
        }
        else if (grayout.activeSelf)
        {
            //시간 활성화
            Time.timeScale = 1;
            //그레이 아웃 비활성화
            grayout.SetActive(false); 
        }
    }

    public void QuitGame()//게임 종료
    {
        Application.Quit();
    }

    public void ReviewSite()
    {//리뷰하러하기
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.StudyCompany.BountyBall");
    }

    public void LRSize(string type)//좌우버튼
    {
        RectTransform B0 = playerLogic.Buttons[0].GetComponent<RectTransform>();
        RectTransform B1 = playerLogic.Buttons[1].GetComponent<RectTransform>();
        if (!toggles[1].isOn) {//버튼(기본값 로드)
            B0.anchoredPosition = new Vector2(75 + playerLogic.ButtonSize[0].value * 50, 90);
            B1.anchoredPosition = new Vector2(215 + playerLogic.ButtonSize[0].value * 125, 90);

            B0.gameObject.transform.localScale = new Vector2(1 + playerLogic.ButtonSize[0].value * 0.5f, 1);
            B1.gameObject.transform.localScale = new Vector2(1 + playerLogic.ButtonSize[0].value * 0.5f, 1);
        }
        else if (toggles[1].isOn) {//패널(조정값 로드)
            B0.anchoredPosition = new Vector2(350, 450);
            B1.anchoredPosition = new Vector2(1550, 450);

            B0.gameObject.transform.localScale = new Vector2(8, 5);
            B1.gameObject.transform.localScale = new Vector2(8, 5);

            playerLogic.Buttons[0].color = new Color(1, 1, 1, 0.0f);
            playerLogic.Buttons[1].color = new Color(1, 1, 1, 0.0f);
        }
        if (type == "N") return;
        PlayerPrefs.SetFloat("LRSize", playerLogic.ButtonSize[0].value);
        Debug.Log(playerLogic.ButtonSize[0].value);//---------------------------
        PlayerPrefs.Save();
    }

    public void SSize()//S버튼
    {
        RectTransform B2 = playerLogic.Buttons[2].GetComponent<RectTransform>();
        B2.anchoredPosition = new Vector2(-50 - playerLogic.ButtonSize[1].value * 100, 50 + playerLogic.ButtonSize[1].value * 100);
        B2.gameObject.transform.localScale = new Vector2(0.5f + playerLogic.ButtonSize[1].value, 0.5f + playerLogic.ButtonSize[1].value);
        PlayerPrefs.SetFloat("SSize", playerLogic.ButtonSize[1].value);
        PlayerPrefs.Save();
    }

    public void AutoStart()//자동시작체크
    {
        if (toggles[0].isOn) autoStartnum = 1;
        else if (!toggles[0].isOn) autoStartnum = 0;
        PlayerPrefs.SetInt("AutoStartNum", autoStartnum);
        PlayerPrefs.Save();
    }

    public void PannelButton()//패널버튼
    {
        if (!toggles[1].isOn) PBnum = 0;//버튼
        else if (toggles[1].isOn) PBnum = 1;//패널
        LRSize("N");
        PlayerPrefs.SetInt("PBNum", PBnum);
        PlayerPrefs.Save();
    }

    public void StageClear()
    {
        GameObject[] ClearEnemy = objectmanager.ReturnObjs("Enemy");
        if ((ClearEnemy[0] == null) && (player.gameObject.activeSelf))
        {
            isCreating = true;
            playerLogic.RebornPos = stagecontroller.WorldPos;
            stagecontroller.BigBang();
            clearstageanim.SetTrigger("SC");
            Invoke("BreakEnemy",2);
        }
    }

    public void SoundPlay(string type)
    {
        switch (type)
        {     
            case "NormalJump": //일반점프
                soundPlayer[0].Play();
                break;
            case "HighJump"://고공점프
                soundPlayer[1].Play();
                break;
            case "WaterIn"://입수
                soundPlayer[2].Play();
                break;
            case "WaterOut"://출수
                soundPlayer[3].Play();
                break;
            case "Dead"://죽음
                soundPlayer[4].Play();
                break;
            case "StrongJump"://강력점프
                soundPlayer[5].Play();
                break;
            case "TreeJump"://나무점프
                soundPlayer[6].Play();
                break;
            case "Telepote"://순간이동
                soundPlayer[7].Play();
                break;
            case "Absolute"://절대벽
                soundPlayer[8].Play();
                break;
            case "WaterFlip"://물벽
                soundPlayer[9].Play();
                break;
            case "Glass"://유리벽
                soundPlayer[10].Play();
                break;
            case "Genoside"://학살
                soundPlayer[11].Play();
                break;
            case "Cold"://얼음
                soundPlayer[12].Play();
                break;
            case "Cure"://치료
                soundPlayer[13].Play();
                break;
            case "VenomIn"://독들어감
                soundPlayer[14].Play();
                break;
            case "VenomOut"://독나옴
                soundPlayer[15].Play();
                break;
            case "BecomeSword"://검화
                soundPlayer[16].Play();
                break;
            case "Exhaust"://탈진
                soundPlayer[17].Play();
                break;
            case "3D"://입체기동
                soundPlayer[18].Play();
                break;
            case "Bee"://벌
                soundPlayer[19].Play();
                break;
            case "SpaceIn"://우주인
                soundPlayer[20].Play();
                break;
            case "SpaceOut"://우주아웃
                soundPlayer[21].Play();
                break;
            case "Wind"://바람
                soundPlayer[22].Play();
                break;
        }
    }
}
