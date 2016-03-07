using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination (GameObject.FindGameObjectWithTag ("Player").transform.position);
	}
}
