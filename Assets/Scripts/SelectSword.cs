using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSword : MonoBehaviour
{
    private int num;


    Button button;

    private SelectSword[] selectSwords;
    [HideInInspector]
    public Image image=null;

    private void Start()
    {
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            if (gameObject.transform == transform.parent.parent.GetChild(i).transform.GetChild(0))
            {
                num = i;
            }
        }

        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(Onselect);
        image.color = new Color(255,255,255,0.5f);
    }

    private void Onselect()
    {
        // 게임 플레이 버튼은 칼을 선택하기 전까지 비활성화 되어있다.
        FindObjectOfType<SceneMove>().InreractableTrue();
        GameManager.Instance.OnSelect(num);
        GameManager.Instance.state = GameManager.State.READY;
        selectSwords = FindObjectsOfType<SelectSword>();
        for (int i = 0; i < selectSwords.Length; i++)
        {
            selectSwords[i].image.color = new Color(1, 1, 1, 0.5f);
        }
        image.color = new Color(1, 1, 1, 1);
    }

}
