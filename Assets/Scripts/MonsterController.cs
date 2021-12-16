using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System;

public class MonsterController : MonoBehaviour
{
    public string[] randomPetNames;
    public string petName;
    [Space]
    [Header("Important variables")]
    public float maxHunger = 100f;
    public float maxThirst = 100f;
    public float maxCleanliness = 100f;
    public float maxFun = 100f;
    public float maxRadiation = 100f;
    [Space]
    [Header("Default rate at which those variables drop")]
    public float defHungerRate = 2f;
    public float defThirstRate = 2f;
    public float defCleanlinessRate = 2f;
    public float defFunRate = 2f;
    public float defRadiationRate = 2f;
    [Space]
    [Header("Increased rate at which those variables drop")]
    public float incHungerRate = 3.5f;
    public float incThirstRate = 3.5f;
    public float incCleanlinessRate = 3.5f;
    public float incFunRate = 3.5f;
    public float incRadiationRate = 3.5f;
    [Space]
    [Header("UI")]
    public Image hungerImage;
    public Image thirstImage;
    public Image cleanlinessImage;
    public Image funImage;
    public Image radiationImage;
    float lerpSpd = 10f;
    public Text statusText;
    public string monsterStatus;
    public float showStatThreshold = 50f;

    //Current values for the stats
    float hunger;
    float thirst;
    float cleanliness;
    float fun;
    float radiation;
    float curHungerRate = 2f;
    float curThirstRate = 2f;
    float curCleanlinessRate = 2f;
    float curFunRate = 2f;
    float curRadiationRate = 2f;

    public GameObject curItem;
    public enum priorities { food, water, fun };
    public priorities curPriority;
    public Queue<Action> actions = new Queue<Action>();
    public Action curAction = null;
    public MonsterRadius rads;

    //Time to take a shit
    [Space]
    [Header("Shit")]
    public float timeToShit;
    float curShit;
    public GameObject shit;
    bool noShitInQueue = true;
    public GameObject shitSpawn;
    public GameObject[] shitPositions;

    //Wants food
    [Space]
    [Header("Food list")]
    public List<Food> foods = new List<Food>();
    public string wantedFood;
    public float timeToWantFoodLow;
    public float timeToWantFoodHigh;
    float curWantFoodTime;
    public float timeTillFoodWantExpires;
    float curTimeTillFoodWantExpires;
    bool foodDepression = false;
    public GameObject speechBubble;
    public SpriteRenderer wantSprite;

    [Space]
    [Header("Movement")]
    public float spd;
    Rigidbody2D bod;
    public float interactDistance;

    [Space]
    [Header("Water bowl things")]
    public WaterBowlController waterBowl;
    bool drinkInQueue = false;

    [Space]
    [Header("Stat reduction")]
    public float redAmt = 10f;

    [Space]
    [Header("D E A T H")]
    public bool DEAD = false;

    Animator anim;
    SpriteRenderer rend;
    [Space]
    [Header("Animation")]
    public float moveAnimThreshold = 0.1f;

    [Space]
    [Header("Sound Effects")]
    public AudioClip hungryClip;
    public AudioClip eatClip;
    public AudioClip thirstyClip;
    public AudioClip drinkClip;
    public AudioClip boredClip;
    public AudioClip playingClip;
    public AudioClip dirtyClip;
    public AudioClip cleanClip;
    public AudioClip lowRadClip;
    public AudioClip refillRadClip;
    public AudioClip[] shitClip;
    public AudioClip dieClip1;
    public AudioClip dieClip2;
    public AnimationClip deathAnim;
    public bool notPlayedDeath = true;
    AudioSource src;

    System.DateTime currentDate;

    //Hats hats hats
    //
    //
    //
    [Header("Hats")]
    public List<GameObject> hats = new List<GameObject>();
    //Experience
    [Header("Experience/Leveling Up")]
    public int level = 1;
    public float experience;
    public float expToNext;
    public AnimationCurve expCurve = new AnimationCurve();
    bool statsAdded = false;
    public int hatTokens;

    private void OnApplicationQuit()
    {
        SaveStats();
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetFloat("thirst", thirst);
        PlayerPrefs.SetFloat("cleanliness", cleanliness);
        PlayerPrefs.SetFloat("fun", fun);
        PlayerPrefs.SetFloat("radiation", radiation);
        PlayerPrefs.SetInt("dead", (hunger <= 0 || thirst <= 0 || cleanliness <= 0 || fun <= 0 || radiation <= 0) ? 0 : 1);
        currentDate = System.DateTime.Today;
        PlayerPrefs.SetString("Date", currentDate.ToString());
        currentDate.CompareTo(System.DateTime.Today);
    }

