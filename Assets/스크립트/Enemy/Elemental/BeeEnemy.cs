using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    public Player player;

    Vector2 hitVec;
    Rigidbody2D rigid;
    Enemy enemy;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
    }

    private void OnDisable()
    {
        if (enemy.health == 0)
        {
            player.ThrowSword(Vector2.up, "R", transform.position);
            player.ThrowSword(Vector2.right, "R", transform.position);
            player.ThrowSword(Vector2.down, "R", transform.position);
            player.ThrowSword(Vector2.left, "R", transform.position);
        }
    }

    void moveCancel() {
        rigid.velocity = Vector2.zero;
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            CancelInvoke();
            hitVec = (Vector2)(collision.transform.position - transform.position).normalized * 5;
            rigid.velocity = hitVec;
            Invoke("moveCancel", 1.0f);
        }
        else if (collision.gameObject.layer == 12|| collision.gameObject.layer == 14)moveCancel();
    }
}
