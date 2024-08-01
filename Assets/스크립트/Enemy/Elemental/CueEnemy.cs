using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueEnemy : MonoBehaviour
{
    float time;
    RaycastHit2D ray;
    public GameObject player;
    public GameManager gamemanager;
    Rigidbody2D rigid;
    int x, y;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Dash", 0.01f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Dash() { rigid.velocity = -(player.transform.position - transform.position).normalized * 10; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy") && ((collision.gameObject.GetComponent<Enemy>().isElemental == true)))
        {
            rigid.velocity = (transform.position - collision.transform.position) * 10;
        }
        else if (collision.gameObject.layer == 14) ControlMove("InvisibleBox");//환상벽에 충돌시
        else if (collision.gameObject.layer == 12) ControlMove("EnemyBorder");//적 벽에 충돌시
        else if (collision.gameObject.layer == 11)
        {
            gameObject.SetActive(false);
            gamemanager.StageClear();
        }
    }

    void ControlMove(string layer)
    {
        x = (rigid.velocity.x >= 0) ? 1 : -1;
        y = (rigid.velocity.y >= 0) ? 1 : -1;
        Debug.DrawRay(transform.position, Vector2.right * x, new Color(1, 0, 0));
        Debug.DrawRay(transform.position, Vector2.up * y, new Color(1, 0, 0));
        RaycastHit2D rayX = Physics2D.Raycast(transform.position, Vector2.right * x, 0.75f, LayerMask.GetMask(layer));
        RaycastHit2D rayY = Physics2D.Raycast(transform.position, Vector2.up * y, 0.75f, LayerMask.GetMask(layer));
        if ((rayX.collider != null)&&(rayY.collider == null))//X측
        {
            rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
        }
        else if ((rayX.collider == null)&&(rayY.collider != null))//Y축
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y);
        }
        else rigid.velocity = -rigid.velocity;
    }  
}
