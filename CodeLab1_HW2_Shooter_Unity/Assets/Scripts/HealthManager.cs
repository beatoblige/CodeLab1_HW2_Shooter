using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;  //a script is calling a version of itself and naming it instance
    
    public int currentHealth;
    public int maxHealth;

    public GameObject deathEffect;

    public float invicibleLength = 2f;

    private float invicibleCounter;
    public SpriteRenderer theSR;

    public int shieldPower;
    public int shieldMaxPower = 2;
    public GameObject theShield;
    private void Awake()    //called when object is activated
    {
        instance = this; // set to this version of the script 
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIManager.instance.healthBar.maxValue = maxHealth;
        UIManager.instance.healthBar.value = currentHealth;
        UIManager.instance.shieldBar.maxValue = shieldMaxPower;
        UIManager.instance.shieldBar.value = shieldPower;
        ActiviateShield();
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleCounter >= 0)
        {
            invicibleCounter -= Time.deltaTime;

            if (invicibleCounter >= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void HurtPlayer()
    {
        if (invicibleCounter < 0)
        {

            if (theShield.activeInHierarchy)
            {
                shieldPower--;

                if (shieldPower <= 0)
                {
                    theShield.SetActive(false);
                }
                
                UIManager.instance.shieldBar.value = shieldPower;
            }
            else
            {



                currentHealth--; //subtract 1

                UIManager.instance.healthBar.value = currentHealth;
                if (currentHealth <= 0)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                    gameObject.SetActive(false);

                    GameManager.instance.KillPlayer();

                    Wave.instance.canSpawnWaves = false;
                }

                PlayerController.instance.doubleShotActive = false;
            }
        }
    }
    
    public void Respawn()
    {
        gameObject.SetActive(true);
        currentHealth = maxHealth;
        UIManager.instance.healthBar.value = currentHealth;

        invicibleCounter = invicibleLength;
        theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
        
    }

    public void ActiviateShield()
    {
        theShield.SetActive(true);
        shieldPower = shieldMaxPower;
        
        UIManager.instance.shieldBar.value = shieldPower;
    } 
}
