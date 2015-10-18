using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	public float vel ;
	//public int tempo=0;
	void Start () {

	}
	

	void Update () {
        //Lancar o tiro
		transform.Translate (0, vel * Time.deltaTime-1, 0);
		//tempo++;
        //Destruir o tiro depois de x segundos
	//	if (tempo >= 100)
		//	Destroy (gameObject);
	}
}
