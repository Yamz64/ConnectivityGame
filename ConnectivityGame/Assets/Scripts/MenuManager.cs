using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("MusicSlider") != null) GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music Volume");
        if (GameObject.FindGameObjectWithTag("SFXSlider") != null) GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("SFX Volume");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1 (Remake)");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AdjustMusic()
    {
        GameObject music_slider = GameObject.FindGameObjectWithTag("MusicSlider");
        PlayerPrefs.SetFloat("Music Volume", music_slider.GetComponent<Slider>().value);
    }

    public void AdjustSFX()
    {
        GameObject music_slider = GameObject.FindGameObjectWithTag("SFXSlider");
        PlayerPrefs.SetFloat("SFX Volume", music_slider.GetComponent<Slider>().value);
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