    private void Awake()
    {
        rads = FindObjectOfType<MonsterRadius>();
        bod = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();

        //InvokeRepeating("ShowAction", 0.01f, 7.5f);
        InvokeRepeating("CheckStatus", 0.01f, 7.5f);


        expToNext = CalculateExp(level);

        for (int i = 1; i < 30; i++)
        {
            expCurve.AddKey(i, CalculateExp(i));
        }
    }

    public float CalculateExp(int lvl)
    {
        float expNeeded = 0;

        //expNeeded = lvl * 100f;
        expNeeded = Mathf.RoundToInt(Mathf.Pow(8 * (lvl + 1), 1.6f));

        return expNeeded;
    }

    public void AddExp(float amt)
    {
        experience += amt;

        if (experience >= expToNext)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        experience -= expToNext;

        if (!statsAdded)
        {
            hatTokens += 1;
            statsAdded = true;
        }

        Invoke("ResetAdded", 0.2f);
        expToNext = CalculateExp(level);
    }

    void ResetAdded()
    {
        statsAdded = false;
    }

    private void OnEnable()
    {
        RegularStatRate();

        //RestoreStats();
        LoadStats();
    }

    public void LoadStats()
    {
        hunger = PlayerPrefs.GetFloat("hunger", maxHunger);
        thirst = PlayerPrefs.GetFloat("thirst", maxThirst);
        cleanliness = PlayerPrefs.GetFloat("cleanliness", maxCleanliness);
        fun = PlayerPrefs.GetFloat("fun", maxFun);
        radiation = PlayerPrefs.GetFloat("radiation", maxRadiation);
        DEAD = (PlayerPrefs.GetInt("dead", 0) == 0);
    }

    public void RestoreStats()
    {
        petName = randomPetNames[Random.Range(0, randomPetNames.Length - 1)];
        notPlayedDeath = true;

        curWantFoodTime = Random.Range(timeToWantFoodLow, timeToWantFoodHigh);
        curShit = timeToShit;
        noShitInQueue = true;

        RestoreHunger();
        RestoreThirst();
        RestoreCleanliness();
        RestoreFun();
        RestoreRadiation();
        DEAD = false;
    }

    public void RegularStatRate()
    {
        curHungerRate = defHungerRate;
        curThirstRate = defThirstRate;
        curCleanlinessRate = defCleanlinessRate;
        curFunRate = defFunRate;
        curRadiationRate = defRadiationRate;
    }

    public void FasterStatRate()
    {
        curHungerRate = incHungerRate;
        curThirstRate = incThirstRate;
        curCleanlinessRate = incCleanlinessRate;
        curFunRate = incFunRate;
        curRadiationRate = incRadiationRate;
    }

