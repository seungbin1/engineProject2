using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    private bool right;
    private bool left;

    private bool hit;

    private Obstacle obstacle;
    private GameObject sword;

    private void OnEnable()
    {
        hit = true;
    }
    private void Start()
    {
        if (gameObject.transform == transform.parent.GetChild(0))
        {
            left = true;
            right = false;
        }
        if (gameObject.transform == transform.parent.GetChild(1))
        {
            right = true;
            left = false;
        }
        obstacle = transform.parent.GetComponent<Obstacle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Sword"&& hit)
        {
            hit = false;
            if (right && GameManager.Instance.SwordRight && gameObject.tag == "Wood")
            {
                if (collision.gameObject != null)
                {
                    Stuck(collision.gameObject);
                    Hit(collision);
                }
            }


            else if (left && GameManager.Instance.SwordLeft && gameObject.tag == "Wood")
            {
                if (collision.gameObject != null)
                {
                    Stuck(collision.gameObject);
                    Hit(collision);
                }
            }

            else
            {
                //Hit 당하면 점수 올리거나 유지하고 칼 생명 줄이고 다달면 게임 끝이고 메뉴 만들어야함, 칼이 아직 남아있으면 멈췄다가 잠깐 무적시간주고 다시 시작, 칼이 박혀있게 해야함
                if (GameManager.Instance.life > 1)
                {
                    collision.GetComponent<Player>().StartCoroutine(collision.GetComponent<Player>().Twinkle());
                }
                GameManager.Instance.LifeDecrease();
            }
        }
    }

    private void Hit(Collider2D collision)
    {
        if (GameManager.Instance.life > 1)
        {
            collision.GetComponent<Player>().StartCoroutine(collision.GetComponent<Player>().Twinkle());
        }
        GameManager.Instance.score += obstacle.score;
        GameManager.Instance.LifeDecrease();
    }

    private void Stuck(GameObject gameObject)
    {
        if (this.gameObject.transform.parent != null&&gameObject!=null)
        {
            sword = Instantiate(gameObject, this.gameObject.transform.parent.transform);
            sword.GetComponent<Player>().enabled = false;
            sword.GetComponent<AudioSource>().enabled = false;
            sword.transform.localPosition += new Vector3(-this.gameObject.transform.parent.position.x, -this.gameObject.transform.parent.position.y, 0);
        }
    }
}