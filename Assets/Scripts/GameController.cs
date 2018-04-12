using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject point;

    public float minY, maxY;

    public int startMoney;

    public int nowMoney;

    private float x;

    public float height;

    void Start()
    {
        x = 0.0f;
        height = maxY - minY;
        nowMoney = startMoney;
        StartCoroutine(UpdateGraph());
    }

    IEnumerator UpdateGraph()
    {
        while (true)
        {
            Debug.Log(nowMoney);

            Instantiate<GameObject>(point, new Vector3(x, nowMoney / (startMoney * 2.0f) * height + minY), new Quaternion());

            x += 0.32f;

            float percentage = Random.Range(-1.0f, 1.0f);

            if ((nowMoney <= 0 && percentage < 0) || (nowMoney >= startMoney * 2 && percentage > 0))
                percentage *= -1;

            int additionalMoney = (int)((float)startMoney * percentage);

            nowMoney = Mathf.Clamp(nowMoney + additionalMoney, 0, startMoney * 2);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