    private void Update()
    {
        hunger -= Time.deltaTime * curHungerRate;
        thirst -= Time.deltaTime * curThirstRate;
        cleanliness -= Time.deltaTime * curCleanlinessRate;
        fun -= Time.deltaTime * curFunRate;
        radiation -= Time.deltaTime * curRadiationRate;

        hungerImage.fillAmount = Mathf.Lerp(hungerImage.fillAmount, hunger / maxHunger, Time.deltaTime * lerpSpd);
        thirstImage.fillAmount = Mathf.Lerp(thirstImage.fillAmount, thirst / maxThirst, Time.deltaTime * lerpSpd);
        cleanlinessImage.fillAmount = Mathf.Lerp(cleanlinessImage.fillAmount, cleanliness / maxCleanliness, Time.deltaTime * lerpSpd);
        funImage.fillAmount = Mathf.Lerp(funImage.fillAmount, fun / maxFun, Time.deltaTime * lerpSpd);
        radiationImage.fillAmount = Mathf.Lerp(radiationImage.fillAmount, radiation / maxRadiation, Time.deltaTime * lerpSpd);

        if (curWantFoodTime > 0) curWantFoodTime -= Time.deltaTime;

        if (curWantFoodTime <= 0 && wantedFood == "NA")
        {
            //Debug.Log("Getting new wanted food");
            curTimeTillFoodWantExpires = timeTillFoodWantExpires;
            foodDepression = true;
            wantedFood = GetRandomFood();
        }

        if (curShit > 0) curShit -= Time.deltaTime;

        if (curShit <= 0 && noShitInQueue)
        {
            actions.Enqueue(new Shit(6f));
            noShitInQueue = false;
        }

        //Add action to queue based on item in range of monster
        if (curItem != null)
        {
            if (curItem.GetComponent<Food>() != null && curItem.GetComponent<Item>().added == false)
            {
                curItem.GetComponent<Item>().added = true;
                actions.Enqueue(new Eat(curItem.GetComponent<Food>(), 2.5f, interactDistance, curItem));
            }
            if (curItem.GetComponent<Toy>() != null && curItem.GetComponent<Item>().added == false)
            {
                curItem.GetComponent<Item>().added = true;
                actions.Enqueue(new Play(curItem.GetComponent<Toy>(), 5f, interactDistance, curItem));
            }
        }

        //If monster thirst, drank
        if (thirst <= maxThirst - showStatThreshold && waterBowl.filled && !drinkInQueue)
        {
            actions.Enqueue(new Drink(5f, interactDistance, waterBowl.gameObject));
            drinkInQueue = true;
        }

        if (curAction == null)
        {
            if (actions.Count > 0)
            {
                curAction = actions.Dequeue();
                curAction.Start();
            }
        }
        else
        {
            curAction.Update();
            if (curAction.isFinished)
            {
                curAction = null;
                //actions.Dequeue();
            }
        }

        if (curTimeTillFoodWantExpires > 0 && wantedFood != "NA")
        {
            curTimeTillFoodWantExpires -= Time.deltaTime;
        }

        if (curTimeTillFoodWantExpires <= 0)
        {
            wantedFood = "NA";
            speechBubble.SetActive(false);
            if (foodDepression == true)
            {
                foodDepression = false;
                fun -= 10;
            }
            curTimeTillFoodWantExpires = timeTillFoodWantExpires;
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                hunger = maxHunger / 2f;
                thirst = maxThirst / 2f;
                cleanliness = maxCleanliness / 2f;
                fun = maxFun / 2f;
                radiation = maxRadiation / 2f;
            }

            if (Input.GetKeyDown(KeyCode.H)) hunger -= 50;
            if (Input.GetKeyDown(KeyCode.T)) thirst -= 50;
            if (Input.GetKeyDown(KeyCode.C)) cleanliness -= 50;
            if (Input.GetKeyDown(KeyCode.F)) fun -= 50;
            if (Input.GetKeyDown(KeyCode.R)) radiation -= 50;

            if (Input.GetKeyDown(KeyCode.S)) curShit -= 30;
            if (Input.GetKeyDown(KeyCode.E)) FindObjectOfType<GameController>().PowerOutage();
            if (Input.GetKeyDown(KeyCode.O)) FindObjectOfType<GameController>().RestorePower();
            if (Input.GetKeyDown(KeyCode.N)) hunger -= 999;

            //PlayerPrefs
            if (Input.GetKeyDown(KeyCode.Period)) SaveStats();
            if (Input.GetKeyDown(KeyCode.Comma)) RestoreStats();
        }

        //Debug.Log(bod.velocity.magnitude);
        //Debug.Log(bod.velocity.sqrMagnitude);
        anim.SetBool("moving", Mathf.Abs(bod.velocity.sqrMagnitude) > moveAnimThreshold);
        //anim.SetBool("moving", bod.velocity.magnitude != 0);

        //DEATH
        if (hunger <= 0 || thirst <= 0 || cleanliness <= 0 || fun <= 0 || radiation <= 0)
        {
            DEAD = true;
        }

