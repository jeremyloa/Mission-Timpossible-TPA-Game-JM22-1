using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Collision cl;
    [SerializeField] float destroyTime;
    float timer;
    public static bool collidePlayer;
    // Start is called before the first frame update
    void Start()
    {
        collidePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= destroyTime) Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        cl = collision;
        //Debug.Log("shoot " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("MainChar"))
        {
            Debug.Log("shoot ketemu player");
            collidePlayer = true;
        } else collidePlayer = false;
        Destroy(this.gameObject);
    }
}

