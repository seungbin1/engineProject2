using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{

    public float speedX;
    public float speedY;

    private void FixedUpdate()
    {
        if(GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state == GameManager.State.HIT)
        {
            transform.position -= new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0);
            if (transform.position.x < -24.9f)
            {
                transform.position = new Vector3(24.9f, transform.position.y, transform.position.z);
            }
        }
    }
}
