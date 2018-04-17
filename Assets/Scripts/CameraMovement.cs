using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        while (true)
        {
            // 매 초마다 카메라를 옆으로 옮깁니다.
            yield return new WaitForSeconds(1.0f);
            transform.position += new Vector3(1, 0, 0);
        }
    }
}
