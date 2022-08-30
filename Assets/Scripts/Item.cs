using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Player player;
    Vector3 targetPos;
    Vector3 magnetRotation;

    private void OnEnable()
    {
        FindTargetPosition();
        transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sword")
        {
            player = collision.GetComponent<Player>();
            player.initSpeed=player.initSpeed + player.initSpeed * 0.15f;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        transform.Translate(targetPos.normalized*Time.deltaTime*2);
        MagnetMove();
        if (transform.position.y > 5 || transform.position.y < -5)
        {
            targetPos = new Vector3(targetPos.x , -targetPos.y,0);
        }
    }

    private void FindTargetPosition()
    {
        targetPos = new Vector3(-10, Random.Range(-10, 10),0);
    }

    private void MagnetMove()
    {
        magnetRotation += new Vector3(300, 300, 0) * Time.deltaTime;
        transform.GetChild(0).transform.rotation = Quaternion.Euler(magnetRotation);
    }
}
