using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    TextMesh textMesh;
    Transform obstacleTr1;
    Transform obstacleTr2;

    public int score;

    //움직이는 장애물
    private bool moveObstacle;
    private float moveSpeed;



    private void OnEnable()
    {
        //점수 관련 코드
        textMesh = GetComponent<TextMesh>();
        score = ObstacleManager.Instance.score;
        textMesh.text = "" + score;

        moveObstacle = false;

        //빈 공간 크기
        EmptySize(GameManager.Instance.Interval);
        RandomPosition();

        //칼 삭제
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Sword")
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //움직이는 장애물 관련
        if (score > 300)
        {
            if(Random.Range(0, ObstacleManager.Instance.randomNum) == 0)
            {
                moveSpeed = ObstacleManager.Instance.moveSpeed;
                moveObstacle = true;
            }
        }
    }

    private void EmptySize(float interval)
    {
        obstacleTr1 = transform.GetChild(0);
        obstacleTr2 = transform.GetChild(1);

        obstacleTr1.localPosition = new Vector3(0, -interval, 0);
        obstacleTr2.localPosition = new Vector3(0, interval, 0);
    }

    private void RandomPosition()
    {
        transform.position += new Vector3(0, Random.Range(-3.2f, 3.2f), 0);
    }

    private void Update()
    {
        if (GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state == GameManager.State.HIT)
        {
            if (moveObstacle)
            {
                transform.position -= new Vector3(ObstacleManager.Instance.Speed, moveSpeed, 0) * Time.deltaTime;
                if(transform.position.y>3.2f || transform.position.y < -3.2f)
                {
                    moveSpeed = moveSpeed * -1;
                }
            }

            else
            {
                transform.position -= new Vector3(ObstacleManager.Instance.Speed, 0, 0) * Time.deltaTime;
            }
        }


    }
}
