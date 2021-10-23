using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public float bulletVelocity = 10f;
    public GameObject bullet;
    public GameObject bullet1;

    private SpriteRenderer bulletSprite;
    private float playerx;   
    private float bulletx;
    

    private float AttackDelay = 0.5f;
    float nextTimeAttackIsAllowed = -1.0f;


    void Update()
    {     
        Attack();
        
    }

                      
  

    void Attack()   
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeAttackIsAllowed)
        {
            //Delaying the actual attack
            

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (Vector2)((worldMousePos - transform.position));
            direction.Normalize();


            // Creates the bullet locally
            GameObject bullet = (GameObject)Instantiate(
                                    bullet1,
                                    transform.position + (Vector3)(direction * 0.5f) + new Vector3(0,0, -2) ,
                                    Quaternion.identity);
            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;


            nextTimeAttackIsAllowed = Time.time + AttackDelay;
        }
    }
    
}
