using UnityEngine;
using System.Collections;

public class Atirador : MonoBehaviour
{

    public Transform bullet;
    public Transform rocket;
    

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
           FireRocket();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }


    void FireBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        
    }

    void FireRocket()
    {    
       Instantiate(rocket, transform.position, transform.rotation);
    }


}