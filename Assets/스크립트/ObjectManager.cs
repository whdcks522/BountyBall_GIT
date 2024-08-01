using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //불렛
    public GameObject playerbulletprefab;
    public GameObject playerbulletBprefab;
    public GameObject enemybulletAprefab;
    public GameObject enemybulletBprefab;
    public GameObject enemybulletCprefab;
    public GameObject enemybulletDprefab;
    public GameObject enemyRocketAprefab;
    public GameObject enemyCanonAprefab;
    public GameObject enemyNiddleAprefab;
    public GameObject enemyGasprefab;
    public GameObject enemyBombAprefab;
    public GameObject enemyBmrAprefab;
    public GameObject enemyCircleprefab;
    public GameObject enemyBallprefab;
    public GameObject enemyInjectionprefab;
    public GameObject enemyWindprefab;
    public GameObject enemyMeteoprefab;
    //적
    public GameObject dummyEnemyprefab;
    public GameObject followEnemyprefab;
    public GameObject slimeEnemyprefab;
    public GameObject excelEnemyprefab;
    public GameObject tomasEnemyprefab;
    public GameObject caterEnemyprefab;
    public GameObject trashEnemyprefab;
    public GameObject birdEnemyprefab;
    public GameObject shieldEnemyprefab;
    public GameObject infinityEnemyprefab;
    public GameObject oldEnemyprefab;
    public GameObject woodEnemyprefab;
    public GameObject debilEnemyprefab;
    public GameObject zealEnemyprefab;
    public GameObject wildEnemyprefab;
    public GameObject indianEnemyprefab;
    public GameObject remoteEnemyprefab;
    public GameObject electricEnemyprefab;
    public GameObject machineEnemyprefab;
    public GameObject ufoEnemyprefab;
    public GameObject venomEnemyprefab;
    public GameObject thiefEnemyprefab;
    public GameObject boneEnemyprefab;
    public GameObject cueEnemyprefab;
    public GameObject leeEnemyprefab;
    public GameObject fanEnemyprefab;
    public GameObject zombieFEEnemyprefab;
    public GameObject sportEnemyprefab;
    public GameObject engelEnemyprefab;
    public GameObject tripleEnemyHprefab;
    public GameObject tripleEnemySprefab;
    public GameObject tripleEnemyLprefab;
    public GameObject laserEnemyprefab;
    public GameObject helicopterEnemyprefab;
    public GameObject beeEnemyprefab;
    public GameObject queenEnemyprefab;
    public GameObject moonEnemyprefab;
    //이펙트
    public GameObject destroyprefab;
    public GameObject bombEffectprefab;
    public GameObject generateEffectprefab;
    public GameObject warningCircleprefab;
    public GameObject warningRectangleprefab;
    public GameObject electricLineprefab;
    public GameObject venomprefab;
    public GameObject becomeSwordprefab;
    public GameObject eyesprefab;
    public GameObject antiEyesprefab;
    //벽
    public GameObject fakeBoxprefab;
    public GameObject moveBoxprefab;
    public GameObject bulbBoxprefab;
    //불렛
    GameObject[] playerbullet;
    GameObject[] playerbulletB;
    GameObject[] enemybulletA;
    GameObject[] enemybulletB;
    GameObject[] enemybulletC;
    GameObject[] enemybulletD;
    GameObject[] enemyRocketA;
    GameObject[] enemyCanonA;
    GameObject[] enemyNiddleA;
    GameObject[] enemyGas;
    GameObject[] enemyBombA;
    GameObject[] enemyBmrA;
    GameObject[] enemyCircle;
    GameObject[] enemyBall;
    GameObject[] enemyInjection;
    GameObject[] enemyWind;
    GameObject[] enemyMeteo;
    //적
    GameObject[] dummyEnemy;
    GameObject[] followEnemy;
    GameObject[] slimeEnemy;
    GameObject[] excelEnemy;
    GameObject[] tomasEnemy;
    GameObject[] caterEnemy;
    GameObject[] trashEnemy;
    GameObject[] birdEnemy;
    GameObject[] shieldEnemy;
    GameObject[] infinityEnemy;
    GameObject[] oldEnemy;
    GameObject[] woodEnemy;
    GameObject[] debilEnemy;
    GameObject[] zealEnemy;
    GameObject[] wildEnemy;
    GameObject[] indianEnemy;
    GameObject[] remoteEnemy;
    GameObject[] electricEnemy;
    GameObject[] machineEnemy;
    GameObject[] ufoEnemy;
    GameObject[] venomEnemy;
    GameObject[] thiefEnemy;
    GameObject[] boneEnemy;
    GameObject[] cueEnemy;
    GameObject[] leeEnemy;
    GameObject[] fanEnemy;
    GameObject[] zombieFEEnemy;
    GameObject[] sportEnemy;
    GameObject[] engelEnemy;
    GameObject[] tripleEnemyH;
    GameObject[] tripleEnemyS;
    GameObject[] tripleEnemyL;
    GameObject[] laserEnemy;
    GameObject[] helicopterEnemy;
    GameObject[] beeEnemy;
    GameObject[] queenEnemy;
    GameObject[] moonEnemy;
    //이펙트
    GameObject[] destroy;
    GameObject[] bombEffect;
    GameObject[] generateEffect;
    GameObject[] warningCircle;
    GameObject[] warningRectangle;
    GameObject[] electricLine;
    GameObject[] venom;
    GameObject[] becomeSword;
    GameObject[] eyes;
    GameObject[] antiEyes;
    //벽
    GameObject[] fakeBox;
    GameObject[] moveBox;
    GameObject[] bulbBox;

    GameObject[] pool;
    public GameObject[] pool2;

    void Start()
    {
        //불렛
        playerbullet = new GameObject[110];
        playerbulletB = new GameObject[8];
        enemybulletA = new GameObject[50];
        enemybulletB = new GameObject[100];
        enemybulletC = new GameObject[80];
        enemybulletD = new GameObject[30];
        enemyRocketA = new GameObject[20];
        enemyCanonA = new GameObject[40];
        enemyNiddleA = new GameObject[40];
        enemyGas = new GameObject[10];
        enemyBombA = new GameObject[40];
        enemyBmrA = new GameObject[20];
        enemyCircle = new GameObject[30];
        enemyBall = new GameObject[8];
        enemyInjection = new GameObject[25];
        enemyWind = new GameObject[30];
        enemyMeteo = new GameObject[30];
        //적
        dummyEnemy = new GameObject[10];
        followEnemy = new GameObject[15];
        slimeEnemy = new GameObject[12];
        excelEnemy = new GameObject[12];
        tomasEnemy = new GameObject[3];
        caterEnemy = new GameObject[8];
        trashEnemy = new GameObject[1];
        birdEnemy = new GameObject[8];
        shieldEnemy = new GameObject[8];
        infinityEnemy = new GameObject[2];
        oldEnemy = new GameObject[2];
        woodEnemy = new GameObject[8];
        debilEnemy = new GameObject[8];
        zealEnemy = new GameObject[2];
        wildEnemy = new GameObject[8];
        indianEnemy = new GameObject[5];
        remoteEnemy = new GameObject[2];
        electricEnemy = new GameObject[8];
        machineEnemy = new GameObject[8];
        ufoEnemy = new GameObject[1];
        venomEnemy = new GameObject[8];
        thiefEnemy = new GameObject[8];
        boneEnemy = new GameObject[1];
        cueEnemy = new GameObject[4];
        leeEnemy = new GameObject[4];
        fanEnemy = new GameObject[1];
        zombieFEEnemy = new GameObject[4];
        sportEnemy = new GameObject[1];
        engelEnemy = new GameObject[5];
        tripleEnemyH = new GameObject[2];
        tripleEnemyS = new GameObject[1];
        tripleEnemyL = new GameObject[1];
        laserEnemy = new GameObject[3];
        helicopterEnemy = new GameObject[1];
        beeEnemy = new GameObject[6];
        queenEnemy = new GameObject[1];
        moonEnemy = new GameObject[1];
        //이펙트
        destroy = new GameObject[110];
        bombEffect = new GameObject[40];
        generateEffect = new GameObject[20];
        warningCircle = new GameObject[8];
        warningRectangle = new GameObject[2];
        electricLine = new GameObject[10];
        venom = new GameObject[200];
        becomeSword = new GameObject[4];
        eyes = new GameObject[6];
        antiEyes = new GameObject[2];
        //벽
        fakeBox = new GameObject[20];
        moveBox = new GameObject[4];
        bulbBox = new GameObject[4];

        pool2 = new GameObject[150];
        Generate();
    }

    void Generate(){
        //불렛
        for (int index = 0; index < playerbullet.Length; index++){
            playerbullet[index] = Instantiate(playerbulletprefab);
            playerbullet[index].SetActive(false);
        }
        for (int index = 0; index < playerbulletB.Length; index++)
        {
            playerbulletB[index] = Instantiate(playerbulletBprefab);
            playerbulletB[index].SetActive(false);
        }
        for (int index = 0; index < enemybulletA.Length; index++)
        {
            enemybulletA[index] = Instantiate(enemybulletAprefab);
            enemybulletA[index].SetActive(false);
        }
        for (int index = 0; index < enemybulletB.Length; index++)
        {
            enemybulletB[index] = Instantiate(enemybulletBprefab);
            enemybulletB[index].SetActive(false);
        }
        for (int index = 0; index < enemyRocketA.Length; index++)
        {
            enemyRocketA[index] = Instantiate(enemyRocketAprefab);
            enemyRocketA[index].SetActive(false);
        }
        for (int index = 0; index < enemyCanonA.Length; index++)
        {
            enemyCanonA[index] = Instantiate(enemyCanonAprefab);
            enemyCanonA[index].SetActive(false);
        }
        for (int index = 0; index < enemyNiddleA.Length; index++)
        {
            enemyNiddleA[index] = Instantiate(enemyNiddleAprefab);
            enemyNiddleA[index].SetActive(false);
        }
        for (int index = 0; index < enemyGas.Length; index++)
        {
            enemyGas[index] = Instantiate(enemyGasprefab);
            enemyGas[index].SetActive(false);
        }
        for (int index = 0; index < enemyBombA.Length; index++)
        {
            enemyBombA[index] = Instantiate(enemyBombAprefab);
            enemyBombA[index].SetActive(false);
        }
        for (int index = 0; index < enemyBmrA.Length; index++)
        {
            enemyBmrA[index] = Instantiate(enemyBmrAprefab);
            enemyBmrA[index].SetActive(false);
        }
        for (int index = 0; index < enemybulletC.Length; index++)
        {
            enemybulletC[index] = Instantiate(enemybulletCprefab);
            enemybulletC[index].SetActive(false);
        }
        for (int index = 0; index < enemyCircle.Length; index++)
        {
            enemyCircle[index] = Instantiate(enemyCircleprefab);
            enemyCircle[index].SetActive(false);
        }
        for (int index = 0; index < enemyBall.Length; index++)
        {
            enemyBall[index] = Instantiate(enemyBallprefab);
            enemyBall[index].SetActive(false);
        }
        for (int index = 0; index < enemyInjection.Length; index++)
        {
            enemyInjection[index] = Instantiate(enemyInjectionprefab);
            enemyInjection[index].SetActive(false);
        }
        for (int index = 0; index < enemybulletD.Length; index++)
        {
            enemybulletD[index] = Instantiate(enemybulletDprefab);
            enemybulletD[index].SetActive(false);
        }
        for (int index = 0; index < enemyWind.Length; index++)
        {
            enemyWind[index] = Instantiate(enemyWindprefab);
            enemyWind[index].SetActive(false);
        }
        for (int index = 0; index < enemyMeteo.Length; index++)
        {
            enemyMeteo[index] = Instantiate(enemyMeteoprefab);
            enemyMeteo[index].SetActive(false);
        }
        //적
        for (int index = 0; index < dummyEnemy.Length; index++)
        {
            dummyEnemy[index] = Instantiate(dummyEnemyprefab);
            dummyEnemy[index].SetActive(false);
        }
        for (int index = 0; index < followEnemy.Length; index++)
        {
            followEnemy[index] = Instantiate(followEnemyprefab);
            followEnemy[index].SetActive(false);
        }
        for (int index = 0; index < slimeEnemy.Length; index++)
        {
            slimeEnemy[index] = Instantiate(slimeEnemyprefab);
            slimeEnemy[index].SetActive(false);
        }
        for (int index = 0; index < excelEnemy.Length; index++)
        {
            excelEnemy[index] = Instantiate(excelEnemyprefab);
            excelEnemy[index].SetActive(false);
        }
        for (int index = 0; index < tomasEnemy.Length; index++)
        {
            tomasEnemy[index] = Instantiate(tomasEnemyprefab);
            tomasEnemy[index].SetActive(false);
        }
        for (int index = 0; index < caterEnemy.Length; index++)
        {
            caterEnemy[index] = Instantiate(caterEnemyprefab);
            caterEnemy[index].SetActive(false);
        }
        for (int index = 0; index < trashEnemy.Length; index++)
        {
            trashEnemy[index] = Instantiate(trashEnemyprefab);
            trashEnemy[index].SetActive(false);
        }
        for (int index = 0; index < birdEnemy.Length; index++)
        {
            birdEnemy[index] = Instantiate(birdEnemyprefab);
            birdEnemy[index].SetActive(false);
        }
        for (int index = 0; index < shieldEnemy.Length; index++)
        {
            shieldEnemy[index] = Instantiate(shieldEnemyprefab);
            shieldEnemy[index].SetActive(false);
        }
        for (int index = 0; index < infinityEnemy.Length; index++)
        {
            infinityEnemy[index] = Instantiate(infinityEnemyprefab);
            infinityEnemy[index].SetActive(false);
        }
        for (int index = 0; index < oldEnemy.Length; index++)
        {
            oldEnemy[index] = Instantiate(oldEnemyprefab);
            oldEnemy[index].SetActive(false);
        }
        for (int index = 0; index < woodEnemy.Length; index++)
        {
            woodEnemy[index] = Instantiate(woodEnemyprefab);
            woodEnemy[index].SetActive(false);
        }
        for (int index = 0; index < debilEnemy.Length; index++)
        {
            debilEnemy[index] = Instantiate(debilEnemyprefab);
            debilEnemy[index].SetActive(false);
        }
        for (int index = 0; index < zealEnemy.Length; index++)
        {
            zealEnemy[index] = Instantiate(zealEnemyprefab);
            zealEnemy[index].SetActive(false);
        }
        for (int index = 0; index < wildEnemy.Length; index++)
        {
            wildEnemy[index] = Instantiate(wildEnemyprefab);
            wildEnemy[index].SetActive(false);
        }
        for (int index = 0; index < indianEnemy.Length; index++)
        {
            indianEnemy[index] = Instantiate(indianEnemyprefab);
            indianEnemy[index].SetActive(false);
        }
        for (int index = 0; index < remoteEnemy.Length; index++)
        {
            remoteEnemy[index] = Instantiate(remoteEnemyprefab);
            remoteEnemy[index].SetActive(false);
        }
        for (int index = 0; index < electricEnemy.Length; index++)
        {
            electricEnemy[index] = Instantiate(electricEnemyprefab);
            electricEnemy[index].SetActive(false);
        }
        for (int index = 0; index < machineEnemy.Length; index++)
        {
            machineEnemy[index] = Instantiate(machineEnemyprefab);
            machineEnemy[index].SetActive(false);
        }
        for (int index = 0; index < ufoEnemy.Length; index++)
        {
            ufoEnemy[index] = Instantiate(ufoEnemyprefab);
            ufoEnemy[index].SetActive(false);
        }
        for (int index = 0; index < venomEnemy.Length; index++)
        {
            venomEnemy[index] = Instantiate(venomEnemyprefab);
            venomEnemy[index].SetActive(false);
        }
        for (int index = 0; index < thiefEnemy.Length; index++)
        {
            thiefEnemy[index] = Instantiate(thiefEnemyprefab);
            thiefEnemy[index].SetActive(false);
        }
        for (int index = 0; index < boneEnemy.Length; index++)
        {
            boneEnemy[index] = Instantiate(boneEnemyprefab);
            boneEnemy[index].SetActive(false);
        }
        for (int index = 0; index < cueEnemy.Length; index++)
        {
            cueEnemy[index] = Instantiate(cueEnemyprefab);
            cueEnemy[index].SetActive(false);
        }
        for (int index = 0; index < leeEnemy.Length; index++)
        {
            leeEnemy[index] = Instantiate(leeEnemyprefab);
            leeEnemy[index].SetActive(false);
        }
        for (int index = 0; index < fanEnemy.Length; index++)
        {
            fanEnemy[index] = Instantiate(fanEnemyprefab);
            fanEnemy[index].SetActive(false);
        }
        for (int index = 0; index < zombieFEEnemy.Length; index++)
        {
            zombieFEEnemy[index] = Instantiate(zombieFEEnemyprefab);
            zombieFEEnemy[index].SetActive(false);
        }
        for (int index = 0; index < sportEnemy.Length; index++)
        {
            sportEnemy[index] = Instantiate(sportEnemyprefab);
            sportEnemy[index].SetActive(false);
        }
        for (int index = 0; index < engelEnemy.Length; index++)
        {
            engelEnemy[index] = Instantiate(engelEnemyprefab);
            engelEnemy[index].SetActive(false);
        }
        for (int index = 0; index < tripleEnemyH.Length; index++)
        {
            tripleEnemyH[index] = Instantiate(tripleEnemyHprefab);
            tripleEnemyH[index].SetActive(false);
        }
        for (int index = 0; index < tripleEnemyS.Length; index++)
        {
            tripleEnemyS[index] = Instantiate(tripleEnemySprefab);
            tripleEnemyS[index].SetActive(false);
        }
        for (int index = 0; index < tripleEnemyL.Length; index++)
        {
            tripleEnemyL[index] = Instantiate(tripleEnemyLprefab);
            tripleEnemyL[index].SetActive(false);
        }
        for (int index = 0; index < laserEnemy.Length; index++)
        {
            laserEnemy[index] = Instantiate(laserEnemyprefab);
            laserEnemy[index].SetActive(false);
        }
        for (int index = 0; index < helicopterEnemy.Length; index++)
        {
            helicopterEnemy[index] = Instantiate(helicopterEnemyprefab);
            helicopterEnemy[index].SetActive(false);
        }
        for (int index = 0; index < beeEnemy.Length; index++)
        {
            beeEnemy[index] = Instantiate(beeEnemyprefab);
            beeEnemy[index].SetActive(false);
        }
        for (int index = 0; index < queenEnemy.Length; index++)
        {
            queenEnemy[index] = Instantiate(queenEnemyprefab);
            queenEnemy[index].SetActive(false);
        }
        for (int index = 0; index < moonEnemy.Length; index++)
        {
            moonEnemy[index] = Instantiate(moonEnemyprefab);
            moonEnemy[index].SetActive(false);
        }
        //이펙트
        for (int index = 0; index < destroy.Length; index++)
        {
            destroy[index] = Instantiate(destroyprefab);
            destroy[index].SetActive(false);
        }
        for (int index = 0; index < bombEffect.Length; index++)
        {
            bombEffect[index] = Instantiate(bombEffectprefab);
            bombEffect[index].SetActive(false);
        }
        for (int index = 0; index < generateEffect.Length; index++)
        {
            generateEffect[index] = Instantiate(generateEffectprefab);
            generateEffect[index].SetActive(false);
        }
        for (int index = 0; index < warningCircle.Length; index++)
        {
            warningCircle[index] = Instantiate(warningCircleprefab);
            warningCircle[index].SetActive(false);
        }
        for (int index = 0; index < warningRectangle.Length; index++)
        {
            warningRectangle[index] = Instantiate(warningRectangleprefab);
            warningRectangle[index].SetActive(false);
        }
        for (int index = 0; index < electricLine.Length; index++)
        {
            electricLine[index] = Instantiate(electricLineprefab);
            electricLine[index].SetActive(false);
        }
        for (int index = 0; index < venom.Length; index++)
        {
            venom[index] = Instantiate(venomprefab);
            venom[index].SetActive(false);
        }
        for (int index = 0; index < becomeSword.Length; index++)
        {
            becomeSword[index] = Instantiate(becomeSwordprefab);
            becomeSword[index].SetActive(false);
        }
        for (int index = 0; index < eyes.Length; index++)
        {
            eyes[index] = Instantiate(eyesprefab);
            eyes[index].SetActive(false);
        }
        for (int index = 0; index < antiEyes.Length; index++)
        {
            antiEyes[index] = Instantiate(antiEyesprefab);
            antiEyes[index].SetActive(false);
        }
        //박스
        for (int index = 0; index < fakeBox.Length; index++)
        {
            fakeBox[index] = Instantiate(fakeBoxprefab);
            fakeBox[index].SetActive(false);
        }
        for (int index = 0; index < moveBox.Length; index++)
        {
            moveBox[index] = Instantiate(moveBoxprefab);
            moveBox[index].SetActive(false);
        }
        for (int index = 0; index < bulbBox.Length; index++)
        {
            bulbBox[index] = Instantiate(bulbBoxprefab);
            bulbBox[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type){
        switch (type){
            case "PlayerBullet":
                pool = playerbullet;
                break;
            case "PlayerBulletB":
                pool = playerbulletB;
                break;
            case "EnemyBulletA":
                pool = enemybulletA;
                break;
            case "EnemyBulletB":
                pool = enemybulletB;
                break;
            case "EnemyRocketA":
                pool = enemyRocketA;
                break;
            case "EnemyCanonA":
                pool = enemyCanonA;
                break;
            case "EnemyNiddleA":
                pool = enemyNiddleA;
                break;
            case "EnemyGas":
                pool = enemyGas;
                break;
            case "EnemyBombA":
                pool = enemyBombA;
                break;
            case "EnemyBmrA":
                pool = enemyBmrA;
                break;
            case "EnemyBulletC":
                pool = enemybulletC;
                break;
            case "EnemyCircle":
                pool = enemyCircle;
                break;
            case "EnemyBall":
                pool = enemyBall;
                break;
            case "EnemyInjection":
                pool = enemyInjection;
                break;
            case "EnemyBulletD":
                pool = enemybulletD;
                break;
            case "EnemyWind":
                pool = enemyWind;
                break;
            case "EnemyMeteo":
                pool = enemyMeteo;
                break;
            //적
            case "DummyEnemy":
                pool = dummyEnemy;
                break;
            case "FollowEnemy":
                pool = followEnemy;
                break;
            case "SlimeEnemy":
                pool = slimeEnemy;
                break;
            case "ExcelEnemy":
                pool = excelEnemy;
                break;
            case "TomasEnemy":
                pool = tomasEnemy;
                break;
            case "CaterEnemy":
                pool = caterEnemy;
                break;
            case "TrashEnemy":
                pool = trashEnemy;
                break;
           case "BirdEnemy":
                pool = birdEnemy;
                break;
            case "ShieldEnemy":
                pool = shieldEnemy;
                break;
            case "InfinityEnemy":
                pool = infinityEnemy;
                break;
            case "OldEnemy":
                pool = oldEnemy;
                break;
            case "WoodEnemy":
                pool = woodEnemy;
                break;
            case "DebilEnemy":
                pool = debilEnemy;
                break;
            case "ZealEnemy":
                pool = zealEnemy;
                break;
            case "WildEnemy":
                pool = wildEnemy;
                break;
            case "IndianEnemy":
                pool = indianEnemy;
                break;
            case "RemoteEnemy":
                pool = remoteEnemy;
                break;
            case "ElectricEnemy":
                pool = electricEnemy;
                break;
            case "MachineEnemy":
                pool = machineEnemy;
                break;
            case "UfoEnemy":
                pool = ufoEnemy;
                break;
            case "VenomEnemy":
                pool = venomEnemy;
                break;
            case "ThiefEnemy":
                pool = thiefEnemy;
                break;
            case "BoneEnemy":
                pool = boneEnemy;
                break;
            case "CueEnemy":
                pool = cueEnemy;
                break;
            case "FanEnemy":
                pool = fanEnemy;
                break;
            case "LeeEnemy":
                pool = leeEnemy;
                break;
            case "ZombieFEEnemy":
                pool = zombieFEEnemy;
                break;
            case "SportEnemy":
                pool = sportEnemy;
                break;
            case "EngelEnemy":
                pool = engelEnemy;
                break;
            case "TripleEnemyH":
                pool = tripleEnemyH;
                break;
            case "TripleEnemyS":
                pool = tripleEnemyS;
                break;
            case "TripleEnemyL":
                pool = tripleEnemyL;
                break;
            case "LaserEnemy":
                pool = laserEnemy;
                break;
            case "HelicopterEnemy":
                pool = helicopterEnemy;
                break;
            case "BeeEnemy":
                pool = beeEnemy;
                break;
            case "QueenEnemy":
                pool = queenEnemy;
                break;
            case "MoonEnemy":
                pool = moonEnemy;
                break;
            //이펙트
            case "Destroy":
                pool = destroy;
                break;
            case "BombEffect":
                pool = bombEffect;
                break;
            case "GenerateEffect":
                pool = generateEffect;
                break;
            case "WarningCircle":
                pool = warningCircle;
                break;
            case "WarningRectangle":
                pool = warningRectangle;
                break;
            case "ElectricLine":
                pool = electricLine;
                break;
            case "Venom":
                pool = venom;
                break;
            case "BecomeSword":
                pool = becomeSword;
                break;
            case "Eyes":
                pool = eyes;
                break;
            case "AntiEyes":
                pool = antiEyes;
                break;
            //박스
            case "FakeBox":
                pool = fakeBox;
                break;
            case "MoveBox":
                pool = moveBox;
                break;
            case "BulbBox":
                pool = bulbBox;
                break;
        }
        for (int index = 0; index < pool.Length; index++)
        {
            if (!pool[index].activeSelf)
            {
                pool[index].SetActive(true);
                return pool[index];
            }
        }
        return null;
    }

    public GameObject[] ReturnObjs(string chooseType)
    {
        for (int index = 0; index < pool2.Length; index++)
        {
            pool2[index] = null;
        }
        switch (chooseType) {
            case "Bullet":
                ChooseObj(playerbullet);
                ChooseObj(playerbulletB);
                ChooseObj(enemybulletA);
                ChooseObj(enemybulletB);
                ChooseObj(enemybulletC);
                ChooseObj(enemybulletD);
                ChooseObj(enemyRocketA);
                ChooseObj(enemyCanonA);
                ChooseObj(enemyNiddleA);
                ChooseObj(enemyGas);
                ChooseObj(enemyBombA);
                ChooseObj(enemyBmrA);
                ChooseObj(enemyCircle);
                ChooseObj(enemyBall);
                ChooseObj(enemyInjection);
                ChooseObj(enemyWind);
                ChooseObj(enemyMeteo);
                break;
            case "Enemy":
                ChooseObj(enemyGas);
                ChooseObj(dummyEnemy);
                ChooseObj(followEnemy);
                ChooseObj(slimeEnemy);
                ChooseObj(tomasEnemy);
                ChooseObj(excelEnemy);
                ChooseObj(caterEnemy);
                ChooseObj(trashEnemy);
                ChooseObj(birdEnemy);
                ChooseObj(shieldEnemy);
                ChooseObj(infinityEnemy);
                ChooseObj(oldEnemy);
                ChooseObj(woodEnemy);
                ChooseObj(debilEnemy);
                ChooseObj(zealEnemy);
                ChooseObj(wildEnemy);
                ChooseObj(indianEnemy);
                ChooseObj(remoteEnemy);
                ChooseObj(electricEnemy);
                ChooseObj(machineEnemy);
                ChooseObj(ufoEnemy);
                ChooseObj(venomEnemy);
                ChooseObj(thiefEnemy);
                ChooseObj(boneEnemy);
                ChooseObj(cueEnemy);
                ChooseObj(leeEnemy);
                ChooseObj(fanEnemy);
                ChooseObj(zombieFEEnemy);
                ChooseObj(sportEnemy);
                ChooseObj(engelEnemy);
                ChooseObj(tripleEnemyH);
                ChooseObj(tripleEnemyS);
                ChooseObj(tripleEnemyL);
                ChooseObj(laserEnemy);
                ChooseObj(helicopterEnemy);
                ChooseObj(beeEnemy);
                ChooseObj(queenEnemy);
                ChooseObj(moonEnemy);
                break;
            case "Effect":
                ChooseObj(destroy);
                ChooseObj(bombEffect);
                ChooseObj(generateEffect);
                ChooseObj(warningCircle);
                ChooseObj(warningRectangle);
                ChooseObj(electricLine);
                ChooseObj(venom);
                ChooseObj(becomeSword);
                ChooseObj(eyes);
                ChooseObj(antiEyes);
                break;
            case "Box":
                ChooseObj(fakeBox);
                ChooseObj(moveBox);
                ChooseObj(bulbBox);
                break;
            case "Tomas":
                ChooseObj(tomasEnemy);
                break;
        }
        return pool2;
    }

    void ChooseObj(GameObject[] Cobj) {
        for (int index = 0; index < Cobj.Length; index++)
        {
            if (Cobj[index].activeSelf)
            {
                for (int index2 = 0; index2 < pool2.Length; index2++)
                {
                    if (pool2[index2] == null)
                    {
                        pool2[index2] = Cobj[index];
                        break;
                    }
                }   
            }
        }
    }
}
