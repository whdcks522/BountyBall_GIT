using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : MonoBehaviour
{

    public GameManager gamemanager;
    public ObjectManager objectmanager;
    public GameObject player;

    BoxCollider2D box;
    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;

    int LeftRight;
    int count;
    bool ignite;
    GameObject light;

    void Update(){
        if (!ignite)
        {
            if (player.transform.position.x < transform.position.x) LeftRight = -1;
            else LeftRight = 1;
            spriterenderer.flipX = (1 == LeftRight) ? false : true;
            box.offset = new Vector2(4 * -LeftRight, 3);
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    private void OnEnable()
    {
        if (light != null && light.activeSelf) light = null;
        ignite = false;
        count = 0;
        Invoke("Rocket", 0.5f);
    }

    void Rocket() {
        for (int i = -2; i <=2; i ++) {
            if (i == 0) continue;
            GameObject rocketR = objectmanager.MakeObj("EnemyRocketA");
            Bullet rocketRLogic = rocketR.GetComponent<Bullet>();
            rocketRLogic.gamemanager = gamemanager;
            rocketRLogic.player = player;
            rocketR.transform.position = (Vector2)transform.position + Vector2.right * -LeftRight * 2;
            Rigidbody2D rocketRRigid = rocketR.GetComponent<Rigidbody2D>();
            rocketRRigid.velocity = new Vector2(LeftRight * -3, i * 1.25f);
            gamemanager.DestroyStart("D", rocketR.transform.position);
        }
        count++;
        if (count > 2)
        {
            count = 0;
            Invoke("Ice", 6.5f);
        }
        else Invoke("Rocket", 0.25f);
    }

    void Ice() {
        GameObject bullet = objectmanager.MakeObj("EnemyBulletD");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        bulletLogic.isIce = true;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = (Vector2)transform.position + Vector2.right * LeftRight;
        bulletRigid.velocity = (Vector2)(player.transform.position - bullet.transform.position).normalized * 8;
        float zValue = Mathf.Atan2(bulletRigid.velocity.x, bulletRigid.velocity.y) * 180 / Mathf.PI;
        Vector3 rotVec = Vector3.back * zValue + Vector3.back * 270;
        bullet.transform.Rotate(rotVec);
        gamemanager.DestroyStart("D", bullet.transform.position);
        Invoke("Spin", 1.25f);
    }

    void Spin() {
        GameObject bullet = objectmanager.MakeObj("EnemyBulletB");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.position = (Vector2)transform.position + new Vector2(-LeftRight,0.55f);
        gamemanager.DestroyStart("D", bullet.transform.position);
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * count * 2.0f / 50), Mathf.Cos(Mathf.PI * count * 2.0f / 50));
        bulletRigid.velocity = dirVec.normalized * 8.0f;
        bullet.transform.Rotate(Vector3.back * 360 * count / 50 + Vector3.forward * 90);
        count++;
        if (count > 50)
        {
            count = 0;
            Invoke("Light", 0.75f);
        }
        else Invoke("Spin", 0.3f);
    }

    void Light() {
        ignite = true;

        light = objectmanager.MakeObj("Eyes");
        light.transform.position = (Vector2)transform.position + new Vector2(LeftRight, 0.55f);
        SpriteRenderer eyesp = light.GetComponent<SpriteRenderer>();
        BoxCollider2D eyebox = light.GetComponent<BoxCollider2D>();
        if (LeftRight == 1)
        {
            eyesp.flipX = false;
            eyebox.offset = new Vector2(1, 0);
        }
        else
        {
            eyesp.flipX = true;
            eyebox.offset = new Vector2(-1, 0);
        }
        Animator eyean = light.GetComponent<Animator>();
        eyean.SetTrigger("Action");

        Invoke("All", 1.25f);
        Invoke("LightReturn", 1.4f);
    }

    void LightReturn() {
        light = null;
        ignite = false;
    }

    void All() {
        for (int x = -8; x <= 8; x +=4) Circle(new Vector2(x, 5.5f), Vector2.down);
        for (int x = -8; x <= 8; x += 4) Circle(new Vector2(x, -5.5f), Vector2.up);
        for (int y = -4; y <= 4; y += 4) Circle(new Vector2(-9.5f, y), Vector2.right);
        for (int y = -4; y <= 4; y += 4) Circle(new Vector2(9.5f, y), Vector2.left);
        Invoke("Rocket", 3.5f);
    }

    void Circle(Vector2 createPos, Vector2 goPos) {
        GameObject bullet = objectmanager.MakeObj("EnemyCircle");
        Bullet bulletLogic = bullet.GetComponent<Bullet>();
        bulletLogic.gamemanager = gamemanager;
        Rigidbody2D bulletRigid = bullet.GetComponent<Rigidbody2D>();
        //힘,방향,위치
        bullet.transform.position = createPos;
        bulletRigid.AddForce(goPos * 5.0f, ForceMode2D.Impulse);
        if (goPos == Vector2.left) bullet.transform.Rotate(Vector3.back * 90);
        else if (goPos == Vector2.right) bullet.transform.Rotate(Vector3.forward * 90);
        else if (goPos == Vector2.up) bullet.transform.Rotate(Vector3.forward * 180);
        bulletLogic.isCircle = false;
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
