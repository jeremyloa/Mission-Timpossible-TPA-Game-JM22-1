using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharScript : MonoBehaviour
{
    CharacterController cont;
    [SerializeField] private Camera cam;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float gravity = -9.8f;
    Vector3 posSphere;
    Vector3 velocity;
    private Animator anim;
    float xDir, zDir;
    // Start is called before the first frame update
    void Start()
    {
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovetoDirection();
        Gravity();
        GetPos(xDir, zDir);
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
}
