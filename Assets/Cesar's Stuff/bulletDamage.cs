using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    [SerializeField] GameObject Particles;
    [SerializeField] float LifeTime = 5f;
    public int Damage;
    

    private void Start()
    {
        Destroy(this.gameObject, LifeTime);
        Damage = 25;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") return;
        if (other.gameObject.CompareTag("Platform")) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth enemyHealth = other.gameObject.GetComponent<enemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
                Despawn();
            }
        }

        Despawn();
    }


    void Despawn()
    {
        Instantiate(Particles, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
