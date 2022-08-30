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
                //Hit ���ϸ� ���� �ø��ų� �����ϰ� Į ���� ���̰� �ٴ޸� ���� ���̰� �޴� ��������, Į�� ���� ���������� ����ٰ� ��� �����ð��ְ� �ٽ� ����, Į�� �����ְ� �ؾ���
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