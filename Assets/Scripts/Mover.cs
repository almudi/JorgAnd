using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float moveSpeed;
	public float lifeTime=5f;


	void Awake(){
		transform.eulerAngles += new Vector3 (90, 0, 0);
		transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
	}

	void Update () {
		GameObject player = GameObject.FindWithTag("Player");

		transform.Translate (new Vector3(0,1,0) * moveSpeed * Time.deltaTime);
		if (lifeTime <= 0) {
			Destroy (this.gameObject);
		}
		lifeTime -= Time.deltaTime;
	}
}
