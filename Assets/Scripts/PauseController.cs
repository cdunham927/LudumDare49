using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseController : MonoBehaviour
{
    public bool paused = false;
    public GameObject pauseObj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0f;
            pauseObj.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseObj.SetActive(false);
        }


        if (Application.isEditor)
        {
            //ResetLevel();
        }
    }

    public void ResetLevel()
    {
        MonsterController monster = FindObjectOfType<MonsterController>();
        monster.RestoreStats();
        monster.SaveStats();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public Slider musicSlider;
    public Slider soundSlider;
    public AudioMixer masterMixer;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            masterMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
            masterMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("SoundVolume"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            soundSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }

    public void ChangeMusicVolume()
    {
        masterMixer.SetFloat("MusicVolume", musicSlider.value);
        //PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void ChangeSoundVolume()
    {
        masterMixer.SetFloat("SoundVolume", soundSlider.value);
        //PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
    }

}
