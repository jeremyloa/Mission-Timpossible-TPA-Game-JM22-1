using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : WeaponControl
{
    //public static bool recoil;
    //[SerializeField] float shootRate;
    //[SerializeField] bool autoShoot;
    //float shootRateTimer;
    //public int damage, speed, drop;
    //[SerializeField] GameObject bullet;
    //[SerializeField] Transform bulletPos;
    //[SerializeField] float bulletVelo;
    //[SerializeField] int bulletsPerShot;
    //MainCharAim aim;
    //MainCharScript MainChar;

    //[SerializeField] AudioClip shotSFX;
    //AudioSource src;

    //Ray ray;
    //[SerializeField] LayerMask STarget;

    //NEW
    PistolAmmo ammo;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //recoil = false;
        //MainChar = GetComponent<MainCharScript>();
        //damage = 70; speed = 600; drop = 50;
        //src = GetComponent<AudioSource>();
        //aim = GetComponentInParent<MainCharAim>();
        //shootRateTimer = shootRate;

        //NEW
        ammo = GetComponent<PistolAmmo>();
    }


    // Update is called once per frame
    void Update()
    {
        //NEW
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        if (CheckShoot()==true) Shoot();
        Debug.Log(ammo.currentAmmo);
    }

    bool CheckShoot()
    {
        if (MainChar.pistolOn == false) return false;
        shootRateTimer = shootRateTimer + Time.deltaTime;
        if (shootRateTimer < shootRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (autoShoot == false && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (autoShoot == true && Input.GetKey(KeyCode.Mouse0)) return true; 
        return false;
    }

    public void Shoot()
    {
        base.Shoot();
        // shootRateTimer = 0;
        //// Debug.Log("Shoot");
        // bulletPos.LookAt(aim.aimPos);
        // for (int i=0; i<bulletsPerShot; i++)
        // {
        //     GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        //     Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
        //     rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);

        // }
        // src.PlayOneShot(shotSFX);
        // recoil = true;
        //Debug.Log("Shoot Success");

        //NEW
        if (aim.hit.transform.name.StartsWith("SoldierEnemy") && Bullet.cl.gameObject.name.StartsWith("SoldierEnemy"))
        {
            Debug.Log("shoot ketemu enemy from pistol");
            if (aim.hit.transform.name.EndsWith("BOSS") && Bullet.cl.gameObject.name.EndsWith("BOSS"))
            {
                BossEnemy bossScript = Bullet.cl.gameObject.GetComponent<BossEnemy>();
                Debug.Log("boss enemy health = " + bossScript.currHealth + " name = " + Bullet.cl.gameObject.name);
                if (bossScript.currHealth > 0)
                {
                    bossScript.currHealth = bossScript.currHealth - 70;
                    bossScript.healthBar.SetHealth(bossScript.currHealth / 20);
                    Debug.Log("boss enemy new health = " + bossScript.currHealth + " name = " + Bullet.cl.gameObject.name);
                }
                return;
            }
            SoldierEnemy soldierScript = Bullet.cl.gameObject.GetComponent<SoldierEnemy>();
            Debug.Log("enemy health = " + soldierScript.currHealth + " name = " + Bullet.cl.gameObject.name);
            if (soldierScript.currHealth > 0)
            {
                soldierScript.currHealth = soldierScript.currHealth - 70;
                soldierScript.healthBar.SetHealth(soldierScript.currHealth);
                Debug.Log("enemy new health = " + soldierScript.currHealth + " name = " + Bullet.cl.gameObject.name);
            }
        }
        ammo.currentAmmo--;
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, STarget) 
            || aim.hit.transform.name.Equals("STarget"))
        {
            MainCharScript.missionThreeScore = MainCharScript.missionThreeScore + 1;
            Debug.Log("Shoot Target Success " + MainCharScript.missionThreeScore);
        }

    }
}
