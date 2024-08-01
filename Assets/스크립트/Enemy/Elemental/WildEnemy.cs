using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildEnemy : MonoBehaviour
{
    public GameObject host;
    float time;
    Vector3 bulletPos;
    Vector3 endPos;
    int rannum;

    private void Update()
    {
        if ((!host.GetComponent<FollowEnemy>().ready2) && (time > 0.0f))
        {
            time -= Time.deltaTime;
            endPos = bulletPos - transform.position;
            if (rannum == 0) host.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp((endPos), new Vector2(-endPos.y, endPos.x), 1).normalized * 10.0f;
            else if (rannum == 1) host.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp((endPos), new Vector2(endPos.y, -endPos.x), 1).normalized * 10.0f;
            if (time < 0.0f)
            {
                host.GetComponent<FollowEnemy>().ready = true;
                host.GetComponent<FollowEnemy>().ready2 = true;
                host.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        transform.position = host.transform.position;
        if (!host.gameObject.activeSelf) gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)//플레이어 총탄 피격시
        {
            bulletPos = collision.transform.position;
            time = 0.5f;
            rannum = Random.Range(0,2);
            host.GetComponent<FollowEnemy>().ready = true;
            host.GetComponent<FollowEnemy>().ready2 = false;
        }
    }


}
