using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    
    public Vector2 forceDirection;
   

    public float spinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        gameObject.GetComponent<Rigidbody2D>().AddTorque(spinSpeed);
        

       /*forceDirectionOpposite = new Vector2(forceDirection.x * -1, forceDirection.y);

        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(forceDirectionOpposite.normalized * throwForce);*/



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerHealth>().TakeDamage(10);
        }
    }
}
