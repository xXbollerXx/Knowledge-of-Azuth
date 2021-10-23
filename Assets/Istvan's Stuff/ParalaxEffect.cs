using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private float LengthX, startposX;
    public GameObject cam;
    public float parallaxEffectX;

    ////////

    private float LengthY, startposY;
    public float parallaxEffectY;

    public bool justFollowTheCameraForFucksSake = false;

    void Start()
    {
        startposX = transform.position.x;
        LengthX = GetComponent<SpriteRenderer>().bounds.size.x;

        ////////

        startposY = transform.position.y;
        LengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffectX));
        float dist = (cam.transform.position.x * parallaxEffectX);

        transform.position = new Vector3(startposX + dist, transform.position.y, transform.position.z);

        if (temp > startposX + LengthX) startposX += LengthX;
        else if (temp < startposX - LengthX) startposX -= LengthX;

        ///////
        
        if (justFollowTheCameraForFucksSake == false)
        {
            float tempY = (cam.transform.position.y * (1 - parallaxEffectY));
            float distY = (cam.transform.position.y * parallaxEffectY);

            transform.position = new Vector3(transform.position.x, startposY + distY, transform.position.z);
        }
        

        if(justFollowTheCameraForFucksSake == true)
        {
            transform.position = new Vector3(cam.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
