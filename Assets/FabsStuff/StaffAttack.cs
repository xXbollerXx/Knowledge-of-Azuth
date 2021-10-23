using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffAttack : MonoBehaviour
{
    public float ProjectileVelocity = 10f;
    public GameObject ProjectilePrefab;

    private float AttackDelay = 0.5f;
    float nextTimeAttackIsAllowed;

    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Transform SecondShotLoc;


    public AudioSource soundEffects;
    public AudioClip Pew;


    void Update()
    {
        Attack();

    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeAttackIsAllowed)
        {
            soundEffects.clip = Pew;
            soundEffects.Play();

            Vector3 SpawnPos = transform.position;
            if (playerSprite.flipX == true)// shot at left side if flipped
            {
                SpawnPos = SecondShotLoc.position;
            }
            

            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //worldMousePos.z = transform.position.z;
            Vector2 direction = (worldMousePos - transform.position);
            
            direction.Normalize();

            // Creates the bullet 
            GameObject bullet = Instantiate(ProjectilePrefab, SpawnPos, Quaternion.identity);
            // Adds velocity to the bullet
            bullet.GetComponent<Rigidbody2D>().velocity = direction * ProjectileVelocity;

            //rotate towards mouse
            Quaternion Rotation =  Quaternion.LookRotation( Vector3.forward, direction);
            bullet.transform.eulerAngles = new Vector3(0, 0, Rotation.eulerAngles.z + 90);

            //delay
            nextTimeAttackIsAllowed = (Time.time + AttackDelay) - Time.deltaTime;
        }
    }
}
