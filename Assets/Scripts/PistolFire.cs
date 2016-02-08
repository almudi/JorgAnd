using UnityEngine;
using System.Collections;

public class PistolFire : MonoBehaviour {

	ParticleSystem shotEffectPrefab;
	ParticleSystem clone;
	void Awake () {
		shotEffectPrefab = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			clone = Instantiate (shotEffectPrefab, transform.position, transform.rotation) as ParticleSystem;
			clone.Play ();

		}
	}
}
