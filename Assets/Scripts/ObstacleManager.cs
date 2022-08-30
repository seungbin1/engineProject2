using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager instance;
    public static ObstacleManager Instance { get { return instance; } }

    public GameObject[] prefabs;
    [HideInInspector]
    public GameObject ObstacleParent;

   
    private float callTime=3;
    public float CallTime
    {
        get
        {
            return callTime;
        }
        set
        {
            if (value < 1)
            {
                callTime = 1;
            }
            else
            {
                callTime = value;
            }
        }
    }
    private float callTimeSpeed=3f;
    private int obstacleNum;

    private float minPosition=-30;
    private float speed = 5;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if (value > 15)
            {
                speed = 15;
            }
            else
            {
                speed = value;
            }
        }
    }
    //시간에 따라 빨라지는 장애물 이동속도
    private float moreSpeed = 0.01f;

    //장애물에 쓰여있는 점수
    public int score;

    //위아래로 움직이는 장애물 관련
    [HideInInspector]
    public int count = 500;
    [HideInInspector]
    public int randomNum = 10;
    [HideInInspector]
    public float moveSpeed = 1f;

    private void Awake()
    {
        StartCoroutine(CallObstacle());
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameManager.State.PLAYING||GameManager.Instance.state == GameManager.State.HIT)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).position.x < minPosition)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            Speed = Speed + moreSpeed * Time.deltaTime;
        }

        else if(GameManager.Instance.state != GameManager.State.STOP)
        {
            Init();
        }
    }

    //장애물에 관한 풀링
    private void RandomObstacle()
    {
        obstacleNum = Random.Range(0, 9);
        Check();
    }
    private void Check()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag== prefabs[obstacleNum].gameObject.tag&& transform.GetChild(i).gameObject.activeSelf == false)
            {
                transform.GetChild(i).localPosition = new Vector3(0, 0, 0);
                transform.GetChild(i).gameObject.SetActive(true);
                return;
            }
        }

        CreateObstacle();
    }

    private void CreateObstacle()
    {
        Instantiate(prefabs[obstacleNum],gameObject.transform);
    }

    IEnumerator CallObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(CallTime);
            if (GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state == GameManager.State.HIT)
            {
                score += 50;
                if(score > count&&count<2300)
                {
                    moveSpeed = moveSpeed + 0.075f;
                    count = count + 300;
                    randomNum--;
                }
                if (score == 500 || score == 1500 || score == 3000)
                {
                    SpawnItem();
                }
                RandomObstacle();
                CallTime = CallTime - callTimeSpeed * Time.deltaTime;
            }
        }
    }

    //초기화
    private void Init()
    {
        moveSpeed = 1;
        randomNum = 10;
        count = 500;
        score = 0;
        Speed = 5;
        CallTime = 3;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //아이템 스폰
    private void SpawnItem()
    {
        transform.Find("Item").gameObject.SetActive(true);
    }
}
