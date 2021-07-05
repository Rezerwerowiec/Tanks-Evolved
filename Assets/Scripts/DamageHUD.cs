using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHUD : MonoBehaviour
{
    private bool isEnemy;
    private int damage;
    Transform target;
    float upPos = 0f;
    void Start()
    {
        if (isEnemy) GetComponentInChildren<Text>().color = Color.red;
        else GetComponentInChildren<Text>().color = Color.green;
        if (damage <= 0) { GetComponentInChildren<Text>().text = "Ricochet!"; GetComponentInChildren<Text>().fontStyle = FontStyle.Normal; }
        else GetComponentInChildren<Text>().text = "-" + damage;
        StartCoroutine("Die");
    }

    public void ShowHUD(bool isEnemy, int damage, Transform target)
    {
        this.target = target;
        this.damage = damage;
        this.isEnemy = isEnemy;
    }
    // Update is called once per frame
    void Update()
    {
        upPos += Time.deltaTime*1.5f;
        if (target)
        {
            transform.position = new Vector3(target.position.x, target.position.y + upPos, target.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
