using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //메인 메뉴의 ui버튼에 관한 함수들
    public void Play()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
