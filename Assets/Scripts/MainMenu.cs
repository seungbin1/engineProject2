using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //���� �޴��� ui��ư�� ���� �Լ���
    public void Play()
    {
        SceneManager.LoadScene("SelectScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
