using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public GameObject bullet;
	public ParticleSystem shotEffectPrefab;
	public Transform bulletSpawn;
	public float fireRate;

	ParticleSystem particle;
	void Awake()
	{
		
	}

	void Update()
	{
		
	}
	public void pistolShot()
	{
		if (fireRate<=0) {
			fireRate = 0.3f;
			particle = Instantiate (shotEffectPrefab, bulletSpawn.position, bulletSpawn.rotation) as ParticleSystem;
			particle.Play ();
			Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
		}
		fireRate -= Time.deltaTime;
		Debug.Log ("Shot");
	}

	public ParticleSystem GetShot()
	{
		return shotEffectPrefab;
	}
}
