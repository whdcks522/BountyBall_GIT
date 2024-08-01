using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int blockNum;
    public string type;
    public GameManager gamemanager;
    Player playerLogic;
    GameObject player;
    //타워박스와 전기줄 박스 전용
    public Vector2[] HitPosSave;
    float time;
    GameObject[] host;
    GameObject[] electricLine;
    //무브 박스
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        if (type == "T2") HitPosSave = new Vector2[20];
        else if (type == "E") {
            HitPosSave = new Vector2[6];
            host = new GameObject[6];
            electricLine = new GameObject[6];
        }
        else if (type == "M")
        {
            HitPosSave = new Vector2[1];
            host = new GameObject[15];
        }
        else if (type == "B2")
        {
            HitPosSave = new Vector2[4];
        }
    }

    private void Start()
    {
        if (type == "T2") player = gamemanager.player;
        playerLogic = gamemanager.player.GetComponent<Player>();
    }

    private void Update()
    {
        if (type == "T2") TowerAttack();
        else if (type == "E") ElectricLineAttack();
        else if (type == "M")
        {
            rigid.velocity = HitPosSave[0];
            
            for (int e = 0; e < host.Length; e++)
            {
                if (host[e] == null) continue;
                if (!host[e].activeSelf)
                {
                    host[e] = null;
                    continue;
                }
                host[e].GetComponent<Rigidbody2D>().velocity = HitPosSave[0];
            }
        }
        else if ((type == "B2"))
        {
            time += Time.deltaTime;
            if ((time > 1.0f)&&(gamemanager.isCreating2))
            {
                time = 0;
                MakeBulbBox();
            }
        }
    }

    private void OnEnable()
    {
        if (type == "T2") CheckTile("Tower", "Box");
        else if (type == "E")
        {
            CheckTile("ElectricLine", "Box");
            for (int z = 0; z < blockNum; z++)
            {
                GameObject electricLine2 = gamemanager.objectmanager.MakeObj("ElectricLine");
                electricLine[z] = electricLine2;
                electricLine[z].transform.localScale = new Vector2(0, 0);
            }
            Invoke("ElectricLineFind", 0.81f);
        }
        else if (type == "B2")
        {
            CheckTile("Bulb", "Box2");
            MakeBulbBox();
        }
    }

    private void OnDisable()
    {

        if (type == "T2")
        {
            blockNum = 0;
            time = 0.0f;
        }
        else if (type == "E")
        {
            blockNum = 0;
            host[0] = null;
            CancelInvoke();
        }
        else if (type == "M")
        {
            for (int e = 0; e < host.Length; e++) host[e] = null;
        }
        else if (type == "B2")
        {
            blockNum = 0;
            time = 0.0f;
        }
    }

    void MakeBulbBox()
    {
            for (int i = 0; i < blockNum; i++)
            {
                GameObject bulbbox = gamemanager.objectmanager.MakeObj("BulbBox");
                Box blogic = bulbbox.GetComponent<Box>();
                blogic.gamemanager = gamemanager;
                bulbbox.transform.position = HitPosSave[i];
            }
    }

    void ElectricLineAttack()
    {
        if (host[0] == null) return;
        for (int i = 0; i < blockNum; i++)
        {
            if (!host[i].activeSelf) electricLine[i].SetActive(false);
            else if (host[i].activeSelf)
            {
                electricLine[i].SetActive(true);
                Vector2 hostVec = new Vector2(host[i].transform.position.x, host[i].transform.position.y) - HitPosSave[i];
                electricLine[i].transform.rotation = Quaternion.identity;
                electricLine[i].transform.localScale = new Vector2(hostVec.magnitude * 0.25f, 1);
                electricLine[i].transform.position = HitPosSave[i];
                float zValue = Mathf.Atan2(hostVec.x, hostVec.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
                electricLine[i].transform.Rotate(rotVec);
            }
        }
    }

    void ElectricLineFind()
    {
        for (int i = 0; i < blockNum; i++)
        {
            //탐색
            GameObject closeEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (closeEnemy == null) return;
            //오브젝트 탐색
            GameObject[] closeEnemys = GameObject.FindGameObjectsWithTag("Enemy");
            float index = 50;
            closeEnemy = null;
            for (int j = 0; j < closeEnemys.Length; j++)
            {
                //거리 구하기
                if (closeEnemys[j].gameObject.name.Contains("Electric Enemy(Clone)")) continue;
                Vector2 dirVec = HitPosSave[i] - new Vector2(closeEnemys[j].transform.position.x, closeEnemys[j].transform.position.y);
                float dir = dirVec.magnitude;
                //적을 경우 빼기
                if (dir < index)
                {
                    index = dir;
                    closeEnemy = closeEnemys[j];
                }
            }
            host[i] = closeEnemy;
        }
    }

    void TowerAttack() {
        if (!player.gameObject.activeSelf) time = 0.0f;
        else if (gamemanager.isCreating)
        {
            time = 2.0f;
            return;
        }
        time += Time.deltaTime;
        if (time > 2.0f)
        {
            for (int i = 0; i < blockNum; i++)
            {
                GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyBulletB");
                Rigidbody2D bulletrigid = bullet.GetComponent<Rigidbody2D>();
                Bullet bulletlogic = bullet.GetComponent<Bullet>();
                bulletlogic.gamemanager = gamemanager;
                Vector2 GoalPos = new Vector2(player.transform.position.x - HitPosSave[i].x, player.transform.position.y - HitPosSave[i].y);
                bulletrigid.velocity = GoalPos.normalized * 8.0f;
                bullet.transform.position = HitPosSave[i];
                float zValue = Mathf.Atan2(bulletrigid.velocity.x, bulletrigid.velocity.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
                bullet.transform.Rotate(rotVec);
                gamemanager.DestroyStart("D", bullet.transform.position);
            }
            time = 0.0f;
        }
    }

   
    void CheckTile(string tagType, string layerType)
    {
        //좌표 찾기
        for (float x = -8.5f; x<= 8.5f; x++) {
            for (float y = -4.5f; y <= 4.5f; y++) {  
                Vector2 Hitpos = new Vector2(x, y);
                RaycastHit2D ray = Physics2D.Raycast(Hitpos, Vector2.zero, 0.1f, LayerMask.GetMask(layerType));
                if ((ray.collider == null) || (ray.collider.tag != tagType))//타워가 아니거나 공백일시
                    continue;
                else if (ray.collider.tag == tagType)//타워일시
                {
                    HitPosSave[blockNum] = Hitpos;
                    blockNum++;
                }
            }
        }
    }

    public void Sort()
    {
        if ((type != "C")&&(playerLogic.rememberType == "C"))
        {
            playerLogic.rememberType = null;
            playerLogic.isCold = false;
            Invoke("BoxJumpReset", 0.1f);
        }
        switch (type)
        {
            case "N":
            case "T2":
            case "E":
            case "M":
                NormalBox();
                break;
            case "F":
                FakeBox();
                break;
            case "H":
                HighBox();
                break;
            case "A":
                AbsoluteBox();
                break;
            case "S":
                StrongBox();
                break;
            case "T":
                TreeBox();
                break;
            case "C":
                playerLogic.rememberType = type;
                playerLogic.isCold = true;
                ColdBox();
                break;
            case "G":
                GenosideBox();
                break;
            case "C2":
                CureBox();
                break;
            case "B":
                BulbBox();
                break;
            case "E2":
                ExhaustBox();
                break;
            case "3D":
                D3Box();
                break;
            case "B3":
                B3Box();
                break;

        }
    }

    void NormalBox() {//노말 박스와 충돌
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("NormalJump");
            playerLogic.Jump("N");
        }
    }

    void FakeBox() {//거짓 박스와 충돌
        if(playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
        else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
        else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
        else gamemanager.SoundPlay("NormalJump");
        playerLogic.Jump("N");
        gameObject.SetActive(false);
        Invoke("ReturnFakeBox", 2.5f);
    }

    void ReturnFakeBox()//거짓 박스 초기화
    {
        if(((gamemanager.stagecontroller.isFight)||(gamemanager.GoggleStage.activeSelf))&&(blockNum == gamemanager.stagecontroller.stage))//고려
        gameObject.SetActive(true);
    }

    void HighBox() { //고공 박스와 충돌
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("HighJump");
            playerLogic.Jump("H");
        }
    }
    void AbsoluteBox()//절대 박스와 충돌
    {
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("Absolute");
            playerLogic.Jump("A");
        }
    }

    void StrongBox(){//근력 상자와 충돌
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("StrongJump");
            playerLogic.Jump("S");
        }
    }

    void TreeBox()//나무 상자와 상하충돌
    {
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("TreeJump");     
            playerLogic.Jump("N");
        }
    }

    void ColdBox()//차가운 상자와 상하충돌
    {
        if (!playerLogic.jump) {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("Cold");
            playerLogic.jump = true;
            playerLogic.Attack("N");
            Invoke("BoxJumpReset", 0.4f);
        }
    }
    void GenosideBox() {//학살 상자와 상하 충돌
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("Genoside");
            playerLogic.Jump("G");
        }
    }

    void CureBox()//회복 박스와 충돌
    {
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("Cure");
            playerLogic.Jump("N");
            playerLogic.isVenom = 0.0f;
        }
    }

    void BulbBox()//전구
    {
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("BecomeSword");
            playerLogic.Jump("N");
            gameObject.SetActive(false);
            GameObject become = gamemanager.objectmanager.MakeObj("BecomeSword");
            become.transform.position = transform.position;
            Animator becomeanim = become.GetComponent<Animator>();
            becomeanim.SetTrigger("Action");
        }
    }

    void ExhaustBox() {//탈진
        if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
        else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
        else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
        else gamemanager.SoundPlay("Exhaust");
        playerLogic.Jump("N");
        playerLogic.mana = 0;
    }

    void D3Box() {//입체기동장치
        if (!playerLogic.jump)
        {
            if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
            else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
            else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
            else gamemanager.SoundPlay("3D");
            playerLogic.Jump("A");
            if (playerLogic.D3sword == null)
            {
                GameObject Bsword = gamemanager.objectmanager.MakeObj("PlayerBulletB");
                Bullet BswordLogic = Bsword.GetComponent<Bullet>();
                BswordLogic.gamemanager = gamemanager;
                BswordLogic.host = null;
                BswordLogic.player = gamemanager.player;
                Bsword.transform.position = gamemanager.player.transform.position;
                
                Rigidbody2D D3Rigid = Bsword.GetComponent<Rigidbody2D>();
                D3Rigid.velocity = playerLogic.D3Vec;
                float zValue = Mathf.Atan2(D3Rigid.velocity.x, D3Rigid.velocity.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.back * 45.0f;
                Bsword.transform.Rotate(rotVec);
                playerLogic.D3sword = Bsword;
            }
        }
    }

    void B3Box(){
        if (playerLogic.isVenom2) gamemanager.SoundPlay("VenomIn");
        else if (playerLogic.isWater) gamemanager.SoundPlay("WaterIn");
        else if (playerLogic.rememberGravity == -2) gamemanager.SoundPlay("SpaceIn");
        else gamemanager.SoundPlay("Bee");
        playerLogic.Jump("A");
    }

    void BoxJumpReset() { playerLogic.JumpReset(); }
    void OnTriggerEnter2D(Collider2D collision)//가상충돌시 
    {
        //물이 플레이어와 충돌
        if ((type == "W") && (collision.gameObject.layer == 13))
        {
            playerLogic.rememberGravity = 0.5f;
            playerLogic.isWater = true;
            gamemanager.SoundPlay("WaterIn");
        }
        //절대벽에 플레이어 불릿 충돌
        else if ((type == "A") && (collision.gameObject.layer == 9)) gamemanager.SoundPlay("Absolute");
        //물과 플레이어 불릿 충돌
        else if ((type == "W") && (collision.gameObject.layer == 9)) gamemanager.SoundPlay("WaterFlip");
        //물이 플레이어와 충돌
        else if ((type == "V") && (collision.gameObject.layer == 13))
        {
            playerLogic.isVenom2 = true;
            gamemanager.SoundPlay("VenomIn");
        }
        else if (type == "M")
        {
            if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12)) HitPosSave[0] = -HitPosSave[0];
            else if ((collision.gameObject.layer == 10)&&(collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero ))
            {
                if ((collision.gameObject.GetComponent<Bullet>().isGas) || (collision.gameObject.GetComponent<Bullet>().isNiddle))
                    for (int e = 0; e < host.Length; e++)
                    {
                        if (host[e] != null) continue;
                        host[e] = collision.gameObject;
                        break;
                    }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((type == "W") && (collision.gameObject.layer == 13)) {
            playerLogic.rememberGravity = 2;
            playerLogic.isWater = false;
            gamemanager.SoundPlay("WaterOut");
        }
        else if ((type == "V") && (collision.gameObject.layer == 13))
        {
            playerLogic.isVenom2 = false;
            gamemanager.SoundPlay("VenomOut");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((type == "M") && (collision.gameObject.layer == 8)) HitPosSave[0] = -HitPosSave[0];
    }

}
