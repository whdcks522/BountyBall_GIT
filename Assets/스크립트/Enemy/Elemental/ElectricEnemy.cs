using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEnemy : MonoBehaviour
{
    bool ready;
    bool ready2;
    public GameObject host;
    Rigidbody2D rigid;
    Vector2 hostVec;
    public GameObject electricLine;
    public GameManager gamemanager;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        if(electricLine != null)electricLine.gameObject.SetActive(false);
        CancelInvoke();
        if (GetComponent<Enemy>().health <= 0) {
            for (int x = -1; x <= 1; x+=2) {
                for (int y = -1; y <= 1; y += 2)
                {
                    GameObject bullet = gamemanager.objectmanager.MakeObj("EnemyBulletC");
                    Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bulletRigid.velocity = new Vector2(x, y).normalized * 3.0f;
                    float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
                    Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
                    bullet.transform.Rotate(rotVec);
                    Bullet bulletLogic = bullet.GetComponent<Bullet>();
                    bulletLogic.gamemanager = gamemanager;
                    bulletLogic.isExcel = true;
                }
            }
        }
    }
    private void OnEnable()
    {
        ready2 = false;
        ready = true;
        Invoke("setHost", 0.1f);
    }

    void setHost() {
        //탐색
        GameObject closeEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (closeEnemy == null) return;
        //오브젝트 탐색
        GameObject[] closeEnemys = GameObject.FindGameObjectsWithTag("Enemy");
        float index = 50;
        closeEnemy = null;
        for (int i = 0; i < closeEnemys.Length; i++)
        {
            //거리 구하기
            if (closeEnemys[i].gameObject.name.Contains("Electric Enemy(Clone)")) continue;
            Vector2 dirVec = transform.position - closeEnemys[i].transform.position;
            float dir = dirVec.magnitude;
            //적을 경우 빼기
            if (dir < index)
            {
                index = dir;
                closeEnemy = closeEnemys[i];
            }
        }
        host = closeEnemy;
        hostVec = host.transform.position - transform.position;
        ready2 = true;
    }

    void Update()
    {
        if (ready2)
        {
            hostVec = host.transform.position - transform.position;
            if ((ready) && (hostVec.magnitude > 0.1f)) rigid.velocity = hostVec.normalized * 2;
            else rigid.velocity = Vector2.zero;
            if (host.gameObject.activeSelf)
            {
                electricLine.transform.rotation = Quaternion.identity;
                electricLine.transform.localScale = new Vector2(hostVec.magnitude * 0.25f, 1);
                electricLine.transform.position = transform.position;
                float zValue = Mathf.Atan2(rigid.velocity.x, rigid.velocity.y) * 180 / Mathf.PI;
                Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
                electricLine.transform.Rotate(rotVec);
            }
            else if (host == null)
            {
                electricLine.transform.localScale = new Vector2(0, 0);
            }
            else electricLine.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 14) || (collision.gameObject.layer == 12))//벽들에 충돌시
        {
            rigid.velocity = Vector2.zero;
            ready = false;
        }
    }
}
