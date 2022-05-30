using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public static bool recoil;
    [SerializeField] public float shootRate;
    [SerializeField] public bool autoShoot;
    public float shootRateTimer;
    [SerializeField] public int damage, speed, drop;
    [SerializeField] public GameObject bullet;
    [SerializeField] public Transform bulletPos;
    [SerializeField] public float bulletVelo;
    [SerializeField] public int bulletsPerShot;
    public MainCharAim aim;
    public MainCharScript MainChar;
    [SerializeField] public AudioClip shotSFX;
    public AudioSource src;
    public Ray ray;
    [SerializeField] public LayerMask STarget;
    // Start is called before the first frame update
    public void Start()
    {
        recoil = false;
        MainChar = GetComponent<MainCharScript>();
        src = GetComponent<AudioSource>();
        aim = GetComponentInParent<MainCharAim>();
        shootRateTimer = shootRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        ray = Camera.main.ScreenPointToRay(screenCentre);
        
    }

    public virtual void Shoot()
    {
        if (MainCharScript.loseGame == true || MainCharScript.winGame == true || PauseMenu.GamePaused == true) return;
        bulletPos.LookAt(aim.aimPos);
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
            rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);

        }
        src.PlayOneShot(shotSFX);
        if (aim.aimZoom == false) recoil = true;
    }
}
