using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    enum State
    {
        NO_CHANGED,
        UP,
        DOWN
    }

    public GameObject point;

    public float minY, maxY;

    public int startMoney;

    public int nowMoney;

    private float x;

    private float height;

    private State state;

    void Start()
    {
        // 변수들을 초기화하고 코루틴을 시작합니다.
        x = 0.0f;
        height = maxY - minY;
        nowMoney = startMoney;
        state = State.NO_CHANGED;
        StartCoroutine(UpdateGraph());
    }

    IEnumerator UpdateGraph()
    {
        while (true)
        {
            // 그래프 점을 찍고 색과 크기를 변경합니다.
            GameObject pointInstance = Instantiate<GameObject>(point, new Vector3(x, nowMoney / (startMoney * 2.0f) * height + minY), new Quaternion());
            SpriteRenderer pointSprite = pointInstance.GetComponent<SpriteRenderer>();
            switch (state)
            {
                case State.NO_CHANGED:
                    pointSprite.color = new Color(0, 0, 0);
                    break;
                case State.UP:
                    pointSprite.color = new Color(0, 0, 1);
                    break;
                case State.DOWN:
                    pointSprite.color = new Color(1, 0, 0);
                    break;
            }

            float percentage = Random.Range(-1.0f, 1.0f);

            if ((nowMoney <= 0 && percentage < 0) || (nowMoney >= startMoney * 2 && percentage > 0))
                percentage *= -1;

            int additionalMoney = (int)((float)startMoney * percentage);

            nowMoney = Mathf.Clamp(nowMoney + additionalMoney, 0, startMoney * 2);

            if (additionalMoney > 0)
                state = State.UP;
            else if (additionalMoney == 0)
                state = State.NO_CHANGED;
            else
                state = State.DOWN;

            x += 0.32f;

            yield return new WaitForSeconds(1.0f);
        }
    }
}
