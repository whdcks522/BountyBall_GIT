using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonEnemy : MonoBehaviour
{
    public GameManager gamemanager;
    public ObjectManager objectmanager;
    public GameObject player;

    public GameObject fire;
    int count;
    float time;
    bool[] ignite;

    private void OnEnable()
    {
        fire.transform.position = (Vector2)transform.position + Vector2.down * 1.5f;
        time = 3;
        ignite[0] = false;
        ignite[1] = true;
        Invoke("Meteo", 0.1f);
    }

    void Summon() {
        GameObject tomasEnemy = gamemanager.objectmanager.MakeObj("TomasEnemy");
        Enemy enemy = tomasEnemy.GetComponent<Enemy>();
        enemy.gamemanager = gamemanager;
        enemy.health = 50;
        TomasEnemy tomasEnemyLogic = tomasEnemy.GetComponent<TomasEnemy>();
        tomasEnemyLogic.objectmanager = objectmanager;
        tomasEnemyLogic.player = player;
        tomasEnemyLogic.gamemanager = gamemanager;
        tomasEnemy.transform.position = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * time / 3), Mathf.Cos(Mathf.PI * time / 3)).normalized;
        fire.transform.position = transform.position + new Vector3(dirVec.x * 1.5f, dirVec.y * 1.5f, 0);
        if (time > 6)
        {
            gamemanager.BombStart(fire.transform.position);
            time = 0;
            GameObject[] tomas = objectmanager.ReturnObjs("Tomas");
            if (tomas[0] == null)
            {
                gamemanager.GenerateStart(transform.position, 5);
                Invoke("Summon", 0.8f);
                //return;
            }
            while (true)
            {
                int r = Random.Range(0, 2);
                if (r == 0 && !ignite[0])
                {
                    ignite[0] = true;
                    ignite[1] = false;
                    Charge();
                    break;
                }
                else if (r == 1 && !ignite[1])
                {
                    ignite[0] = false;
                    ignite[1] = true;
                    if (player.transform.position.y > 0) LeftUp();
                    else RightDown();
                    break;
                }
            }       
        }
        
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Awake()
    {
        ignite = new bool[2];
    }

    void LeftUp()
    {
        GameObject eye = objectmanager.MakeObj("AntiEyes");
        eye.transform.position = new Vector2(-9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = false;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(1, 0);
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        count = -8;
        //Invoke("LeftUp2", 1.1f);
    }

    void LeftUp2()
    {
        GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
        Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
        Bullet niddleLogic = niddle.GetComponent<Bullet>();
        niddleLogic.gamemanager = gamemanager;
        niddle.transform.position = new Vector2(-9, -1);
        Vector2 dirVec = new Vector2(count, 4) - (Vector2)niddle.transform.position;
        niddleRigid.velocity = dirVec.normalized * 10.0f;
        float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
        niddle.transform.Rotate(rotVec);
        count++;
        if (count <= 2) Invoke("LeftUp2", 0.1f);
    }

    void RightDown() {
        GameObject eye = objectmanager.MakeObj("AntiEyes");
        eye.transform.position = new Vector2(9, 0);
        eye.GetComponent<SpriteRenderer>().flipX = true;
        eye.GetComponent<BoxCollider2D>().offset = new Vector2(-1, 0);
        Animator eyean = eye.GetComponent<Animator>();
        eyean.SetTrigger("Action");
        count = 8;
        //Invoke("RightDown2", 1.1f);
    }

    void RightDown2() {
        GameObject niddle = objectmanager.MakeObj("EnemyNiddleA");
        Rigidbody2D niddleRigid = niddle.GetComponent<Rigidbody2D>();
        Bullet niddleLogic = niddle.GetComponent<Bullet>();
        niddleLogic.gamemanager = gamemanager;
        niddle.transform.position = new Vector2(9, 1);
        Vector2 dirVec = new Vector2(count, -4) - (Vector2)niddle.transform.position;
        niddleRigid.velocity = dirVec.normalized * 10.0f;
        float zValue = Mathf.Atan2(niddleRigid.velocity.x, niddleRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90.0f;
        niddle.transform.Rotate(rotVec);

        count--;
        if (count >= -2) Invoke("RightDown2", 0.1f);
    }

    void Charge() {
        for (int i = 0; i < 25; i++)
        {
            GameObject bullet = objectmanager.MakeObj("EnemyWind");
            Bullet bulletLogic = bullet.GetComponent<Bullet>();
            bulletLogic.gamemanager = gamemanager;
            bulletLogic.Breakable();

            Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * i * 2.0f / 25), Mathf.Cos(Mathf.PI * i * 2.0f / 25));
            bulletRigid.velocity = dirVec.normalized * 8;
            bullet.transform.Rotate(Vector3.back * 360 * i / 25 + Vector3.back * 270);

            bullet.transform.position = (Vector2)transform.position - dirVec * 12;
            gamemanager.DestroyStart("D", bullet.transform.position);
        }
        Invoke("Charge2", 1.7f);
    }

    void Charge2() {
        GameObject become = objectmanager.MakeObj("BecomeSword");
        become.transform.position = transform.position;
        become.GetComponent<Animator>().SetTrigger("Action");
    }

    void Meteo() {
        Vector2 rayVec = new Vector2(Random.Range(-6, 7), Random.Range(1, 4) * ((player.transform.position.y > 0) ? -1 : 1)).normalized;
        Debug.DrawRay(player.transform.position, rayVec * 30, new Color(1, 0, 0));
        RaycastHit2D ray = Physics2D.Raycast(player.transform.position, rayVec, 30, LayerMask.GetMask("EnemyBorder"));
        if (ray.collider != null) rayVec = ray.point + rayVec * 2;
        else Invoke("Meteo", 0.5f);

        GameObject bullet = objectmanager.MakeObj("EnemyMeteo");
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = rayVec;
        bulletRigid.velocity = (player.transform.position - transform.position).normalized * 5;
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.forward * 90;
        bullet.transform.Rotate(rotVec);
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Invoke("Meteo", 0.4f);
    }
}
