using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour 
{
    private Transform m_TargetTransform;
    private NavMeshAgent m_NavMesh;

    [SerializeField]
    private int m_Damage = 10;

    [SerializeField]
    private float m_Time = 0.5f;

    private bool m_Damaged = false;

    void Awake()
    {
        m_NavMesh = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Objective");
        m_TargetTransform = player.GetComponent<Transform>();
    }

	void Update () 
    {
        m_NavMesh.SetDestination(m_TargetTransform.position);
	}

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Objective"))
            return;

        if (m_Damaged)
            return;
        
        other.SendMessage("TakeDamage", m_Damage, SendMessageOptions.DontRequireReceiver);
        StartCoroutine("Damage");
    }

    IEnumerator Damage()
    {
        m_Damaged = true;
        yield return new WaitForSeconds(m_Time);
        m_Damaged = false;
    }
}
