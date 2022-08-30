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
    //게임 씬에서 게임 멈춤 버튼
    public void GameStopButton()
    {
        GameManager.Instance.state = GameManager.State.STOP;
        Time.timeScale = 0;
        menuObj.SetActive(true);
    }

    //게임 멈춤에서 계속하기 버튼
    public void Resume()
    {
        GameManager.Instance.state = GameManager.State.PLAYING;
        Time.timeScale = 1;
        menuObj.SetActive(false);
        stopButton.SetActive(true);
    }

    //게임 다시시작
    public void Restart()
    {
        GameManager.Instance.state = GameManager.State.PLAY;
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    //게임 세팅
    public void Setting()
    {
        settingObj.SetActive(true);
    }

    //메인 화면으로 나가기
    public void Exit()
    {
        GameManager.Instance.state = GameManager.State.Main;
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    //세팅 나가기 및 볼륨 저장
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
