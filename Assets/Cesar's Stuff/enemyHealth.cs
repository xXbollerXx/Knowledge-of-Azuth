    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour
{
    public int MaxHealth;
    public int currentHealth;
    public GameObject EnemyHealthUI;
    public Slider enemySlider;

    //Enemy Face Swap    
    private Texture EnemyFaceCam;
    private RenderTexture NewEnemyRenderer;

    public AudioSource soundEffects;
    public AudioClip death;
    public AudioClip damaged;
    void Awake()
    {
        currentHealth = MaxHealth;
        
        //Enemy Face Swap
        NewEnemyRenderer = new RenderTexture (256, 256, 24, RenderTextureFormat.Default);
        EnemyFaceCam = GetComponentInChildren<Camera>().targetTexture = NewEnemyRenderer;
    }

    
    void Update()
    {
        if (currentHealth <= 0)
        {
            GetComponent<EnemyMove>().enabled = false;
            Destroy(gameObject, 0.653f);
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        


        EnemyHealthUI.SetActive(true);
        enemySlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            soundEffects.clip = death;
            soundEffects.Play();
        }

        //Changing UI so slider doesn't stick around after enemy dies
        if (enemySlider.value == 0)
        {
            enemySlider.gameObject.SetActive(false);
        }
        else if (enemySlider.value > 0)
        {
            enemySlider.gameObject.SetActive(true);
        }

        //Changing Slider colorur based on health value
        if (enemySlider.value >= 51)
        {
            GameObject.Find("Enemy Fill").GetComponent<Image>().color = new Color(0, 1, 0, 1);
        }
        if (enemySlider.value <= 50)
        {
            GameObject.Find("Enemy Fill").GetComponent<Image>().color = new Color(1.0f, 0.6f, 0.0f, 1.0f);
        }
        if (enemySlider.value <= 25)
        {
            GameObject.Find("Enemy Fill").GetComponent<Image>().color = new Color(1, 0, 0, 1);
        }
        


        GameObject.Find("Enemy RawImage").GetComponent<RawImage>().texture = EnemyFaceCam;

        if (currentHealth > 0)
        {
            soundEffects.clip = damaged;
            soundEffects.Play();
        }
        
    }
}
