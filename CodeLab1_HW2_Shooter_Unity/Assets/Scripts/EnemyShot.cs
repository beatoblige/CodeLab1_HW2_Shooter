using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{

    public float shotSpeed = 7f;
    public GameObject impactEffect;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(shotSpeed * Time.deltaTime, 0f, 0f); //Enemy shot now (-=) not (+=)
        
    }

    private void OnTriggerEnter2D(Collider2D other)   //shot will collide with another object 
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            HealthManager.instance.HurtPlayer();
        }
        
        Destroy(this.gameObject);  //destroy game object attached to the script 
    }

    private void OnBecameInvisible()  //shot will destroy once shot becomes invisible. 
    {
        Destroy(gameObject);
    }
}
