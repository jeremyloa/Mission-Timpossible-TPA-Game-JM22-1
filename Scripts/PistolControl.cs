using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : MonoBehaviour
{

    [SerializeField] float shootRate;
    [SerializeField] bool autoShoot;
    float shootRateTimer;
    float tempShootRate;
    public int damage, speed, drop;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] float bulletVelo;
    [SerializeField] int bulletsPerShot;
    MainCharAim aim;
    MainCharScript MainChar;

    [SerializeField] AudioClip shotSFX;
    AudioSource src;

    PistolAmmo ammo;

    Ray ray;
    [SerializeField] LayerMask STarget;
    // Start is called before the first frame update
    void Start()
    {
        MainChar = GetComponent<MainCharScript>();
        damage = 70; speed = 600; drop = 50;
        src = GetComponent<AudioSource>();
        aim = GetComponentInParent<MainCharAim>();
        ammo = GetComponent<PistolAmmo>();
        shootRateTimer = shootRate;
    }


    // Update is called once per frame
    void Update()
    {
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
        shootRateTimer = 0;
       // Debug.Log("Shoot");
        bulletPos.LookAt(aim.aimPos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, STarget) 
            || aim.hit.transform.name.Equals("STarget"))
        {
            MainCharScript.missionThreeScore = MainCharScript.missionThreeScore + 1;
            Debug.Log("Shoot Target Success " + MainCharScript.missionThreeScore);
        }
        for (int i=0; i<bulletsPerShot; i++)
        {
            GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
            rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);

        }
        src.PlayOneShot(shotSFX);
        ammo.currentAmmo--;
        Debug.Log("Shoot Success");
    }
}
