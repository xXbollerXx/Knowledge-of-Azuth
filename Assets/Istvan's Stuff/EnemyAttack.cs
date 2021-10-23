using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject Axe;

    //public float throwSpeed;

    //public float spinSpeed;

    private float timePassedFloat;
    private int timePassedInt;

    public int attackFrequency;


    public float throwForce;


    void Start()
    {
        timePassedFloat = 0f;
    }

    void Update()
    {
        timePassedInt = (int)timePassedFloat;
        timePassedFloat = timePassedFloat + 1 * Time.deltaTime;

        if (timePassedInt >= attackFrequency)
        {
            timePassedFloat = 0f;
            //attack
            //Instantiate(Axe, this.transform.position, Quaternion.identity);
            GameObject Temp = Instantiate(Axe, this.transform.position, Quaternion.identity);
            Vector2 direction = GetComponent<EnemyMove>().movingRight? new Vector2(1,1) : new Vector2(-1, 1);
            Rigidbody2D rb = Temp.GetComponent<Rigidbody2D>();
            rb.AddForce(direction.normalized * throwForce);//movement

        }

        /*if (Input.GetKeyDown("p"))
        {
            Instantiate(Axe, this.transform.position, Quaternion.identity);
        }*/
    }
}
