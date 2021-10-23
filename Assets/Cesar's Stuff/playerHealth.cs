using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerHealth : MonoBehaviour
{
    //Declaring Variables for the player max and current health
    public int maxHealth;
    public int currentHealth;

    //Declaring variable for a slider
    private Slider playerSlider;

    public AudioClip playerDamaged;
    public AudioClip playerDeath;

    public AudioSource soundEffects;

    private void Awake()
    {
        //Once game starts the player will start at maxhealth
        currentHealth = maxHealth;
        //referencing the slider that was declared, to the players health slider UI component
        playerSlider = GameObject.Find("Player Health Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        //Checks if the player has no health left 
        if(currentHealth <= 0)
        {
            
            Destroy(playerSlider.gameObject);
            //the players model disappears in order to mimic the player being dead
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            //Disables the players movement script in order to stop the player being able to move while they're dead
            gameObject.GetComponent<PlayerController>().enabled = false;
            //If it is true then the IEnumerator LevelRestart starts running
            StartCoroutine(LevelRestart());
            //Disables the players attack script in order to stop the player being able to atack while they're dead
            gameObject.GetComponent<playerAttack>().enabled = false;
            
        }

        //COnstantly updates the Health UI so the player knows how much health they have
        playerSlider.value = currentHealth;

        //Checks how much health the player has and changes the colour of the HEalth UI depending on how low the health is
        if (playerSlider.value >= 51)
        {
            //Changes color to green
            GameObject.Find("Player Fill").GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        if (playerSlider.value <= 50)
        {
            //changes color to orange
            GameObject.Find("Player Fill").GetComponent<Image>().color = new Color(1.0f, 0.6f, 0.0f, 1.0f);
        }
        if (playerSlider.value <= 25)
        {
            //changes color to red
            GameObject.Find("Player Fill").GetComponent<Image>().color = new Color(1, 0, 0, 1);
        }
        
    }

    //This void is public so that it can be called from the enemy attack script by allowing that script to note
    //what amount of damage is being dealt
    public void TakeDamage(int amount)
    {
        //Removes health from the player based on how much damage is specified from different scripts
        currentHealth -= amount;

        if( currentHealth > 0)
        {
            soundEffects.clip = playerDamaged;
            soundEffects.Play();
        }
        else
        {
            soundEffects.clip = playerDeath;
            soundEffects.Play();
        }
        
        
    }

    //Co routine created instead of void as the IEnumerator can utilize WaitForSeconds
    IEnumerator LevelRestart()
    {
        //the script will wait to seconds before executing the rest of the function
        yield return new WaitForSeconds(2f);
        
        //The scene manager restarts the level as level 0 is the first level
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(25);
        }
    }
}
