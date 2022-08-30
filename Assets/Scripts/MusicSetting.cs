using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    public AudioMixer Mixer;

    public Slider main;
    public Slider game;
    public Slider effect;

    private float sound;
    private float sound2;
    private float sound3;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke("Init", 0.5f);
    }
    //���� ������ �����Ѵ�.
    public void MainMusic()
    {
        sound = main.value;
        Mixer.SetFloat("MainMusic", Mathf.Log10(sound) * 20);
        GameManager.Instance.data.SaveSound(sound, sound2, sound3);
    }
    //���� ������ �����Ѵ�.
    public void GameMusic()
    {
        sound2 = game.value;
        Mixer.SetFloat("GameMusic", Mathf.Log10(sound2) * 20);
        GameManager.Instance.data.SaveSound(sound, sound2, sound3);
    }
    
    //���� ������ �����Ѵ�.
    public void EffectMusic()
    {
        sound3 = effect.value; 
        Mixer.SetFloat("EffectMusic", Mathf.Log10(sound3) * 20);
        GameManager.Instance.data.SaveSound(sound, sound2, sound3);
    }
    //������ ���� ������ ��ư�� �ִ�.

    private void Init()
    {
        sound = float.Parse(GameManager.Instance.data.mainSound.ToString());
        sound2 = float.Parse(GameManager.Instance.data.gameSound.ToString());
        sound3 = float.Parse(GameManager.Instance.data.effectSound.ToString());

        Mixer.SetFloat("MainMusic", Mathf.Log10(sound) * 20);
        Mixer.SetFloat("GameMusic", Mathf.Log10(sound2) * 20);
        Mixer.SetFloat("EffectMusic", Mathf.Log10(sound3) * 20);

        main.value = sound;
        game.value = sound2;
        effect.value = sound3;
    }
}
