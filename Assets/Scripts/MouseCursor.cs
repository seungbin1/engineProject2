using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private void OnEnable()
    {
        transform.position = new Vector3(2.5f, 0, 0);
    }
    private void Update()
    {
        //게임 매니저에서 입력받은 마우스 포지션으로 이동
        CursorMove(GameManager.Instance.mousePostion);
    }
    private void CursorMove(Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}
