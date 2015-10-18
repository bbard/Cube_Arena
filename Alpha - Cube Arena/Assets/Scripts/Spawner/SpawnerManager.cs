using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour 
{
    private Transform[] m_SpawnerPoints;
    private ObjectPooler[] m_Poolers;
    private float m_ElapsedTime = 0.0f;
    private float m_TimeToNextEnemy = 0.0f;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float m_MinTimeToNextEnemy = 1.0f;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float m_MaxTimeToNextEnemy = 10.0f;

    void Start()
    {
        // Procurando os pontos de spawn
        GameObject[] gos = GameObject.FindGameObjectsWithTag("SpawnerPoint");
        m_SpawnerPoints = new Transform[gos.Length];
        for (int i = 0; i < gos.Length; i++)
            m_SpawnerPoints[i] = gos[i].GetComponent<Transform>();
        // Pegando referência dos poolers
        m_Poolers = GetComponents<ObjectPooler>();
        m_TimeToNextEnemy = NextRandomTime();
    }

    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
        if (m_ElapsedTime < m_TimeToNextEnemy)
            return;
        m_ElapsedTime = 0.0f;
        m_TimeToNextEnemy = NextRandomTime();
        Spawn();
    }

    private float NextRandomTime()
    {
        return Random.Range(m_MinTimeToNextEnemy, m_MaxTimeToNextEnemy);
    }

    void Spawn()
    {
        int indexForSpawn = Random.Range(0, m_SpawnerPoints.Length);
        int indexForPooler = Random.Range(0, m_Poolers.Length);

        GameObject go = m_Poolers[indexForPooler].NextObject();

        go.transform.position = m_SpawnerPoints[indexForSpawn].position;
        go.SetActive(true);
    }
}
