using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public int TowerHP, HullHP, HitPoints, TowerArmour, HullArmour, Speed, TraverseSpeed, Damage, EnginePower, Weight, TowerWeight, HullWeight, HPperT, CurHP, Armour;
    public float ReloadTime;
    public float acc;
    public Shooting SH;
    public float timer = 0;
    public HealthBar HB;
    private void Start()
    {
        StartCoroutine("IEGetStats");
        acc = 0;

        
    }
    private void FixedUpdate()
    {
        Movement();

        if (SH.CurrentHit && Input.GetMouseButton(0) && timer >= ReloadTime)
        {
            //timer = 0;
            if (SH.CurrentHit.tag == "Enemy")
            {
                EnemyController EC = null;
                if (SH.CurrentHit.GetComponent<EnemyController>()) EC = SH.CurrentHit.GetComponent<EnemyController>();
                else if (SH.CurrentHit.GetComponentInChildren<EnemyController>()) EC = SH.CurrentHit.GetComponentInChildren<EnemyController>();
                else if (SH.CurrentHit.GetComponentInParent<EnemyController>()) EC = SH.CurrentHit.GetComponentInParent<EnemyController>();

                GunStats GS = GAME_CONTROLLER.Guns[GAME_CONTROLLER.CurGun].GetComponent<GunStats>();

                EC.TakeDamage(GAME_CONTROLLER.CalculateDamageOnHit(GS.Penetration, GS.Damage, EC.GetComponent<EnemyController>().Armour));
                timer = 0;
            }
        }
        else if(timer < ReloadTime) timer += Time.deltaTime;
        
    }

    

    void Movement()
    {
        Vector3 hor, yy;
        Vector2 ver;
        yy = new Vector2(0, 1);
        ver = new Vector2(0.0f, Input.GetAxis("Vertical"));
        if (ver.y == 0) acc = 0;
        else if (ver.y > 0 && (acc < Speed / 6)) acc += Time.deltaTime * HPperT;
        else if (ver.y < 0 && (acc > -Speed / 20)) acc -= Time.deltaTime * HPperT;



        if (acc == 0) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //transform.Translate(yy * Time.deltaTime * acc);
        GetComponent<Rigidbody2D>().AddForce(transform.up * Time.deltaTime * acc*10000);

        hor = new Vector3(0.0f, 0.0f, -Input.GetAxis("Horizontal"));
        if (ver.y < 0) hor = -hor;
        transform.Rotate(hor * TraverseSpeed * Time.deltaTime);

        if (hor.magnitude > 0.01f) GetComponent<Rigidbody2D>().velocity *= 0.995f;
    }
    IEnumerator IEGetStats()
    {
        yield return new WaitForSeconds(0.5f);
        GetStats();
    }

    void GetStats()
    {
        TowerStats TowerS = GetComponentInChildren<TowerStats>();
        TowerHP = TowerS.TowerHP;
        TowerArmour = TowerS.TowerArmour;
        TowerWeight = TowerS.TowerWeight;

        HullStats HS = GetComponentInChildren<HullStats>();
        HullHP = HS.HullHP;
        HullArmour = HS.HullAmour;
        EnginePower = HS.EnginePower;
        Speed = HS.Speed;
        HullWeight = HS.HullWeight;

        GunStats GS = GetComponentInChildren<GunStats>();
        Damage = GS.Damage;
        ReloadTime = GS.ReloadTime;

        TrackStats TrackS = GetComponentInChildren<TrackStats>();
        TraverseSpeed = TrackS.TraverseSpeed;


        Weight = TowerWeight + HullWeight;
        HPperT = EnginePower / Weight;
        HitPoints = TowerHP + HullHP;
        CurHP = HitPoints;
        HB.SetMaxHealth(HitPoints);
        Armour = (int)((float)(HullArmour + TowerArmour) /1.5f);
    }

    public void TakeDamage(int damage)
    {
        GameObject DamageHUD = Instantiate(GAME_CONTROLLER.DamageHUD, Camera.main.transform) as GameObject;
        DamageHUD.GetComponent<DamageHUD>().ShowHUD(true, damage, transform);
        if (damage <= 0)
        {
            Debug.Log("TC: Ricochet!");
            return;
        }
        CurHP -= damage;
        HB.SetHealth(CurHP);
        
        
        Debug.Log("TC: Player was hit for:" + damage);
        if (CurHP < 0)
        {
            Debug.Log("To Implement HP<0");
        }
    }

    public int GetHP() { return CurHP; }
}
