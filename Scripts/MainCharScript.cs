using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharScript : MonoBehaviour
{
    public static bool loseGame, winGame;
    public static int missionOne, missionTwo, missionThree, missionFour, missionFive, missionSix;
    public static int missionThreeScore, missionFourScore, missionFiveScore;
    public static int maxHealth = 100;
    public static int currHealth;
    [SerializeField] public HealthBarScript healthBar;
    public bool pistolAvail, sniperAvail, pistolOn, sniperOn, checkNearAsuna;
    CharacterController cont;
    [SerializeField] private Camera cam;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] LayerMask asunaMask;
    [SerializeField] GameObject trainingWalls, villageWalls;
    Vector3 posSphere;
    Vector3 velocity;
    private Animator anim;
    float xDir, zDir;
    MainCharAim aim;
    [SerializeField] CanvasGroup SniperIcon, PistolIcon;
    [SerializeField] TMPro.TMP_Text AmmoCount;
    PistolAmmo pistolAmmo;
    SniperAmmo sniperAmmo;
    float walkRate, walkRateTimer;
    [SerializeField] AudioClip walkSFX;
    AudioSource src;
    [SerializeField] int speedUp;
    int display;

    float checkTimer;
    // Start is called before the first frame update
    void Start()
    {
        checkTimer = 60;
        loseGame = winGame = false;
        src = GetComponent<AudioSource>();
        trainingWalls.SetActive(true); villageWalls.SetActive(true);
        pistolAvail = sniperAvail = pistolOn = sniperOn = checkNearAsuna = false;
        missionOne = missionTwo = missionThree = missionFour = missionFive = missionSix = -1;
        missionThreeScore = missionFourScore = 0;
        currHealth = maxHealth;
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        aim = GetComponent<MainCharAim>();
        pistolAmmo = GetComponent<PistolAmmo>();
        sniperAmmo = GetComponent<SniperAmmo>();
        healthBar.SetMax(maxHealth);
        AmmoCount.SetText("0/0");
        walkRateTimer = walkRate = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pistolOn == true)
        {
            display = pistolAmmo.baseAmmo + pistolAmmo.extraAmmo;
            AmmoCount.SetText(pistolAmmo.currentAmmo + "/" + display);
            PistolIcon.alpha = 1;
        } 
        else
        {
            PistolIcon.alpha = 0.5f;
        }

        if (sniperOn == true)
        {
            display = sniperAmmo.baseAmmo + sniperAmmo.extraAmmo;
            AmmoCount.SetText(sniperAmmo.currentAmmo + "/" + display);
            SniperIcon.alpha = 1;
        }
        else
        {
            SniperIcon.alpha = 0.5f;
        }
        walkRateTimer = walkRateTimer + Time.deltaTime;
        MovetoDirection();
        //Gravity();
        GetPos(xDir, zDir);
        if (Physics.CheckSphere(transform.position, 1, asunaMask) == true || aim.transform.name.Equals("SphereAsuna")) checkNearAsuna = true;
        else checkNearAsuna = false;
        if (missionFour == 1) trainingWalls.SetActive(false);
        if (missionFive == 1) villageWalls.SetActive(false);

        if (Input.GetKey(KeyCode.Z) && currHealth>0)
        {
            healthSystem();
            //Debug.Log("CurrHealth Z : " + currHealth);
        }

        if (checkTimer > 0)
        {
            checkTimer -= Time.deltaTime;
            //Debug.Log("timer = " + checkTimer + " in int = " + Mathf.FloorToInt(checkTimer));
        }
        //else Debug.Log("timer is 0");
    }

    public void healthSystem()
    {
        if (currHealth > 0)
        {
            currHealth = currHealth - 1;
            healthBar.SetHealth(currHealth);
        }
        else loseGame = true;
    }
    
    void MovetoDirection ()
    {
        Vector3 prevLoc = transform.eulerAngles;
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");
        //Debug.Log("xDir before = " + prevLoc.x);
        //Debug.Log("xDir curr = " + xDir);
        //Debug.Log("zDir before = " + prevLoc.z);
        //Debug.Log("zDir curr = " + zDir);
        Vector3 newLoc = new Vector3(xDir, transform.eulerAngles.y, zDir);
        if (prevLoc!=newLoc)
        {
        //Debug.Log(prevLoc + "and " + newLoc);
            if (walkRateTimer >= walkRate)
            {
                //Debug.Log(walkRateTimer + "dan " + walkRate);
                walkRateTimer = 0;
                src.PlayOneShot(walkSFX);
                //Debug.Log("NEW" + walkRateTimer + "dan " + walkRate);
            }
        }

        if (pistolOn == false && sniperOn == false)
        {
            //Debug.Log("No weapon used");
            if (prevLoc.x != newLoc.x)
            {
                //Debug.Log("xDir before = " + prevLoc.x);
                //Debug.Log("xDir curr = " + xDir);
                float deltaX = newLoc.x - prevLoc.x;
                xDir = prevLoc.x + (deltaX * speedUp);
                //Debug.Log("xDir after = " + xDir);
                Debug.Log("Speed up xDir");
            }

            if (prevLoc.z != newLoc.z)
            {
                //Debug.Log("zDir before = " + prevLoc.z);
                //Debug.Log("zDir curr = " + zDir);
                float deltaZ = zDir - prevLoc.z;
                zDir = prevLoc.z + (deltaZ * speedUp);
                //Debug.Log("zDir after = " + zDir);
                Debug.Log("Speed up zDir");
            }

        }

        // Vector3 moveDir = new Vector3(xDir, 0.0f, zDir);
        // transform.position += moveDir;
    }

    bool IsGrounded()
    {
        posSphere = new Vector3(transform.position.x,
            transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(posSphere, cont.radius - 0.05f, groundMask)) return true;
        else return false;
    }
    void Gravity()
    {
        if (IsGrounded() == false) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;
        cont.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posSphere, cont.radius-0.05f);
    }

    void GetPos(float xDir, float zDir)
    {
        anim.SetFloat("Pos X", xDir);
        anim.SetFloat("Pos Y", zDir);
    }

    void ReceiveDamage (int damage)
    {
        currHealth = currHealth - damage;
    }
}
