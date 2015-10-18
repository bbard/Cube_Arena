using UnityEngine;
using System.Collections;

// Dicas
// Awake - OnEnable - Start - OnDestroy - OnDisable - OnApplicationQuit
// (Pesados) GetComponent - Instantiate - Destroy - FindObject
// Update (FPS) - LateUpdate (Ultimo) - FixedUpdate (Física)

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5.0f;
    private int m_FloorMask;
    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private Vector3 m_Movement = Vector3.zero;

    private PlayerShooter m_Weapon;
    private bool m_CanShoot = true;
    [SerializeField]
    private float m_TimeToNextShoot = 0.5f;    // cooldown 

    void Awake()
    {
        m_FloorMask = LayerMask.GetMask("World");
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Transform = GetComponent<Transform>();
       m_Weapon = GetComponentInChildren<PlayerShooter>();
    }

    void FixedUpdate()
    {
        //GetAxis (-1, 1) - GetAxisRaw [-1, 0, 1]
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool shoot = Input.GetButtonDown("Fire1");

        Move(vertical, horizontal);
        Shoot(shoot);
        Turning();
    }

    void Shoot(bool shoot)
    {
        if (shoot && m_CanShoot)
        {
            StartCoroutine(TimeToNextShoot());
            m_Weapon.Shoot();
        }
    }

    IEnumerator TimeToNextShoot()
    {
        m_CanShoot = false;
        yield return new WaitForSeconds(m_TimeToNextShoot);
        m_CanShoot = true;
    }

    void Move(float vertical, float horizontal)
    {
        m_Movement.Set(horizontal, 0.0f, vertical);
        m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Transform.position + m_Movement);
    }

    void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(cameraRay, out floorHit, Camera.main.farClipPlane, m_FloorMask))
        {
            Vector3 playerToMouse = floorHit.point - m_Transform.position;
            playerToMouse.y = 0.0f;
            Quaternion rotation = Quaternion.LookRotation(playerToMouse);
            m_Rigidbody.MoveRotation(rotation);
        }
    }
}

