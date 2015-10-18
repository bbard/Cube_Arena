using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
    public float m_Time = 2.0f;

    private int m_Health = 100;
    private int m_CurrentHealth;
    private Slider m_HealthSlider;
    private float m_StartTime;

    private ScreenManager m_ScreenManager;
    private PlayerController m_PlayerController;
    private bool m_IsAlive = true;

    void Awake()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }
   
    void Start()
    {
        m_ScreenManager = ScreenManager.Instance;
        m_CurrentHealth = m_Health;
        m_HealthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
    }

    public void TakeDamage(int damage)
    {
        m_StartTime = Time.time;
        m_CurrentHealth = Mathf.Clamp(m_Health - damage,0,  100);
        if (m_CurrentHealth <= 0.0f && m_IsAlive)
            StartCoroutine("Gameover");
    }

    IEnumerator Gameover()
    {
        m_IsAlive = false;
        m_PlayerController.enabled = false;
        yield return new WaitForSeconds(2.0f);
        m_ScreenManager.LoadLevel("Gameover");
    }

    public void Heal(int amountHealth)
    {
        m_StartTime = Time.time;
        m_CurrentHealth = Mathf.Clamp(m_Health + amountHealth,0, 100);
    }

    void Update()
    {
        m_Health = (int)Mathf.Lerp(m_Health, m_CurrentHealth, (Time.time - m_StartTime) / m_Time);
        m_HealthSlider.value = m_Health;
    }
}

