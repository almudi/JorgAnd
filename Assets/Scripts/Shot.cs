using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public ParticleSystem shot;

	Transform bulletSpawn;
	ParticleSystem shotEffect;

	void Start()
	{
		bulletSpawn = transform.GetChild (0);
		shotEffect = shot.GetComponent<ParticleSystem> () as ParticleSystem;
		if (shotEffect != null) {
			Debug.Log ("ShotEffect no es nulo");
		}
	}

	void Update()
	{

		Debug.Log (shot.emission.enabled);
		shot.Emit (1000);
		if (!shot.isPlaying) {
			shot.Simulate (0.0f, true, true);
			shot.Play ();
		}

		if (shot.isPlaying)
			shot.Stop();
	}
	public void pistolShot(){
		Debug.Log ("Disparando");
		shot.Play ();
	}
}
