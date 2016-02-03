using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float smooth = 1;
	public int cameraUpValue = 1;

	Vector3 offset;


	void Start () {
		offset =  transform.position - target.transform.position;
	}

	void Update () {
		Vector3 desiredPosition = Vector3.Lerp (transform.position, target.transform.position + offset, smooth * Time.deltaTime);
		transform.position = desiredPosition;
		transform.LookAt (target.transform.position);
		CameraMovementAux (cameraUpValue);
	}

	//Recoloca la posicion de la camara en el eje Y mientras pulsas el boton S  segun el factor 'cameraUpValue' 
	void CameraMovementAux(float cameraUpValue)
	{
		if (Input.GetKeyDown(KeyCode.S)) {
			//offset += new Vector3 (0, cameraUpValue, 0);
		}
		if (Input.GetKeyUp(KeyCode.S)) {
			//offset -=  new Vector3 (0, cameraUpValue, 0);
		}
	}
}
