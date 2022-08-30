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

        //회전방향
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

    //아이템과 충돌시 게임 화면 위아래로 자석이 내려온다
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            magnet.transform.position += new Vector3(0, 0.4f, 0);
            magnet2.transform.position += new Vector3(0, -0.4f, 0);
        }
    }

    //플레이어의 이동에 관한 함수
    //자석의 Y좌표 뿐만 아니라 X좌표도 칼의 이동속도에 영향을 준다 칼에서 자석이 멀어질수록 이동속도가 느려지게 구현되었다.
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

    //오른쪽으로 칼이 회전할 때
    private void PlayerRotateRight()
    {
        GameManager.Instance.SwordRight = true;
        rotateSpeed = -1000;
        rightbutton.gameObject.SetActive(false);
        leftbutton.gameObject.SetActive(false);
        GameManager.Instance.state = GameManager.State.PLAYING;
    }
    
    //왼쪽으로 칼이 회전할 때
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