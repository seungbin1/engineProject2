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
        //���� �Ŵ������� �Է¹��� ���콺 ���������� �̵�
        CursorMove(GameManager.Instance.mousePostion);
    }
    private void CursorMove(Vector3 targetPos)
    {
        transform.position = targetPos;
    }
}
