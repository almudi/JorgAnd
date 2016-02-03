using UnityEngine;
using System.Collections;

public class DungeonCrawlerCamera : MonoBehaviour {

	public GameObject target;
	public float damping = 1;

	private Vector3 offset;

	void Start(){
		offset = transform.position - target.transform.position;
		//offset = target.transform.position - transform.position;
	}

	void LateUpdate () {
		Vector3 desiredPosition = target.transform.position + offset;
		Vector3 position = Vector3.Lerp(transform.position,desiredPosition, Time.deltaTime * damping);
		transform.position = position;

		//la camara tiene que mirar al player con LookAt
		transform.LookAt(target.transform.position);
	}
}
