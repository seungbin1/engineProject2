using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLimit : MonoBehaviour
{
    //최고 점수에 따라 선택할 수 있는 칼의 제한에 대한 내용
    private void OnEnable()
    {
        int limit = 0;
        GameObject gameObject;
        Text text;
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject = transform.GetChild(i).gameObject;

            limit = limit + 500;

            if (i != 0)
            {
                if (i % 10 == 9)
                {
                    limit = limit + 5000;
                }
                text = gameObject.transform.GetChild(1).GetComponentInChildren<Text>();
                text.text = "" + limit;
            }

            if (i != 0 && limit <= GameManager.Instance.bestScore)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }

        }
    }
}
