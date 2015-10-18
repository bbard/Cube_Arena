using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObjectPooler))]
public class PlayerShooter : MonoBehaviour 
{
    [SerializeField]
    private float m_Force = 12.0f;
    [SerializeField]
    private AudioClip m_ShootSound;
    [SerializeField]
    private Transform m_WeaponTransform;

    private ObjectPooler m_BulletPooler;

    void Awake()
    {
        m_BulletPooler = GetComponent<ObjectPooler>();
    }

    public void Shoot()
    {
        if (!m_WeaponTransform)
            return;

        GameObject go = m_BulletPooler.NextObject();
        go.transform.position = m_WeaponTransform.position;
        go.transform.rotation = m_WeaponTransform.rotation;
        go.SetActive(true);

        Rigidbody bullet = go.GetComponent<Rigidbody>();
        bullet.velocity = Vector3.zero;
        bullet.AddForce(m_WeaponTransform.forward * m_Force, ForceMode.Impulse);

        if (m_ShootSound)
            AudioSource.PlayClipAtPoint(m_ShootSound, m_WeaponTransform.position);
    }
	
}
