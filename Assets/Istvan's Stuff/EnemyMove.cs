 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed;
    public float fallDistance;

    public bool movingRight;

    public GameObject groundDetect;
    private float groundDetectX;
    private float groundDetectY;
    public GameObject enemyFacecam;

    private float facecamX;
    private float facecamY;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        groundDetectY = groundDetect.transform.localPosition.y;
        groundDetectX = groundDetect.transform.localPosition.x;
        enemyFacecam = GetComponentInChildren<Camera>().gameObject;

        facecamX = enemyFacecam.transform.localPosition.x;
        facecamY = enemyFacecam.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.P)) { transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); }
        if(movingRight == true)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            
        }
        
        if(movingRight == false)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }


        RaycastHit2D hit = Physics2D.Raycast(groundDetect.transform.position, Vector2.down, fallDistance);

        if(hit.collider == null) //turn around
        {
            Debug.Log("hit is null");

            movingRight = !movingRight;

            if(movingRight == true)
            {
                groundDetect.transform.localPosition = new Vector2(groundDetectX, groundDetectY);
                sprite.flipX = false;
                enemyFacecam.transform.localPosition = new Vector3(facecamX, facecamY, -10);
            }
            
            if(movingRight != true)
            {
                groundDetect.transform.localPosition = new Vector2(groundDetectX * -1, groundDetectY);
                sprite.flipX = true;
                enemyFacecam.transform.localPosition = new Vector3(facecamX * -1, facecamY, -10);
            }
        }
    }
}
