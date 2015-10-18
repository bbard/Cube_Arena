using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int m_Health = 2;
    private int m_CurrentHealth;
    [SerializeField]
    private int m_PointToPlayer = 10;
    private static ScoreManager m_Score = null;

    void Start()
    {
        if (m_Score)
            return;

        GameObject go = GameObject.Find("ScoreManager");
        m_Score = go.GetComponent<ScoreManager>();
    }

    void OnEnable()
    {
        m_CurrentHealth = m_Health;
    }

    public void TakeDamage(int damage)
    {
        m_CurrentHealth -= damage;

        if (m_CurrentHealth > 0)
            return;

        m_Score.ScoreUp(m_PointToPlayer);
        gameObject.SetActive(false);
    }
}
