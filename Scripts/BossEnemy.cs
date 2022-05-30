using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public int maxHealth = 2000;
    public int currHealth;
    [SerializeField] GameObject player;
    [SerializeField] MainCharScript playerScript;
    [SerializeField] public HealthBarScript healthBar;
    public float xDir, zDir;
    public int damage, speed, drop;
    float shootRate, shootRateTimer, bulletVelo;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos;
    [SerializeField] AudioClip shotSFX;
    AudioSource src;
    //public Transform currentDest;
    [SerializeField] int noticeRadius, attackRadius;
    [SerializeField] Transform targetPlayer;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.8f;
    //Vector3 posSphere, velocity;


    [SerializeField] LayerMask playerMask;
    [SerializeField] float moveSpeed = 10;

    //AStarPathFinding pathfind;
    //bool walk;

    Rigidbody rigid;
    bool enemyDown;
    // Start is called before the first frame update

    [SerializeField] GameObject RobossUI;
    bool BossActive;
    
    void Start()
    {
        //transform.position = new Vector3(patrol1.transform.position.x, transform.position.y, patrol1.transform.position.z);
        //currentDest = transform;
        //walk = false;
        //pathfind = GetComponent<AStarPathFinding>();
        //pathfind.end = currentDest;
        bulletVelo = 600;
        shootRate = 1 / 10;
        damage = 1; speed = 800; drop = 30;
        currHealth = maxHealth;
        //cont = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        shootRateTimer = shootRate;
        src = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        RobossUI.SetActive(false);
        BossActive = false;
        enemyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, 300, playerMask))
        {
            RobossUI.SetActive(true);
            BossActive = true;
        }

        if (BossActive == true)
        {
            if (currHealth > 0)
            {
                if (NoticePlayer() == false) Patrol();
                else Chase();
            }
            else
            {
                if (enemyDown == false)
                {
                    rigid.mass = 1000;
                    rigid.isKinematic = false;
                    rigid.useGravity = false;
                    transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 10), transform.position.z);
                    if (transform.localPosition.y <= -10)
                    {
                        enemyDown = true;
                    }
                } 
                else
                {
                    BossActive = false;
                    RobossUI.SetActive(false);
                    MainCharScript.missionSix = 1;
                    MainCharScript.winGame = true;
                    gameObject.SetActive(false);
                }
            }

        }
    }

    void Patrol()
    {
        Debug.Log("boss enemy patroling");
    }

    void Chase()
    {
        Debug.Log("boss enemy chasing player");
        if (Vector3.Distance(targetPlayer.position, transform.position) >= 30f)
        {
            transform.LookAt(targetPlayer);
            transform.position += transform.forward * 5 * Time.deltaTime;
            Debug.Log("boss enemy move " + transform.position);
        }
        if (CheckShoot() == true) Shoot();
    }

    //void GetPos(float xDir, float zDir)
    //{
    //    //transform.position = Vector3.MoveTowards(transform.position, new Vector3(xDir, transform.position.y, zDir), Time.deltaTime * 5);
    //    anim.SetFloat("Pos X", xDir);
    //    anim.SetFloat("Pos Y", zDir);
    //}

    bool NoticePlayer()
    {
        return Physics.CheckSphere(transform.position, noticeRadius, playerMask);
    }

    bool AttackPlayer()
    {
        return Physics.CheckSphere(transform.position, attackRadius, playerMask);
    }
    bool CheckShoot()
    {
        shootRateTimer = shootRateTimer + Time.deltaTime;

        if (shootRateTimer < shootRate) return false;
        if (MainCharScript.loseGame == true || MainCharScript.winGame == true || PauseMenu.GamePaused == true) return false;
        if (currHealth <= 0) return false;
        if (AttackPlayer() == true) return true;

        return false;
    }

    public void Shoot()
    {
        shootRateTimer = 0;
        Debug.Log("Shoot from Boss Enemy");
        bulletPos.LookAt(targetPlayer.transform);

        GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
        rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);
        src.PlayOneShot(shotSFX);

        if (Bullet.collidePlayer == true)
            playerScript.healthSystem();
    }
}
