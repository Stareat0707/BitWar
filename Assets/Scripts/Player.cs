using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State
    {
        None,
        Buy,
        Sell
    }

    [SerializeField] GameObject m_GameManager;

    State m_State;

    float m_Money;

    void Start()
    {
        m_State = State.None;
    }

    public void ClickButton()
    {
        switch(m_State)
        {
            case State.None:
                m_State = State.Buy;
                m_Money = m_GameManager.GetComponent<GameManager>().GetMoney() * -1;
                break;
            case State.Buy:
                m_State = State.Sell;
                m_Money += m_GameManager.GetComponent<GameManager>().GetMoney();
                break;
        }
    }
}
