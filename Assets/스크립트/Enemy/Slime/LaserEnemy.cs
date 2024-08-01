using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    public GameObject player;
    public ObjectManager objectmanager;
    public GameManager gamemanager;
    int LeftRight;
    SpriteRenderer spriterenderer;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.transform.position.x < transform.position.x) LeftRight = -1;
        else if (player.transform.position.x >= transform.position.x) LeftRight = 1;
        spriterenderer.flipX = (1 == LeftRight) ? false : true;
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void Action() {
        anim.SetTrigger("Action");
    }

    void Laser()
    {
        CancelInvoke();
        
        GameObject bullet = objectmanager.MakeObj("EnemyCircle");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        bulletLogic.isCircle = false;
        bulletLogic.time = 0.5f;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = transform.position;
        bulletRigid.velocity = Vector2.right * LeftRight * 5; 
        if(LeftRight == -1)bullet.transform.Rotate(Vector3.back *90);
        else bullet.transform.Rotate(Vector3.forward * 90);

        Invoke("Action", 1.75f);
        Invoke("Laser", 2.0f);
    }

    private void OnEnable()
    {
        Invoke("Action", 1.75f);
        Invoke("Laser", 2.0f);
    }
}
