using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currHealth;
    //private Animator anim;
    [SerializeField] GameObject player;
    [SerializeField] MainCharScript playerScript;
    [SerializeField] public HealthBarScript healthBar;
    //CharacterController cont;
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

    [SerializeField] GameObject patrol1, patrol2, patrol3, patrol4, patrol5, patrol6, patrol7, patrol8;
    //AStarPathFinding pathfind;
    //bool walk;

    Rigidbody rigid;
    bool rand;
    bool giveScore5;
    bool enemyDown;

    [SerializeField] GameObject pistolAmmo, sniperAmmo;
    
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(patrol1.transform.position.x, transform.position.y, patrol1.transform.position.z);
        //currentDest = transform;
        //walk = false;
        //pathfind = GetComponent<AStarPathFinding>();
        //pathfind.end = currentDest;
        bulletVelo = 600;
        shootRate = 1/10;
        damage = 1; speed = 800; drop = 30;
        currHealth = maxHealth;
        //cont = GetComponent<CharacterController>();
        //anim = GetComponent<Animator>();
        shootRateTimer = shootRate;
        src = GetComponent<AudioSource>();
        giveScore5 = false;
        enemyDown = false;
        rigid = GetComponent<Rigidbody>();
        pistolAmmo.SetActive(false);
        sniperAmmo.SetActive(false);
        rand = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currHealth>0)
        {
            if (NoticePlayer() == false) Patrol();
            else Chase();
            //pathfind.end = currentDest;

            //if (CheckShoot() == true) Shoot();
        }
        else
        {
            if (rand == false)
            {
                if (TeleportScript.EnterPreTeleport == true) TeleportScript.TeleportScore += 1;
                int rand1 = Random.Range(1, 4);
                if (rand1 == 1 || rand1 == 2)
                {
                    if (rand1 == 1)
                    {
                        pistolAmmo.transform.position = transform.position;
                        pistolAmmo.SetActive(true);
                    }
                    else
                    {
                        sniperAmmo.transform.position = transform.position;
                        sniperAmmo.SetActive(true);
                    }
                }
                rand = true;
            }

            if (enemyDown==false)
            {
                rigid.mass = 1000;
                rigid.isKinematic = false;
                rigid.useGravity = false;
                if (giveScore5==false)
                {
                    MainCharScript.missionFiveScore += 1;
                    giveScore5 = true;
                }
                if (transform.position.y >= -50 && enemyDown == false)
                    transform.position = new Vector3(transform.position.x, transform.position.y - (Time.deltaTime * 10), transform.position.z);
                else
                {
                    enemyDown = true;
                    gameObject.SetActive(false);
                }
            } 
            else
            {
                
            }
        }
    }

    void Patrol()
    {
        Debug.Log("enemy patroling");
        ////if (newstart==true)
        ////{
        ////    transform.position = new Vector3(patrol1.transform.position.x, transform.position.y, patrol1.transform.position.z);
        ////    newstart = false;
        ////    return;
        ////}
        //Debug.Log("trans x = " + transform.position.x + " trans z = " + transform.position.z 
        //    + " curr x = " + currentDest.transform.position.x + " curr z = " + currentDest.transform.position.z);
        ////bool none = (transform.position.x != patrol1.transform.position.x && transform.position.z != patrol1.transform.position.z) 
        ////    || (transform.position.x != patrol2.transform.position.x && transform.position.z != patrol2.transform.position.z)
        ////    || (transform.position.x != patrol3.transform.position.x && transform.position.z != patrol3.transform.position.z)
        ////    || (transform.position.x != patrol4.transform.position.x && transform.position.z != patrol4.transform.position.z)
        ////    || (transform.position.x != patrol5.transform.position.x && transform.position.z != patrol5.transform.position.z)
        ////    || (transform.position.x != patrol6.transform.position.x && transform.position.z != patrol6.transform.position.z)
        ////    || (transform.position.x != patrol7.transform.position.x && transform.position.z != patrol7.transform.position.z)
        ////    || (transform.position.x != patrol8.transform.position.x && transform.position.z != patrol8.transform.position.z);

        ////bool none = (transform.position.x != patrol1.transform.position.x && transform.position.z != patrol1.transform.position.z)
        ////    && (transform.position.x != patrol2.transform.position.x && transform.position.z != patrol2.transform.position.z)
        ////    && (transform.position.x != patrol3.transform.position.x && transform.position.z != patrol3.transform.position.z)
        ////    && (transform.position.x != patrol4.transform.position.x && transform.position.z != patrol4.transform.position.z)
        ////    && (transform.position.x != patrol5.transform.position.x && transform.position.z != patrol5.transform.position.z)
        ////    && (transform.position.x != patrol6.transform.position.x && transform.position.z != patrol6.transform.position.z)
        ////    && (transform.position.x != patrol7.transform.position.x && transform.position.z != patrol7.transform.position.z)
        ////    && (transform.position.x != patrol8.transform.position.x && transform.position.z != patrol8.transform.position.z);

        //if (walk==true)
        //{
        //    Debug.Log("walk is true");
        //    if (transform.position.x != currentDest.position.x)
        //    {
        //        Debug.Log("trans is not currdest yet");
        //        xDir = pathfind.grid.path[0].gridX;
        //        zDir = pathfind.grid.path[0].gridY;
        //        Debug.Log("is xdir = " + xDir + " zdir = " + zDir);
        //        //transform.position = new Vector3(pathfind.grid.path[0].gridX, transform.position.y, pathfind.grid.path[0].gridY);
        //        pathfind.grid.path.Remove(pathfind.grid.path[0]);
        //    }
        //    else walk = false;
        //} else
        //{
        //    Debug.Log("walk is false");
        //    if (transform.position.x == patrol1.transform.position.x
        //        && transform.position.z == patrol1.transform.position.z)
        //    {
        //        currentDest.position = patrol2.transform.position;
        //        Debug.Log("is set to patrol2");
        //    }
        //    else if (transform.position.x == patrol2.transform.position.x
        //        && transform.position.z == patrol2.transform.position.z)
        //    {
        //        currentDest.position = patrol3.transform.position;
        //        Debug.Log("is set to patrol3");
        //    }
        //    else if (transform.position.x == patrol3.transform.position.x
        //        && transform.position.z == patrol3.transform.position.z)
        //    {
        //        currentDest.position = patrol4.transform.position;
        //        Debug.Log("is set to patrol4");
        //    }
        //    else if (transform.position.x == patrol4.transform.position.x
        //        && transform.position.z == patrol4.transform.position.z)
        //    {
        //        currentDest.position = patrol5.transform.position;
        //        Debug.Log("is set to patrol5");
        //    }
        //    else if (transform.position.x == patrol5.transform.position.x
        //        && transform.position.z == patrol5.transform.position.z)
        //    {
        //        currentDest.position = patrol6.transform.position;
        //        Debug.Log("is set to patrol6");
        //    }
        //    else if (transform.position.x == patrol6.transform.position.x
        //        && transform.position.z == patrol6.transform.position.z)
        //    {
        //        currentDest.position = patrol7.transform.position;
        //        Debug.Log("is set to patrol7");
        //    }
        //    else if (transform.position.x == patrol7.transform.position.x
        //        && transform.position.z == patrol7.transform.position.z)
        //    {
        //        currentDest.position = patrol8.transform.position;
        //        Debug.Log("is set to patrol8");
        //    }
        //    else if (transform.position.x == patrol8.transform.position.x
        //        && transform.position.z == patrol8.transform.position.z)
        //    {
        //        currentDest.position = patrol1.transform.position;
        //        Debug.Log("is set to patrol1");
        //    }
        //    walk = true;
        //    Debug.Log("walk is set true");
        //    Debug.Log("curr dest is = " + currentDest.position.x + " & " + currentDest.position.z);
        //}
        ////if (none==true)
        ////{
        ////    Debug.Log("none is true");
        ////    if (walk == false)
        ////    {
        ////        Debug.Log("walk is false");
        ////        transform.position = new Vector3(patrol1.transform.position.x, transform.position.y, patrol1.transform.position.z);
        ////    }
        ////    else
        ////    {


        ////    }
        ////}
        ////else
        ////{

        ////}
        ////xDir = xDir + 1;
        ////zDir = zDir + 1;
        ////transform.position = transform.position + (transform.forward * moveSpeed * Time.deltaTime);
        ////xDir = Input.GetAxis("Horizontal");
        ////zDir = Input.GetAxis("Vertical");
        //GetPos(xDir, zDir);
        //Gravity();
    }
    
    void Chase()
    {
        Debug.Log("enemy chasing player");
        if (Vector3.Distance(targetPlayer.position, transform.position) >= 10f)
        {
            transform.LookAt(targetPlayer);
            transform.position += transform.forward * 20 * Time.deltaTime;
            Debug.Log("enemy move " + transform.position);
        }
        if (CheckShoot() == true) Shoot();
    }
    //bool IsGrounded()
    //{
    //    posSphere = new Vector3(transform.position.x,
    //        transform.position.y - groundYOffset, transform.position.z);
    //    if (Physics.CheckSphere(posSphere, cont.radius - 0.05f, groundMask)) return true;
    //    else return false;
    //}
    //void Gravity()
    //{
    //    if (IsGrounded() == false) velocity.y += gravity * Time.deltaTime;
    //    else if (velocity.y < 0) velocity.y = -2;
    //    //cont.Move(velocity * Time.deltaTime);
    //}
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
        shootRateTimer =+ Time.deltaTime;

        if (shootRateTimer < shootRate) return false;
        if (MainCharScript.loseGame == true || MainCharScript.winGame == true || PauseMenu.GamePaused == true) return false;
        if (currHealth <= 0) return false;
        if (AttackPlayer() == true) return true;

        return false;
    }

    public void Shoot()
    {
        shootRateTimer = 0;
        Debug.Log("Shoot from Enemy");
        bulletPos.LookAt(targetPlayer.transform);

        GameObject currBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody rigid = currBullet.GetComponent<Rigidbody>();
        rigid.AddForce(bulletPos.forward * bulletVelo, ForceMode.Impulse);
        src.PlayOneShot(shotSFX);

        if (Bullet.collidePlayer==true)
            playerScript.healthSystem();
    }
}
