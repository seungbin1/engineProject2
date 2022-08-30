using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneSword : MonoBehaviour
{
    private GameObject sword;
    public void OnEnable()
    {
        if (GameManager.Instance.sword != null)
        {
            SpawnSword(GameManager.Instance.sword);
        }

        else
        {
            SpawnSword(GameManager.Instance.prefabs[0]);
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1000) * Time.deltaTime);
    }

    private void SpawnSword(GameObject gameObject)
    {
        sword = Instantiate(gameObject, transform);
        sword.transform.localPosition = Vector3.zero;
    }
}
