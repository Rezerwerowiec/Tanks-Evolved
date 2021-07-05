using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public int HitPoints, Armour, Speed, CurHP, Penetration, Damage, xp;
    public Transform[] CheckPoints = null;
    public int i = 0;
    public Transform Tower = null;
    public GameObject CurrentHit;
    public HealthBar HB;
    private Transform player;
    public float ShootingSpeed = 2, FocusSpeed= 3;
    private float shootingTimer = 0;

    TankController TC;
    private bool is_shooting, is_focusing;

    private void Start()
    {
        TC = GameObject.FindGameObjectWithTag("Player_Controller").GetComponent<TankController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CurHP = HitPoints;
        HB.SetMaxHealth(HitPoints);
    }

    void FixedUpdate()
    {
        
        ShootingSystem();
        RayCastSystem();
        if(!is_focusing)
            Movement();

    }
    void ShootingSystem()
    {
        //Debug.Log("EC: Pen: " + Penetration + ", Damage: " + Damage + ", Armor: " + TC.Armour);
        if (shootingTimer > ShootingSpeed)
        {
            TC.TakeDamage(GAME_CONTROLLER.CalculateDamageOnHit(Penetration, Damage, TC.Armour));
            
            shootingTimer = 0f;
            
        }
    }
    void RayCastSystem()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = player.position - transform.position;
        //Debug.DrawRay(startPos, endPos);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(startPos, endPos);
        if (hit != false)
        {
            //Debug.Log("Found: " + hit.transform);
            CurrentHit = hit.transform.gameObject;


        }
        else
        {
            is_shooting = false;
            is_focusing = false;
            CurrentHit = null;
            return;
        }
        if (CurrentHit.tag == "Player" || CurrentHit.tag == "Player_Controller")
        {
            TowerRotating();
            
            is_focusing = true;
            
        }
        else
        {
            is_shooting = false;
            is_focusing = false;
            shootingTimer = 0;
        }
    }
    void Movement()
    {
        if (Vector3.Distance(transform.position, CheckPoints[i].position) > 1.5f)
        {
            transform.Translate(new Vector3(0, 1, 0) * Speed * Time.deltaTime);
            Vector3 diff = CheckPoints[i].position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Quaternion quat = Quaternion.Euler(0f, 0f, rot_z - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 0.052f);

        }
        else i++;

        if (i == CheckPoints.Length) i = 0;
    }

    void TowerRotating()
    {
        Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion quat = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        Tower.transform.rotation = Quaternion.Lerp(Tower.transform.rotation, quat, 0.01f * FocusSpeed);
        if(Vector2.Distance(new Vector2(Tower.transform.rotation.z, Tower.transform.rotation.w), new Vector2(quat.z, quat.w)) < 0.1f)
        {
            is_shooting = true;
            shootingTimer += Time.deltaTime;
            //Debug.Log("EC: Preparing to shoot...");
        } else
        {
            is_shooting = false;
            shootingTimer = 0;
        }
        //Debug.Log(Vector2.Distance(new Vector2(Tower.transform.rotation.z, Tower.transform.rotation.w), new Vector2(quat.z, quat.w)));
    }




    public void TakeDamage(int damage)
    {
        GameObject DamageHUD = Instantiate(GAME_CONTROLLER.DamageHUD, Camera.main.transform) as GameObject;
        DamageHUD.GetComponent<DamageHUD>().ShowHUD(false, damage, transform);
        if (damage <= 0)
        {
            Debug.Log("EC: Ricochet!");
            return;
        }
        CurHP -= damage;
        Debug.Log("EC: Damage taken: " + damage);


        if (CurHP <= 0)
        {
            GAME_CONTROLLER.ExperiencePoints = xp;
            Destroy(gameObject);
        }
        HB.SetHealth(CurHP);
    }
}
