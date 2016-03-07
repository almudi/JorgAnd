using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public GameObject bullet;
	public ParticleSystem shotEffectPrefab;
	public Transform bulletSpawn;
	public float fireRate;

	public Transform cursor;

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
			faceForward ();
			particle = Instantiate (shotEffectPrefab, bulletSpawn.position, bulletSpawn.rotation) as ParticleSystem;
			particle.Play ();
			Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);
		}
		fireRate -= Time.deltaTime;
	}

	public ParticleSystem GetShot()
	{
		return shotEffectPrefab;
	}

	private void faceForward()
	{
		Camera camera = Camera.main;
		Transform bulletSpawn = GetComponentInChildren<Transform> ();

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit))
		{
			Vector3 posDisparo = new Vector3 (floorHit.point.x, bulletSpawn.position.y, floorHit.point.z);
			bulletSpawn.LookAt (posDisparo);

			cursor.position = floorHit.point + Vector3.up;

		}
	}
		
}
