using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject magnet;
    public GameObject magnet2;

    public float initSpeed = 0.1f;
    public float rotateSpeed;

    private float targetY;

    public Button rightbutton;
    public Button leftbutton;

    private MeshRenderer meshRenderer;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        //ȸ������
        rightbutton.onClick.AddListener(PlayerRotateRight);
        leftbutton.onClick.AddListener(PlayerRotateLeft);
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.state == GameManager.State.PLAYING || GameManager.Instance.state == GameManager.State.HIT)
        {
            MovePlayer(GameManager.Instance.mousePostion);
            transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
        }
    }

    //�����۰� �浹�� ���� ȭ�� ���Ʒ��� �ڼ��� �����´�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            magnet.transform.position += new Vector3(0, 0.4f, 0);
            magnet2.transform.position += new Vector3(0, -0.4f, 0);
        }
    }

    //�÷��̾��� �̵��� ���� �Լ�
    //�ڼ��� Y��ǥ �Ӹ� �ƴ϶� X��ǥ�� Į�� �̵��ӵ��� ������ �ش� Į���� �ڼ��� �־������� �̵��ӵ��� �������� �����Ǿ���.
    public void MovePlayer(Vector3 targetPos)
    {
        float speed;
        targetPos = targetPos - transform.position;
        targetY += targetPos.y * Time.deltaTime * 6;
        targetY = Mathf.Clamp(targetY, -10, 10f);
        speed = initSpeed - targetPos.x * Time.deltaTime * 1.75f;
        transform.position += new Vector3(0, targetY, 0) * speed * Time.deltaTime;
        transform.position = new Vector3(-5, Mathf.Clamp(transform.position.y, -5, 5), 0);
    }

    //���������� Į�� ȸ���� ��
    private void PlayerRotateRight()
    {
        GameManager.Instance.SwordRight = true;
        rotateSpeed = -1000;
        rightbutton.gameObject.SetActive(false);
        leftbutton.gameObject.SetActive(false);
        GameManager.Instance.state = GameManager.State.PLAYING;
    }
    
    //�������� Į�� ȸ���� ��
    private void PlayerRotateLeft()
    {
        GameManager.Instance.SwordLeft = true;
        rotateSpeed = 1000;
        rightbutton.gameObject.SetActive(false);
        leftbutton.gameObject.SetActive(false);
        GameManager.Instance.state = GameManager.State.PLAYING;
    }

    public IEnumerator Twinkle()
    {
        for (int i = 0; i < 4; i++)
        {
            meshRenderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            meshRenderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
        }
    }


}