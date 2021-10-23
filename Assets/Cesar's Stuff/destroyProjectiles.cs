using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyProjectiles : MonoBehaviour
{
 
    void Awake ()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.7f);

        Destroy(gameObject);

        
    }
}
