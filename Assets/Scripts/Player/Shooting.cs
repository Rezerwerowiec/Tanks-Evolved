using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject CurrentHit;
    public Transform CrossHair;
    void FixedUpdate()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.up * 100;
        Debug.DrawRay(startPos, endPos);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(startPos, endPos);
        if (hit != false)
        {
            CurrentHit = hit.transform.gameObject;
            ChangePosition(hit.transform.position);
        } else ChangePosition(new Vector3(999, 999, 999));
    }

    void ChangePosition(Vector3 localisation)
    {
        //Debug.Log("Send an info to crosshair");
        CrossHair.position = localisation;
        
    }
}
