using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Canvas pauseMenu;
    public Canvas HUD;
    public Canvas MainMenu;
    public Canvas Options;
    public Canvas GameOver;

    public PlayerController Player;

    public Text Score;
    public Text TimeRemaining;
    public Slider Boost;

    public Camera thirdpCamera;
    public Camera firstpCamera;

    AudioSource audios;
    public AudioClip playing;
    public AudioClip SuperPlaying;
    public AudioClip stopped;

    private void Awake()
    {
        audios = GetComponent<AudioSource>();
        AudioListener.volume = PlayerPrefs.GetInt("Audio");
        Debug.Log(PlayerPrefs.GetInt("Audio"));
        audios.clip = stopped;
        audios.Play();
    }
    private void Update()
    {

        if (Player)
        {
            if (Input.GetKeyDown(KeyCode.C))
                Camera();
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();

            Score.text = "Score: " + (int)Player.score;
            TimeRemaining.text = "Time: " + (int)(Player.gameTimer > 0 ? Player.gameTimer : 0);
            Boost.value = Player.boostMeter;
            if (Player.gameOver)
            {
                Player.gameOver = false;
                GameOver.enabled = true;
                audios.clip = stopped;
                audios.Play();
                Player.playing = false;
            }
            if (Player.InvinMusic)
            {
                Player.InvinMusic = false;
                audios.clip = SuperPlaying;
                audios.Play();
            }
            if (Player.playing && !Player.Invincible())
            {
                if (audios.clip != playing)
                {
                    audios.clip = playing;
                    audios.Play();
                }
            }
        }
    }

    public void Menu()
    {
        MainMenu.enabled = !MainMenu.enabled;
        Options.enabled = !Options.enabled;
    }

    public void Mute()
    {
        PlayerPrefs.SetInt("Audio", PlayerPrefs.GetInt("Audio") == 0 ? 1 : 0);
        PlayerPrefs.Save();
        AudioListener.volume = PlayerPrefs.GetInt("Audio");
    }

    public void Pause()
    {
        pauseMenu.enabled = !pauseMenu.enabled;
        HUD.enabled = !HUD.enabled;
        audios.clip = (!Player.playing) ? playing : stopped;
        audios.Play();
        Player.playing = Player.playing ? false : true;
    }

    public void Camera()
    {
        thirdpCamera.enabled = !thirdpCamera.enabled;
        firstpCamera.enabled = !firstpCamera.enabled;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void MMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
