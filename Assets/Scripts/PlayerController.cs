using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public enum Weapon{Pistol, Axe};
	public float moveSpeed = 5;
	public GameObject primaryWeapon;
	public GameObject secondaryWeapon;
	public Transform weaponSpawn;

	Rigidbody rbody;
	float camRayLength = 100f;
	Animator playerAnimator;
	float attackTimer = 0f;
	float characterSpeed;
	bool isCrouching = false;
	Weapon myWeapon;
	GameObject equipedWeapon;
	bool isAiming = false;



	void Start()
	{	
		myWeapon = Weapon.Axe;
		equipedWeapon = Instantiate (primaryWeapon, Vector3.zero, Quaternion.identity) as GameObject;
		equipedWeapon.SetActive (true);
		setWeaponPosition (primaryWeapon.transform);

		characterSpeed = moveSpeed;
		playerAnimator = GetComponent<Animator> ();
		rbody = GetComponent<Rigidbody> ();
	}
		

	void Update()
	{
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		MoveAnimation (horizontal, vertical);
		Move (horizontal, vertical);
		Turning ();
		Attack(myWeapon);
		BlockAttack (myWeapon);
		Aim (myWeapon);
		SetWeapon (primaryWeapon, secondaryWeapon);
	}


	//Movimiento del objecto a lo largo del plano
	void Move(float h, float v)
	{
			Vector3 movement = new Vector3 (h, 0.0f, v);
			movement = movement.normalized * moveSpeed * Time.deltaTime;
			rbody.MovePosition ( transform.position + movement);
	}


	//Giro del objecto mirando a la posicion del raton
	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			//suavizado del movimiento de rotacion
			float desiredAngle = Quaternion.LookRotation (playerToMouse).eulerAngles.y;
			float angle = Mathf.LerpAngle (transform.eulerAngles.y, desiredAngle, Time.deltaTime * 12);
			Quaternion rotation = Quaternion.Euler (0, angle, 0);
			rbody.MoveRotation (rotation);
		}

	}

	//Animaciones de movimiento
	void MoveAnimation(float h, float v)
	{
		float hor = h;
		float ver = v;
		if (h < 0)
			hor *= -1;
		if (v < 0)
			ver *= -1;
		if (isAiming) {		//Si esta apuntando se mueve caminando
			moveSpeed = 2.5f;
			playerAnimator.SetFloat ("axisSpeed",Mathf.Lerp(0, 1, hor + ver) * 0.5f);
		} else {			//Si no se mueve con normalidad
			moveSpeed = characterSpeed;
			playerAnimator.SetFloat("axisSpeed",Mathf.Lerp(0, 1, hor + ver));	//Calcula un valor entre 0 y 1 apartir de los axis horizontal y vertical
		}
			
	}
		
	void Aim(Weapon weapon)
	{
		if (weapon.Equals (Weapon.Pistol)) {
			playerAnimator.SetLayerWeight (2, 1f);
			if (Input.GetMouseButton (1)) {
				isAiming = true;
				playerAnimator.SetBool ("isAiming", isAiming);
			}
			if (Input.GetMouseButtonUp (1)) {
				isAiming = false;
				playerAnimator.SetBool ("isAiming", isAiming);
				playerAnimator.SetFloat ("axisSpeed", 1f);
			}
		} else {
			playerAnimator.SetLayerWeight (2, 0f);
			isAiming = false;
		}
	}

	void BlockAttack(Weapon weapon)
	{
		if(weapon.Equals(Weapon.Axe)){
			
			if (Input.GetMouseButton (1)) {
				playerAnimator.SetBool ("isBlocking", true);
				playerAnimator.SetLayerWeight (1, 1f);
			}
			if (Input.GetMouseButtonUp (1)) {
				playerAnimator.SetBool ("isBlocking", false);
				playerAnimator.SetLayerWeight (1, 0f);
			}
		}
	}


	void Attack(Weapon weapon)
	{
		if (weapon.Equals (Weapon.Axe)) {
			if (attackTimer <= 0) {
				playerAnimator.SetInteger ("attackNumber", 0);
				playerAnimator.SetLayerWeight (1, 0f);
			}
			if (attackTimer <= 0 && Input.GetMouseButton (0)) {
				attackTimer = 1.4f;
				playerAnimator.SetInteger ("attackNumber", 1);
				playerAnimator.SetLayerWeight (1, 1f);
			}
			attackTimer -= Time.deltaTime;
		}

		if (weapon.Equals (Weapon.Pistol) && isAiming && Input.GetMouseButtonDown (0)) {
			Shot a;
			a = secondaryWeapon.GetComponent<Shot> () as Shot;
			if (a != null) {
				a.pistolShot ();
			}
		}
	}


	void Crouch()
	{
		if (Input.GetKeyDown (KeyCode.C)) {
			if (!isCrouching) {
				isCrouching = true;
				playerAnimator.SetBool("isCrouching",isCrouching);
				Debug.Log ("Me agacho");
			} else {
				isCrouching = false;
				playerAnimator.SetBool("isCrouching",isCrouching);
			}
		}
	}



	//Cambio entre arma primaria y secundaria
	void SetWeapon(GameObject primary, GameObject secondary)
	{	
		if(Input.GetKeyDown("1")){			//Primary Weapon
			myWeapon = Weapon.Axe;
			Destroy (equipedWeapon);
			equipedWeapon = Instantiate (primary, weaponSpawn.position, weaponSpawn.rotation) as GameObject;
			setWeaponPosition(primary.transform);
		}

		if(Input.GetKeyDown("2")){			//Secondary Weapon
			myWeapon = Weapon.Pistol;
			Destroy (equipedWeapon);
			equipedWeapon = Instantiate (secondary, weaponSpawn.position, weaponSpawn.rotation) as GameObject; 
			setWeaponPosition (secondary.transform);
		}
	
	}	


	void setWeaponPosition(Transform location)
	{
		equipedWeapon.transform.rotation = weaponSpawn.rotation;
		equipedWeapon.transform.position = weaponSpawn.position;
		equipedWeapon.transform.SetParent (weaponSpawn);
		equipedWeapon.transform.localRotation = location.rotation;
		equipedWeapon.transform.localPosition = location.position;
	}
}
		

