using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour
{
    public string SceneName;

    public GameManager.State State;

    private void OnEnable()
    { 
        if (GameManager.Instance!=null&&GameManager.Instance.state == GameManager.State.SELECT)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void InreractableTrue()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void SceneChanageState()
    {
        SceneManager.LoadScene(SceneName);
        GameManager.Instance.state = State;
    }    
    public void SceneChanageNULL()
    {
        SceneManager.LoadScene(SceneName);
    }    

    public void SelectSceneChanage()
    {
        GameManager.Instance.Interval = GameManager.Instance.initInterval;
        GameManager.Instance.mouseLimit = GameManager.Instance.initMouseLimit;
        GameManager.Instance.state = GameManager.State.PLAY;
        SceneManager.LoadScene(SceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
