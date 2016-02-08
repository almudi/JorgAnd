using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour {


	IEnumerator Start()
	{
		yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
		Destroy(gameObject); 
	}

}
