using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class GameMenu : MonoBehaviour
{
    public enum Kind
    {
        GAMESTOP,
        RESUME,
        RESTART,
        SETTING,
        EXIT,
        GAMEOVER,
        SETTINGEXIT,
        GAMESETTINGEXIT
    }
    public Kind kind;

    public GameObject stopButton;
    public GameObject menuObj=null;
    public GameObject settingObj;
    public GameObject gameOver;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        switch (kind)
        {
            case Kind.GAMESTOP:
                button.onClick.AddListener(GameStopButton);
                break;
            case Kind.RESUME:
                button.onClick.AddListener(Resume);
                break;
            case Kind.RESTART:
                button.onClick.AddListener(Restart);
                break;
            case Kind.SETTING:
                button.onClick.AddListener(Setting);
                break;
            case Kind.EXIT:
                button.onClick.AddListener(Exit);
                break;
            case Kind.SETTINGEXIT:
                button.onClick.AddListener(SettingExit);
                break;
        }
    }
    //���� ������ ���� ���� ��ư
    public void GameStopButton()
    {
        GameManager.Instance.state = GameManager.State.STOP;
        Time.timeScale = 0;
        menuObj.SetActive(true);
    }

    //���� ���㿡�� ����ϱ� ��ư
    public void Resume()
    {
        GameManager.Instance.state = GameManager.State.PLAYING;
        Time.timeScale = 1;
        menuObj.SetActive(false);
        stopButton.SetActive(true);
    }

    //���� �ٽý���
    public void Restart()
    {
        GameManager.Instance.state = GameManager.State.PLAY;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    //���� ����
    public void Setting()
    {
        settingObj.SetActive(true);
    }

    //���� ȭ������ ������
    public void Exit()
    {
        GameManager.Instance.state = GameManager.State.Main;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    //���� ������ �� ���� ����
    public void SettingExit()
    {
        settingObj.SetActive(false);
        GameManager.Instance.Save(GameManager.Instance.data);
    }


    private void Update()
    {
        if (kind == Kind.GAMESTOP)
        {
            if (GameManager.Instance.state == GameManager.State.PLAYING||GameManager.Instance.state == GameManager.State.HIT)
            {
                gameObject.GetComponent<Button>().interactable = true;
            }

            else
            {
                gameObject.GetComponent<Button>().interactable = false;
            }
        }

        else if(kind == Kind.GAMEOVER&&GameManager.Instance.state==GameManager.State.BROKEN)
        {
            gameOver.SetActive(true);
        }

        else
        {
            return;
        }
    }
}
