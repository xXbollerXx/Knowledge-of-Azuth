using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupScript : MonoBehaviour
{
    private float targetFOV;
    //private Color flashColor = new Color(0, 0, 1, 0.2f);
    private Color defaultColor = Color.clear;
    public RawImage damageImage;
    public float flashSpeed = 1f;
    public int numFlashes = 4;

    private void Awake()
    {
        //Set the Target FOV of the camera to what it is set to when the game starts
        targetFOV = Camera.main.orthographicSize;
    }
    private void Update()
    {
        //Throughout the game, the camera FOV is constantly changing to the TargetFOV
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetFOV, 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Speed Powerup"))
        {
            //Character movement increased,  
            gameObject.GetComponent<PlayerController>().Speed *= 2;
            //powerup object destroyed
            Destroy(other.gameObject);
            //TargetFOV changed so the update function will run
            targetFOV = 7f;
            //BlinkBlue function starts
            Blink(new Color(0, 0, 1, 0.2f));
            //IEnumerator starts
            StartCoroutine(RevertSpeed());
        }

        //if(other.CompareTag("Damage Powerup"))
        //{
        //    gameObject.GetComponent<playerAttack>().burstDamage *= 2;
        //    gameObject.GetComponent<playerAttack>().ShootDamage *= 2;
        //    Destroy(other.gameObject);
        //    targetFOV = 50f;
        //    Blink(new Color(1, 0, 0, 0.2f));
        //    StartCoroutine(RevertDamage());
        //}

    }

    //IEnumerator RevertDamage()
    //{
    //    //waits for 2 seconds and then reduces the character movement, and the targetFOV to the original
    //    yield return new WaitForSeconds(2f);
    //    gameObject.GetComponent<playerAttack>().burstDamage /= 2;
    //    gameObject.GetComponent<playerAttack>().ShootDamage /= 2;
    //    targetFOV = 60f;
    //}


    IEnumerator RevertSpeed()
    {
        //waits for 2 seconds and then reduces the character movement, and the targetFOV to the original
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<PlayerController>().Speed /= 2;
        targetFOV = 5f;
    }

    public void Blink(Color flashColor)
    {
        StartCoroutine(Blink(flashColor, 5, 3.5f));
    }

    public IEnumerator ColorLerpTo(Color color, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            damageImage.color = Color.Lerp(damageImage.color, color, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator Blink(Color blinkIn, int blinkCount, float totalBlinkDuration)
    {
        //Split up the full duration equally between all blinks
        float fractionalBlinkDuration = totalBlinkDuration / blinkCount;

        for (int blinked = 0; blinked < blinkCount; blinked++)
        {
            // Split up each blink in half so that the blink can happen fully and equally
            float halfFractionalDuration = fractionalBlinkDuration * 0.5f;

            // Lerp to the color
            yield return StartCoroutine(ColorLerpTo(blinkIn, halfFractionalDuration));

            // Lerp to transparent
            StartCoroutine(ColorLerpTo(Color.clear, halfFractionalDuration));
        }

    }
}
