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
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public Slider musicSlider;
    public Slider soundSlider;
    public AudioMixer masterMixer;

    public void ChangeMusicVolume()
    {
        masterMixer.SetFloat("MusicVolume", musicSlider.value);
    }

    public void ChangeSoundVolume()
    {
        masterMixer.SetFloat("SoundVolume", soundSlider.value);
    }
}
