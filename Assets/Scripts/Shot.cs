using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public GameObject bullet;
	public ParticleSystem shotEffectPrefab;
	public Transform bulletSpawn;

	ParticleSystem particle;
	void Awake()
	{
		
	}

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
			pistolShot();
	}
	public void pistolShot()
	{
		particle = Instantiate (shotEffectPrefab, bulletSpawn.position, bulletSpawn.rotation) as ParticleSystem;
		particle.Play ();
		Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
		Debug.Log ("Shot");
	}

	public ParticleSystem GetShot()
	{
		return shotEffectPrefab;
	}

}
