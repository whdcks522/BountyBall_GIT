using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create : MonoBehaviour
{
    public Image BossHp;
    public Image BossHp2;
    public Image BossHp3;
    public GameManager gamemanager;
    Animator anim;
    public string type;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void isCreating2Set() {
        gamemanager.isCreating = false;
        gamemanager.isCreating2 = false;
    }

    public void GenerateEffect(){
        anim.SetTrigger("Generate");
        if (!gamemanager.isCreating) gamemanager.isCreating = true;
        if (!gamemanager.isCreating2) gamemanager.isCreating2 = true;
        Invoke("isCreating2Set", 0.8f);
        switch (type)
        {
            case "D":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("DummyEnemys", 0.8f);
                break;
            case "F":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("FollowEnemys", 0.8f);
                break;
            case "S":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("SlimeEnemys", 0.8f);
                break;
            case "E":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("ExcelEnemys", 0.8f);
                break;
            case "BD":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("TomasEnemys", 0.8f);
                break;
            case "C":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("CaterEnemys", 0.8f);
                break;
            case "BT":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("TrashEnemys", 0.8f);
                break;
            case "B":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("BirdEnemys", 0.8f);
                break;
            case "S2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("ShieldEnemys", 0.8f);
                break;
            case "BI":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("InfinityEnemys", 0.8f);
                break;
            case "BO":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("OldEnemys", 0.8f);
                break;
            case "W":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("WoodEnemys", 0.8f);
                break;
            case "D2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("DebilEnemys", 0.8f);
                break;
            case "BZ":
                transform.localScale = Vector3.one * 10.0f;
                Invoke("ZealEnemys", 0.8f);
                break;
            case "W2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("WildEnemys", 0.8f);
                break;
            case "I":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("IndianEnemys", 0.8f);
                break;
            case "BR":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("RemoteEnemys", 0.8f);
                break;
            case "E2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("ElectricEnemys", 0.8f);
                break;
            case "M":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("MachineEnemys", 0.8f);
                break;
            case "BU":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("UfoEnemys", 0.8f);
                break;
            case "V":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("VenomEnemys", 0.8f);
                break;
            case "T":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("ThiefEnemys", 0.8f);
                break;
            case "BB":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("BoneEnemys", 0.8f);
                break;
            case "C2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("CueEnemys", 0.8f);
                break;
            case "L":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("LeeEnemys", 0.8f);
                break;
            case "BF":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("FanEnemys", 0.8f);
                break;
            case "ZFE":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("ZombieFEEnemys", 0.8f);
                break;
            case "BS":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("SportEnemys", 0.8f);
                break;
            case "E3":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("EngelEnemys", 0.8f);
                break;
            case "BT2H":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("TripleEnemyHs", 0.8f);
                break;
            case "BT2S":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("TripleEnemySs", 0.8f);
                break;
            case "BT2L":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("TripleEnemyLs", 0.8f);
                break;
            case "BT2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("TripleEnemys", 0.8f);
                break;
            case "L2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("LaserEnemys", 0.8f);
                break;
            case "BH":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("HelicopterEnemys", 0.8f);
                break;
            case "B2":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("BeeEnemys", 0.8f);
                break;
            case "BQ":
                transform.localScale = Vector3.one * 3.0f;
                Invoke("QueenEnemys", 0.8f);
                break;
            case "BM":
                transform.localScale = Vector3.one * 5.0f;
                Invoke("MoonEnemys", 0.8f);
                break;
        }
    }

    public void GenerateBox()
    {
        if (type.Contains("M"))
        {
            GameObject movebox = gamemanager.objectmanager.MakeObj("MoveBox");
            Box mlogic = movebox.GetComponent<Box>();
            Rigidbody2D mrigid = movebox.GetComponent<Rigidbody2D>();
            BoxCollider2D boxc = movebox.GetComponent<BoxCollider2D>();
            mlogic.gamemanager = gamemanager;
            movebox.transform.position = transform.position;
            int size = int.Parse(type.Split('=')[1]);
            if (size <= -1)
            {
                movebox.GetComponent<SpriteRenderer>().size = new Vector2(1, -size);
                boxc.size = new Vector2(0.83f, -size - 0.17f);
            }
            else if (size >= 1)
            {
                movebox.GetComponent<SpriteRenderer>().size = new Vector2(size, 1);
                boxc.size = new Vector2(size - 0.17f, 0.83f);
            }
            switch (type.Split('=')[2])
            {
                case "U":
                    mrigid.velocity = Vector2.up * 3;
                    break;
                case "D":
                    mrigid.velocity = Vector2.down * 3;
                    break;      
                case "L":
                    mrigid.velocity = Vector2.left * 3;
                    break;
                case "R":
                    mrigid.velocity = Vector2.right * 3;
                    break;
            }
            mlogic.HitPosSave[0] = mrigid.velocity;
            return;
        }
        switch (type)
        {
            case "F":
                GameObject fakebox = gamemanager.objectmanager.MakeObj("FakeBox");
                Box fakeboxlogic = fakebox.GetComponent<Box>();
                fakeboxlogic.gamemanager = gamemanager;
                fakeboxlogic.blockNum = gamemanager.stagecontroller.stage;
                fakebox.transform.position = transform.position;
                break;
        }
    }
    void DummyEnemys()
    {//더미 소환
        GameObject dummyEnemy = gamemanager.objectmanager.MakeObj("DummyEnemy");
        Enemy enemy = dummyEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        //위치 이동
        dummyEnemy.transform.position = transform.position;
    }

    void SlimeEnemys()
    {//슬라임 소환
        GameObject slimeEnemy = gamemanager.objectmanager.MakeObj("SlimeEnemy");
        Enemy enemy = slimeEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        SlimeEnemy slimeEnemyLogic = slimeEnemy.GetComponent<SlimeEnemy>();
        slimeEnemyLogic.objectmanager = gamemanager.objectmanager;
        slimeEnemyLogic.player = gamemanager.player;
        slimeEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        slimeEnemy.transform.position = transform.position; 
    }

    void FollowEnemys()
    {//추적자 소환
        GameObject followEnemy = gamemanager.objectmanager.MakeObj("FollowEnemy");
        Enemy enemy = followEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        FollowEnemy followEnemyLogic = followEnemy.GetComponent<FollowEnemy>();
        followEnemyLogic.player = gamemanager.player;
        //위치 이동
        followEnemy.transform.position = transform.position;
    }

    void ExcelEnemys()
    {//가속자 소환
        GameObject excelEnemy = gamemanager.objectmanager.MakeObj("ExcelEnemy");
        Enemy enemy = excelEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ExcelEnemy excelEnemyLogic = excelEnemy.GetComponent<ExcelEnemy>();
        excelEnemyLogic.player = gamemanager.player;
        //위치 이동
        excelEnemy.transform.position = transform.position;
    }

    void TomasEnemys()
    {//토마스 소환
        GameObject tomasEnemy = gamemanager.objectmanager.MakeObj("TomasEnemy");
        Enemy enemy = tomasEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        TomasEnemy tomasEnemyLogic = tomasEnemy.GetComponent<TomasEnemy>();
        tomasEnemyLogic.objectmanager = gamemanager.objectmanager;
        tomasEnemyLogic.player = gamemanager.player;
        tomasEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        tomasEnemy.transform.position = transform.position; 
    }

    void CaterEnemys()
    {
        GameObject caterEnemy = gamemanager.objectmanager.MakeObj("CaterEnemy");
        Enemy enemy = caterEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        CaterEnemy caterEnemyLogic = caterEnemy.GetComponent<CaterEnemy>();
        caterEnemyLogic.objectmanager = gamemanager.objectmanager;
        caterEnemyLogic.player = gamemanager.player;
        caterEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        caterEnemy.transform.position = transform.position;
    }

    void TrashEnemys()
    {//쓰레기통 소환
        GameObject trashEnemy = gamemanager.objectmanager.MakeObj("TrashEnemy");
        Enemy enemy = trashEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        TrashEnemy logic = trashEnemy.GetComponent<TrashEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        trashEnemy.transform.position = transform.position;
    }

    void BirdEnemys()
    {
        GameObject birdEnemy = gamemanager.objectmanager.MakeObj("BirdEnemy");
        Enemy enemy = birdEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        BirdEnemy birdEnemyLogic = birdEnemy.GetComponent<BirdEnemy>();
        birdEnemyLogic.objectmanager = gamemanager.objectmanager;
        birdEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        birdEnemy.transform.position = transform.position;
    }

    void ShieldEnemys()
    {//쉴더 소환
        GameObject shieldEnemy = gamemanager.objectmanager.MakeObj("ShieldEnemy");
        Enemy enemy = shieldEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ShieldEnemy shieldEnemyLogic = shieldEnemy.GetComponent<ShieldEnemy>();
        shieldEnemyLogic.player = gamemanager.player;
        shieldEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        shieldEnemy.transform.position = transform.position;
    }

    void InfinityEnemys()
    {//해파리
        GameObject infinityEnemy = gamemanager.objectmanager.MakeObj("InfinityEnemy");
        Enemy enemy = infinityEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        InfinityEnemy logic = infinityEnemy.GetComponent<InfinityEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        infinityEnemy.transform.position = transform.position;
    }

    void OldEnemys()
    {//거북이
        GameObject oldEnemy = gamemanager.objectmanager.MakeObj("OldEnemy");
        Enemy enemy = oldEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        OldEnemy logic = oldEnemy.GetComponent<OldEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        oldEnemy.transform.position = transform.position;
    }

    void WoodEnemys()
    {//벌목자 소환
        GameObject woodEnemy = gamemanager.objectmanager.MakeObj("WoodEnemy");
        Enemy enemy = woodEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        WoodEnemy woodEnemyLogic = woodEnemy.GetComponent<WoodEnemy>();
        woodEnemyLogic.player = gamemanager.player;
        woodEnemyLogic.objectmanager = gamemanager.objectmanager;
        woodEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        woodEnemy.transform.position = transform.position;
    }

    void DebilEnemys()
    {//악마 소환
        GameObject debilEnemy = gamemanager.objectmanager.MakeObj("DebilEnemy");
        Enemy enemy = debilEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        DebilEnemy debilEnemyLogic = debilEnemy.GetComponent<DebilEnemy>();
        debilEnemyLogic.player = gamemanager.player;
        debilEnemyLogic.objectmanager = gamemanager.objectmanager;
        debilEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        debilEnemy.transform.position = transform.position;
    }

    void ZealEnemys()
    {//주님
        GameObject zealEnemy = gamemanager.objectmanager.MakeObj("ZealEnemy");
        Enemy enemy = zealEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        ZealEnemy logic = zealEnemy.GetComponent<ZealEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        zealEnemy.transform.position = transform.position;
    }

    void WildEnemys()
    {//짐승 소환
        GameObject wildEnemy = gamemanager.objectmanager.MakeObj("WildEnemy");
        FollowEnemy follow = wildEnemy.GetComponent<FollowEnemy>();
        follow.player = gamemanager.player;
        Enemy enemy = wildEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        GameObject warningcircle = gamemanager.objectmanager.MakeObj("WarningCircle");
        WildEnemy wildEnemyLogic = warningcircle.GetComponent<WildEnemy>();
        wildEnemyLogic.host = wildEnemy;
        //위치 이동
        wildEnemy.transform.position = transform.position;
        warningcircle.transform.position = transform.position;
    }

    void IndianEnemys()
    {//인디언 소환
        GameObject indianEnemy = gamemanager.objectmanager.MakeObj("IndianEnemy");
        Enemy enemy = indianEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        IndianEnemy indianEnemylogic = indianEnemy.GetComponent<IndianEnemy>();
        indianEnemylogic.player = gamemanager.player;
        indianEnemylogic.gamemanager = gamemanager;
        indianEnemylogic.objectmanager = gamemanager.objectmanager;
        //위치 이동
        indianEnemy.transform.position = transform.position;
    }

    void RemoteEnemys() {//리모컨 소환
        GameObject remoteEnemy = gamemanager.objectmanager.MakeObj("RemoteEnemy");
        Enemy enemy = remoteEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        GameObject warningRectangle = gamemanager.objectmanager.MakeObj("WarningRectangle");
        RemoteEnemy warningRectangleLogic = warningRectangle.GetComponent<RemoteEnemy>();
        warningRectangleLogic.host = remoteEnemy;
        warningRectangleLogic.player = gamemanager.player;
        warningRectangleLogic.gamemanager = gamemanager;
        warningRectangleLogic.objectmanager = gamemanager.objectmanager;
        //위치 이동
        remoteEnemy.transform.position = transform.position;
        warningRectangle.transform.position = transform.position;
    }

    void ElectricEnemys() {//전기구체 소환
        GameObject electricEnemy = gamemanager.objectmanager.MakeObj("ElectricEnemy");
        Enemy enemy = electricEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ElectricEnemy electricEnemyLogic = electricEnemy.GetComponent<ElectricEnemy>();
        electricEnemyLogic.gamemanager = gamemanager;
        GameObject electricLine = gamemanager.objectmanager.MakeObj("ElectricLine");
        electricEnemyLogic.electricLine = electricLine;
        electricLine.transform.localScale = new Vector2(0, 0);
        //위치 이동
        electricEnemy.transform.position = transform.position;
        electricLine.transform.position = transform.position;
    }

    void MachineEnemys() {//기계 슬라임
        GameObject mEnemy = gamemanager.objectmanager.MakeObj("MachineEnemy");
        Enemy enemy = mEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        MachineEnemy mEnemylogic = mEnemy.GetComponent<MachineEnemy>();
        mEnemylogic.gamemanager = gamemanager;
        //위치 이동
        mEnemy.transform.position = transform.position;
    }

    void UfoEnemys() {
        GameObject uEnemy = gamemanager.objectmanager.MakeObj("UfoEnemy");
        Enemy enemy = uEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        UfoEnemy logic = uEnemy.GetComponent<UfoEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        uEnemy.transform.position = transform.position;
    }

    void VenomEnemys()
    {//추적자 소환
        GameObject vEnemy = gamemanager.objectmanager.MakeObj("VenomEnemy");
        Enemy enemy = vEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        VenomEnemy vEnemyLogic = vEnemy.GetComponent<VenomEnemy>();
        vEnemyLogic.player = gamemanager.player;
        vEnemyLogic.objectmanager = gamemanager.objectmanager;
        //위치 이동
        vEnemy.transform.position = transform.position;
    }

    void ThiefEnemys()
    {//인디언 소환
        GameObject tEnemy = gamemanager.objectmanager.MakeObj("ThiefEnemy");
        Enemy enemy = tEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ThiefEnemy tEnemylogic = tEnemy.GetComponent<ThiefEnemy>();
        tEnemylogic.player = gamemanager.player;
        tEnemylogic.gamemanager = gamemanager;
        tEnemylogic.objectmanager = gamemanager.objectmanager;
        //위치 이동
        tEnemy.transform.position = transform.position;
    }

    void BoneEnemys()
    {
        GameObject bEnemy = gamemanager.objectmanager.MakeObj("BoneEnemy");
        Enemy enemy = bEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        BoneEnemy logic = bEnemy.GetComponent<BoneEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        bEnemy.transform.Rotate(Vector3.back * 5);
        bEnemy.transform.position = transform.position;
    }

    void CueEnemys()
    {//당구공 소환
        GameObject cEnemy = gamemanager.objectmanager.MakeObj("CueEnemy");
        Enemy enemy = cEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        CueEnemy cEnemyLogic = cEnemy.GetComponent<CueEnemy>();
        cEnemyLogic.player = gamemanager.player;
        cEnemyLogic.gamemanager = gamemanager;
        cEnemy.transform.position = transform.position;
    }

    void LeeEnemys()
    {
        GameObject lEnemy = gamemanager.objectmanager.MakeObj("LeeEnemy");
        Enemy enemy = lEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        LeeEnemy lEnemyLogic = lEnemy.GetComponent<LeeEnemy>();
        lEnemyLogic.objectmanager = gamemanager.objectmanager;
        lEnemyLogic.player = gamemanager.player;
        lEnemyLogic.gamemanager = gamemanager;
        //위치 이동
        lEnemy.transform.position = transform.position;
    }

    void FanEnemys()
    {
        GameObject fEnemy = gamemanager.objectmanager.MakeObj("FanEnemy");
        Enemy enemy = fEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        FanEnemy logic = fEnemy.GetComponent<FanEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        fEnemy.transform.position = transform.position;
    }

    void ZombieFEEnemys()
    {
        GameObject zEnemy = gamemanager.objectmanager.MakeObj("ZombieFEEnemy");
        Enemy enemy = zEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        ZombieFEEnemy Logic = zEnemy.GetComponent<ZombieFEEnemy>();
        Logic.player = gamemanager.player;
        Logic.gamemanager = gamemanager;
        //위치 이동
        zEnemy.transform.position = transform.position;
    }

    void SportEnemys()
    {//농구공 소환
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("SportEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        SportEnemy logic = mainEnemy.GetComponent<SportEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        logic.playerLogic = gamemanager.player.GetComponent<Player>();
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void EngelEnemys()
    {//천사 소환
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("EngelEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        EngelEnemy logic = mainEnemy.GetComponent<EngelEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void TripleEnemyHs()
    {//망치 소환
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("TripleEnemyH");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        TripleEnemy logic = mainEnemy.GetComponent<TripleEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        logic.partner[0] = gamemanager.player;
        logic.partner[1] = gamemanager.player;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void TripleEnemySs()
    {//검 소환
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("TripleEnemyS");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        TripleEnemy logic = mainEnemy.GetComponent<TripleEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        logic.partner[0] = gamemanager.player;
        logic.partner[1] = gamemanager.player;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void TripleEnemyLs()
    {//창 소환
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("TripleEnemyL");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        TripleEnemy logic = mainEnemy.GetComponent<TripleEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        logic.partner[0] = gamemanager.player;
        logic.partner[1] = gamemanager.player;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void TripleEnemys()
    {//삼위일체 소환
        GameObject[] mainEnemy = new GameObject[3];
        string Word = "H";
        for (int i = 0; i <= 2; i++)
        {
            if (i == 1) Word = "S";
            else if (i == 2) Word = "L";
            mainEnemy[i] = gamemanager.objectmanager.MakeObj("TripleEnemy" + Word);
        }
            for (int i = 0; i <= 2; i++)
        {
            Enemy enemy = mainEnemy[i].GetComponent<Enemy>();
            enemy.gamemanager = gamemanager;
            if (i == 0) enemy.BossHp = BossHp;
            else if (i == 1) enemy.BossHp = BossHp2;
            else if (i == 2) enemy.BossHp = BossHp3;
            TripleEnemy logic = mainEnemy[i].GetComponent<TripleEnemy>();
            logic.player = gamemanager.player;
            logic.objectmanager = gamemanager.objectmanager;
            logic.gamemanager = gamemanager;
            if (i == 0) {
                logic.partner[0] = mainEnemy[1];
                logic.partner[1] = mainEnemy[2];
            }
            else if (i == 1)
            {
                logic.partner[0] = mainEnemy[0];
                logic.partner[1] = mainEnemy[2];
            }
            else if (i == 2)
            {
                logic.partner[0] = mainEnemy[0];
                logic.partner[1] = mainEnemy[1];
            }
            //위치 이동
            mainEnemy[i].transform.position = transform.position;
        }
    }

    void LaserEnemys()
    {//광년
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("LaserEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        LaserEnemy logic = mainEnemy.GetComponent<LaserEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = new Vector3(transform.position.x, gamemanager.player.transform.position.y, transform.position.z);
    }

    void HelicopterEnemys()
    {//헬리콥터
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("HelicopterEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        HelicopterEnemy logic = mainEnemy.GetComponent<HelicopterEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = new Vector3(transform.position.x, gamemanager.player.transform.position.y, transform.position.z);

        if (logic.player.transform.position.x < transform.position.x) mainEnemy.GetComponent<SpriteRenderer>().flipX = true;
        else mainEnemy.GetComponent<SpriteRenderer>().flipX = false;

    }

    void BeeEnemys()
    {//벌구
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("BeeEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        BeeEnemy logic = mainEnemy.GetComponent<BeeEnemy>();
        logic.player = gamemanager.player.GetComponent<Player>();
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void QueenEnemys()
    {//여왕
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("QueenEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        QueenEnemy logic = mainEnemy.GetComponent<QueenEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }

    void MoonEnemys()
    {//달
        GameObject mainEnemy = gamemanager.objectmanager.MakeObj("MoonEnemy");
        Enemy enemy = mainEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.BossHp = BossHp;
        MoonEnemy logic = mainEnemy.GetComponent<MoonEnemy>();
        logic.player = gamemanager.player;
        logic.objectmanager = gamemanager.objectmanager;
        logic.gamemanager = gamemanager;
        //위치 이동
        mainEnemy.transform.position = transform.position;
    }
}