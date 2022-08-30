using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    public enum Sound
    {
        MAIN,
        GAME,
        SWORD,
        EFFECT
    }

    public Sound sound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSetting();

        if (sound == Sound.MAIN || sound == Sound.GAME)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sound == Sound.EFFECT&&collision.tag=="Sword")
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (sound == Sound.SWORD)
        {
            if(GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state ==GameManager.State.HIT)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            else
            {
                audioSource.Pause();
            }
        }

        else if (sound == Sound.GAME)
        {
            if (GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state == GameManager.State.HIT)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            else
            {
                audioSource.Pause();
            }
        }

        else
        {
            return;
        }
    }

    private void Main()
    {
        audioSource.playOnAwake = true;
        audioSource.loop = true;
    }

    private void Game()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    private void Sword()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.pitch = 0.5f;
    }

    private void Effect()
    {
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void AudioSetting()
    {
        audioSource = GetComponent<AudioSource>();

        if (sound == Sound.MAIN)
        {
            Main();
        }        

        else if(sound == Sound.GAME)
        {
            Game();
        }        
        
        else if (sound == Sound.EFFECT)
        {
            Effect();
        }

        else
        {
            Sword();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sound == Sound.MAIN && (GameManager.Instance.state == GameManager.State.READY || GameManager.Instance.state == GameManager.State.SELECT || GameManager.Instance.state == GameManager.State.Main))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        else
        {
            audioSource.Stop();
        }
    }
}