using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ScaleY = 현재 돈 - 이전 돈 / (최대 변화량 / 2)
 * PositionY = (이전 돈 + 현재 돈 - 시작 돈 * 2) / 최대 변화량
 */

public class GameManager : MonoBehaviour
{
    [SerializeField] int m_StartMoney;
    [SerializeField] int m_Range;

    [SerializeField] float m_PointWidth;

    [SerializeField] GameObject m_Point;
    [SerializeField] GameObject m_Camera;

    int m_Money;
    int m_Turn;

    float m_PrevSpawnPositionY;

    void Start()
    {
        m_Money = m_StartMoney;
        m_Turn = 1;
        m_PrevSpawnPositionY = 0;
        StartCoroutine("Func");
    }

    IEnumerator Func()
    {
        for (int i = 0; i < 60; ++i)
        {
            int prevMoney = m_Money;
            m_Money = Mathf.Max(Random.Range(m_Money - m_Range, m_Money + m_Range), 0);

            float cameraPositionX = m_Camera.transform.position.x;
            if (m_Turn > 8)
                cameraPositionX += m_PointWidth;

            m_Camera.transform.position = new Vector3(cameraPositionX, m_PrevSpawnPositionY, -10);

            float scaleY = (float)(m_Money - prevMoney) / ((float)m_Range / 2);
            float positionX = m_Turn * m_PointWidth - m_PointWidth / 2 - 2;
            m_PrevSpawnPositionY = (float)(prevMoney + m_Money - m_StartMoney * 2) / m_Range;
            Vector3 spawnPosition = new Vector3(positionX, m_PrevSpawnPositionY, 0);
            Vector3 spawnScale = new Vector3(m_PointWidth, scaleY, 0);

            GameObject point = Instantiate<GameObject>(m_Point, spawnPosition, Quaternion.identity);
            point.transform.localScale = spawnScale;
            point.GetComponent<SpriteRenderer>().color = (m_Money > prevMoney ? Color.blue : Color.red);

            Debug.Log("Money: " + m_Money);

            ++m_Turn;
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Game Over");
    }

    public float GetMoney()
    {
        return m_Money;
    }
}
