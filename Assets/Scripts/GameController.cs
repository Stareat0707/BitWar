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

    private float nowMoney;

    private Vector3 spawnPosition;

    private float height;

    private State state;

    void Start()
    {
        // 변수들을 초기화하고 코루틴을 시작합니다.
        height = maxY - minY;
        nowMoney = startMoney;
        state = State.NO_CHANGED;
        StartCoroutine(UpdateGraph());
    }

    IEnumerator UpdateGraph()
    {
        while (true)
        {
            // 새로 그릴 점의 높이를 구합니다.
            spawnPosition.y = nowMoney / (startMoney * 2) * height + minY;

            // 그래프 점을 찍고 색을 변경합니다.
            GameObject pointInstance = Instantiate<GameObject>(point, spawnPosition, new Quaternion());
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

            // 상승/하락률을 무작위로 구합니다.
            float percentage = Random.Range(-1.0f, 1.0f);

            // 그래프가 최고/최저점일 경우 방향을 반대로 꺾습니다.
            if ((nowMoney <= 0 && percentage < 0) || (nowMoney >= startMoney * 2 && percentage > 0))
                percentage *= -1;

            // 현재 그래프를 업데이트 합니다.
            float additionalMoney = (float)startMoney * percentage;
            nowMoney = Mathf.Clamp(nowMoney + additionalMoney, 0, startMoney * 2);

            // 업데이트된 그래프에 따라 상태 변수를 변경합니다.
            if (additionalMoney > 0)
                state = State.UP;
            else if (additionalMoney == 0)
                state = State.NO_CHANGED;
            else
                state = State.DOWN;

            // 다음에 그려질 점의 위치를 한 칸 옮깁니다.
            ++(spawnPosition.x);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
