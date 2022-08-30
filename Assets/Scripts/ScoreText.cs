using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public bool bestScore;

    public bool gameScore= false;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (!gameScore)
        {
            if (bestScore)
            {
                BestScore();
            }
            else
            {
                Score();
            }
        }
        if (gameScore)
        {
           text.text = "" + GameManager.Instance.score;
        }
    }

    private void BestScore()
    {
        text.text = "BESTSCORE\n" + GameManager.Instance.bestScore;
    }

    private void Score()
    {
        text.text = "SCORE\n" + GameManager.Instance.score;
    }
}
