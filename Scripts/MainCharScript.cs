using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharScript : MonoBehaviour
{
    public static int missionOne, missionTwo, missionThree, missionFour, missionFive, missionSix;
    public static int missionThreeScore, missionFourScore, missionFiveScore;
    public int maxHealth = 100;
    public int currHealth;
    public HealthBarScript healthBar;
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
    // Start is called before the first frame update
    void Start()
    {
        trainingWalls.SetActive(true); villageWalls.SetActive(true);
        pistolAvail = sniperAvail = pistolOn = sniperOn = checkNearAsuna = false;
        missionOne = missionTwo = missionThree = missionFour = missionFive = missionSix = -1;
        missionThreeScore = missionFourScore = 0;
        currHealth = maxHealth;
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        aim = GetComponent<MainCharAim>();
        healthBar.SetMax(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        MovetoDirection();
        Gravity();
        GetPos(xDir, zDir);
        if (Physics.CheckSphere(transform.position, 1, asunaMask) == true || aim.transform.name.Equals("SphereAsuna")) checkNearAsuna = true;
        else checkNearAsuna = false;
        if (missionFour == 1) trainingWalls.SetActive(false);
        if (missionFive == 1) villageWalls.SetActive(false);
    }

    void MovetoDirection ()
    {
        xDir = Input.GetAxis("Horizontal");
        zDir = Input.GetAxis("Vertical");

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
