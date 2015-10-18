using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_TimeToDestroy = 5.0f;
    [SerializeField]
    private int m_DamagePerShot = 2;
    [SerializeField]
    private int m_Velocity = 2;

    void Update ()    {
        transform.Translate (0, m_Velocity * Time.deltaTime-1, 0);
    }


    void OnEnable()
    {
        Invoke("Die", m_TimeToDestroy);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
            other.transform.SendMessage("TakeDamage", m_DamagePerShot, SendMessageOptions.DontRequireReceiver);

        CancelInvoke("Die");
        Die();
    }
}
