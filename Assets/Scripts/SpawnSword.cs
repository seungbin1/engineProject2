using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSword : MonoBehaviour
{
    //����
    public GameObject[] prefabs;
    public GameObject Life;


    private void Awake()
    {
        GameManager.Instance.lifeObj = Life;
    }

    //Į�� ������ �����
    private void OnEnable()
    {
        Instantiate(GameManager.Instance.sword, GameObject.Find("Sword").transform);
        Instantiate(prefabs[GameManager.Instance.prefabNum], Life.transform).transform.localScale= new Vector3(75,75,75);
        Instantiate(prefabs[GameManager.Instance.prefabNum], Life.transform).transform.localScale = new Vector3(75, 75, 75);
        Instantiate(prefabs[GameManager.Instance.prefabNum], Life.transform).transform.localScale = new Vector3(75, 75, 75);
    }
}