        if (DEAD && notPlayedDeath)
        {
            notPlayedDeath = false;
            src.PlayOneShot(dieClip1, 0.9f);
            src.PlayOneShot(dieClip2, 0.2f);
            Invoke("Pause", deathAnim.length + 1f);
            anim.SetTrigger("dead");
        }
    }

    public void Pause()
    {
        FindObjectOfType<PauseController>().paused = true;
    }

    public void CheckStatus()
    {
        if (hunger < maxHunger - showStatThreshold || thirst < maxThirst - showStatThreshold || cleanliness < maxCleanliness - showStatThreshold || fun < maxFun - showStatThreshold || radiation < maxRadiation - showStatThreshold)
        {
            if (hunger < thirst && hunger < cleanliness && hunger < fun && hunger < radiation)
            {
                src.PlayOneShot(hungryClip);
                monsterStatus = "Hungry";
            }
            else if (thirst < cleanliness && thirst < fun && thirst < radiation)
            {
                src.PlayOneShot(thirstyClip);
                monsterStatus = "Thirsty";
            }
            else if (fun < cleanliness && fun < radiation)
            {
                src.PlayOneShot(boredClip, 0.55f);
                monsterStatus = "Irritated";
            }
            else if (cleanliness < radiation)
            {
                src.PlayOneShot(dirtyClip);
                monsterStatus = "Dirty";
            }
            else
            {
                src.PlayOneShot(lowRadClip, 0.2f);
                monsterStatus = "Rad Deficiency";
            }

            anim.SetBool("sad", true);
        }
        else
        {
            monsterStatus = "OK";
            anim.SetBool("sad", false);
        }

        statusText.text = petName + " Status: " + monsterStatus;
    }

    public void ShowAction()
    {
        Debug.Log("Current Action: " + curAction);
    }

    public string GetRandomFood()
    {
        Food f = foods[Random.Range(0, foods.Count)];
        speechBubble.SetActive(true);
        wantSprite.sprite = f.GetComponent<SpriteRenderer>().sprite;
        return f.foodName;
    }

    public void GotToy(Toy t)
    {
        anim.SetTrigger("happy");
        src.PlayOneShot(playingClip, 0.55f);
        hunger -= redAmt;
        thirst -= redAmt;
        cleanliness -= redAmt;
        if (fun + t.refillAmt < maxFun) fun += t.refillAmt;
        else fun = maxFun;
        radiation -= redAmt;
        FindObjectOfType<SpawnOnlyOneItem>().alreadySpawned = false;
        curItem.SetActive(false);
        curItem = null;
        CheckStatus();
    }

    public void FixElectricity()
    {
        //Set stats to regular reduction amt
        RegularStatRate();
        CheckStatus();
    }

    public void PowerOutage()
    {
        //Set stats to increased reduction amt
        FasterStatRate();
        CheckStatus();
    }

    public void AddUranium(float incAmt = 75f)
    {
        src.PlayOneShot(refillRadClip, 0.2f);
        if (radiation + incAmt < maxRadiation) radiation += incAmt;
        else radiation = maxRadiation;
        anim.SetTrigger("happy");
        CheckStatus();
    }

    public void Shit(float decAmt = 50f)
    {
        src.PlayOneShot(shitClip[Random.Range(0, shitClip.Length)]);
        hunger -= redAmt;
        thirst -= redAmt;
        cleanliness -= decAmt;
        fun -= redAmt;
        noShitInQueue = true;
        Instantiate(shit, shitSpawn.transform.position, Quaternion.identity);
        curShit = timeToShit;
        anim.SetTrigger("happy");
        CheckStatus();
    }

    public void CleanShit(float incAmt = 50f)
    {
        cleanliness += incAmt;
        CheckStatus();
    }

    public void PowerShower(float incAmt = 75f, float decAmt = 35f)
    {
        src.PlayOneShot(cleanClip);
        if (cleanliness + incAmt < maxCleanliness) cleanliness += incAmt;
        else cleanliness = maxCleanliness;
        fun -= redAmt;
        radiation -= decAmt;
        //Check for food in monster room to make it go bad
        rads.SpoilAllFoodInRoom();
        anim.SetTrigger("happy");
        CheckStatus();
    }

    public void Drink(float incAmt = 100f)
    {
        anim.SetTrigger("eating");
        src.PlayOneShot(drinkClip);
        if (thirst + incAmt < maxThirst) thirst += incAmt;
        else thirst = maxThirst;
        radiation -= redAmt;
        waterBowl.filled = false;
        drinkInQueue = false;
        CheckStatus();
    }

    public void Eat(Food fd, float incAmt = 30f)
    {
        src.PlayOneShot(eatClip);
        anim.SetTrigger("eating");
        float amtMod = 0f;
        if (fd.spoiled)
        {
            amtMod -= 10f;
            cleanliness -= redAmt;
        }
        if (fd.foodName == wantedFood)
        {
            //Debug.Log("We got food we wanted");
            fun += incAmt;
            wantedFood = "NA";
            speechBubble.SetActive(false);
            curWantFoodTime = Random.Range(timeToWantFoodLow, timeToWantFoodHigh);
        }
        hunger += incAmt;
        //If you give it the food it wants, restore more hunger
        hunger += amtMod;
        thirst -= redAmt;
        cleanliness -= redAmt;
        //fun -= incAmt;
        radiation -= redAmt;
        if (hunger > maxHunger) hunger = maxHunger;
        curShit -= fd.shitMod;
        curItem.SetActive(false);
        curItem = null;
        CheckStatus();
    }

    public void Move(int dir)
    {
        if (dir == -1)
        {
            bod.AddForce(Vector2.right * -1f * spd * Time.deltaTime);
            anim.SetBool("moving", true);
            //anim.SetInteger("dir", -1);
            shitSpawn.transform.position = shitPositions[0].transform.position;
            rend.flipX = false;
        }
        else
        {
            anim.SetBool("moving", true);
            //anim.SetInteger("dir", 1);
            bod.AddForce(Vector2.right * spd * Time.deltaTime);
            shitSpawn.transform.position = shitPositions[1].transform.position;
            rend.flipX = true;
        }
    }

    public void RestoreHunger()
    {
        hunger = maxHunger;
    }

    public void RestoreThirst()
    {
        thirst = maxThirst;
    }

    public void RestoreCleanliness()
    {
        cleanliness = maxCleanliness;
    }

    public void RestoreFun()
    {
        fun = maxFun;
    }

    public void RestoreRadiation()
    {
        radiation = maxRadiation;
    }

    public void GetHat()
    {

    }
}
