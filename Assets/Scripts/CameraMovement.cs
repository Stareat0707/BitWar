using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;

    void Start()
    {
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        while (true)
        {
            transform.position += new Vector3(speed, 0.0f, 0.0f);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
