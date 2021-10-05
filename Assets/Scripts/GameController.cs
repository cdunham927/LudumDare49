using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Timer things")]
    public float elecTimerLower;
    public float elecTimerHigher;
    float curTimerMax;
    float curElecTimer;
    public bool powerOn = true;
    public Image elecImage;
    float lerpSpd = 10f;
    public GameObject fakeShadows;
    MonsterController monster;
    AudioSource src;
    public AudioClip otherClip;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        monster = FindObjectOfType<MonsterController>();
        curTimerMax = Random.Range(elecTimerLower, elecTimerHigher);
        curElecTimer = curTimerMax;
    }

    private void Update()
    {
        if (curElecTimer > 0) curElecTimer -= Time.deltaTime;
        elecImage.fillAmount = Mathf.Lerp(elecImage.fillAmount, curElecTimer / curTimerMax, Time.deltaTime * lerpSpd);
        if (curElecTimer <= 0) PowerOutage();

        fakeShadows.SetActive(!powerOn);
    }

    public void PowerOutage()
    {
        src.PlayOneShot(otherClip, 0.4f);
        monster.PowerOutage();
        curElecTimer = 0;
        powerOn = false;
    }

    public void RestorePower()
    {
        src.PlayOneShot(src.clip, 0.4f);
        monster.FixElectricity();
        powerOn = true;
        curTimerMax = Random.Range(elecTimerLower, elecTimerHigher);
        curElecTimer = curTimerMax;
    }
}
