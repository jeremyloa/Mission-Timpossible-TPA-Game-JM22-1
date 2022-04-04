using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{

    [SerializeField] float shootRate;
    [SerializeField] bool autoShoot;
    float shootRateTimer;
    float tempShootRate;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] float bulletVelo;
    MainCharAim aim;

    [SerializeField] AudioClip shotSFX;
    AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        aim = GetComponentInParent<MainCharAim>();
        shootRateTimer = tempShootRate = shootRate;
    }


    // Update is called once per frame
    void Update()
    {
        if (CheckShoot()==true) Shoot();
        shootRate = tempShootRate;
    }

    bool CheckShoot()
    {
        shootRateTimer = shootRateTimer + Time.deltaTime;
        if (shootRateTimer < shootRate) return false;
        else if (autoShoot == true && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        else if (autoShoot == true && Input.GetKey(KeyCode.Mouse0))
        {
            shootRate = shootRate / 40;
            return true;
        }
        return false;
    }

    void Shoot()
    {
        shootRateTimer = 0;
       // Debug.Log("Shoot");
        bulletPos.LookAt(aim.aimPos);
        GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
        rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);
        src.PlayOneShot(shotSFX);
    }
}
