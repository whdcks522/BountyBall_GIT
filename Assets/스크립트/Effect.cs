using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Animator anim;
    public float time;
    float rotateCount;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Disable() { gameObject.SetActive(false); }

    void OnEnable()
    {
        if ((gameObject.name.Contains("Destroy"))) Invoke("Disable", 0.5f);
        else if ((gameObject.name.Contains("BombEffect"))) Invoke("Disable", 0.7f);
        else if ((gameObject.name.Contains("Generate2"))) Invoke("Disable", 0.6f);//1.0f
        else if (gameObject.name.Contains("Venom")) rotateCount = 0.0f;
        else if ((gameObject.name.Contains("BecomeSword"))) Invoke("Disable", 1.4f);//14
        else if ((gameObject.name.Contains("Eyes"))) Invoke("Disable", 1.1f);
    }

    private void Update()
    {
        if (gameObject.name.Contains("Venom") && (Time.timeScale == 1)) {
            if ((time < 4.0f))
            {
                gameObject.transform.rotation = Quaternion.identity;
                time -= Time.deltaTime;
                gameObject.transform.localScale = new Vector2(0.125f * time, 0.125f * time);
                gameObject.transform.Rotate(Vector3.back * 180 * time);
                if (time <= 0.0f) gameObject.SetActive(false);
            }
            else {
                rotateCount++;
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.Rotate(Vector3.back * 10 * rotateCount);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //총탄벽
        if ((collision.gameObject.layer == 11) && (gameObject.name.Contains("Venom")))
        {
            transform.localScale = new Vector2(0.25f, 0.25f);
            gameObject.SetActive(false);
        }
    }
    public void DestroyEffect(string oldtype)
    {
        anim.SetTrigger("Destroy");
        switch (oldtype)
        {
            case "EnemyInjection":
                transform.localScale = Vector3.one * 0.5f;
                break;
            case "Flag":
            case "P":
            case "D":
            case "F":
            case "S":
            case "E":
            case "C":
            case "B":
            case "S2":
            case "W":
            case "D2":
            case "W2":
            case "I":
            case "E2":
            case "M":
            case "V":
            case "T":
            case "BBG":
            case "C2":
            case "L":
            case "E3":
            case "BT2H":
            case "BT2S":
            case "BT2L":
            case "L2":
            case "B2":
            case "BQ":
                transform.localScale = Vector3.one * 1.5f;
                break;
            case "BD":
            case "BT":
            case "BI":
            case "BO":
            case "BR":
            case "BU":
            case "BB":
            case "BF":
            case "ZFE":
            case "BS":
            case "BH":
            case "BM":
                transform.localScale = Vector3.one * 3f;
                break;
            case "BZ":
                transform.localScale = Vector3.one * 6f;
                break;
        }
    }
}
