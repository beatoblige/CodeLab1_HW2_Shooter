using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public string bossName;
    public int currentHealth = 100;

    //public BattleShot[] shotsToFire;

    public BattleSequence[] sequences;
    public int currentSequence;
    public Animator bossAnimation; //a way to
                                  
    public GameObject bossExplosion;
    public bool battleDone;
    public float timeForExplosionEnd;
    public float waitTimeEndLevel;

    public Transform theBoss;

    public int bossScore = 50000;
                                   
     
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.bossName.text = bossName;
        UIManager.instance.bossHealthSlider.maxValue = currentHealth;
        UIManager.instance.bossHealthSlider.value = currentHealth;
        UIManager.instance.bossHealthSlider.gameObject.SetActive(true);
        
        
        MusicController.instance.PlayBoss();
    }

    // Update is called once per frame
    void Update()
    {
        /* for(int i = 0; i < shotsToFire.Length; i++) // i starts at 0, checks shots2fire is > its length, then keeps +1{
        {
            shotsToFire[i].shotCounter -= Time.deltaTime; //using i allows us to  go from 0-2 elements quickly
            if (shotsToFire[i].shotCounter <= 0) //i begins at 0
            {
                shotsToFire[i].shotCounter = shotsToFire[i].timeBetweenShots;
                Instantiate(shotsToFire[i].theShot, shotsToFire[i].fireTip.position, shotsToFire[i].fireTip.rotation);
            }
        } */

        if (!battleDone)
        {



            if (currentHealth <= sequences[currentSequence].healthToEndSequence
            ) //if current health goes under 50, add 1
            {


                sequences[currentSequence].removeAtSequenceEnd.SetActive(false);
                Instantiate(sequences[currentSequence].addAtSequenceEnd,
                    sequences[currentSequence].newSpawnPoint.position,
                    sequences[currentSequence].newSpawnPoint.rotation);


                currentSequence++;

                bossAnimation.SetInteger("Phase", currentSequence + 1);

            }
            else
            {
                for (int i = 0;
                    i < sequences[currentSequence].phaserShots.Length;
                    i++) // i starts at 0, checks shots2fire is > its length, then keeps +1{
                {
                    sequences[currentSequence].phaserShots[i].shotCounter -=
                        Time.deltaTime; //using i allows us to  go from 0-2 elements quickly
                    if (sequences[currentSequence].phaserShots[i].shotCounter <= 0) //i begins at 0
                    {
                        sequences[currentSequence].phaserShots[i].shotCounter =
                            sequences[currentSequence].phaserShots[i].timeBetweenShots;
                        Instantiate(sequences[currentSequence].phaserShots[i].theShot,
                            sequences[currentSequence].phaserShots[i].fireTip.position,
                            sequences[currentSequence].phaserShots[i].fireTip.rotation);
                    }
                }
            }

        }
    }
 

    public void HurtBoss()
    {
        currentHealth--;
        UIManager.instance.bossHealthSlider.value = currentHealth;
        
        if (currentHealth <= 0 && !battleDone)
        {
            /*Destroy(gameObject);
            UIManager.instance.bossHealthSlider.gameObject.SetActive(false); //will diminish boss's health*/
            battleDone = true;
            StartCoroutine(EndBattleCo());

        }
    }

    public IEnumerator EndBattleCo()
    {
        UIManager.instance.bossHealthSlider.gameObject.SetActive(false); //Boss Health gone after either death
        Instantiate(bossExplosion, theBoss.position, theBoss.rotation);
        bossAnimation.enabled = false; //animator will stop 
        
        GameManager.instance.AddScore(bossScore);
        
        yield return new WaitForSeconds(timeForExplosionEnd);
        
        theBoss.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(waitTimeEndLevel);

        StartCoroutine(GameManager.instance.EndLevelCo());
    }
}

[System.Serializable]
public class BattleShot
{
    public GameObject theShot;
    public float timeBetweenShots;
    public float shotCounter;
    public Transform fireTip;
}

[System.Serializable]
public class BattleSequence
{
    public BattleShot[] phaserShots;
    public int healthToEndSequence;  //move on to the next phase of animation in boss 
    public GameObject addAtSequenceEnd;
    public GameObject removeAtSequenceEnd;
    public Transform newSpawnPoint; 
}