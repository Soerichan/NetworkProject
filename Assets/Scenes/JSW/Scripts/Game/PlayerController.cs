namespace jsw
{
using Cinemachine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

[RequireComponent(typeof(DoggyMan))]
public class PlayerController : MonoBehaviourPun
{
	private DoggyMan Doggy;
	
	private void Awake()
	{
		Doggy = GetComponent<DoggyMan>();
		//camera = GameObject.Find("vcam1").GetComponent<CinemachineVirtualCamera>();
	}

	public void Start()
	{
		//foreach (Renderer r in GetComponentsInChildren<Renderer>())
		//{
		//	r.material.color = GameData.GetColor(photonView.Owner.GetPlayerNumber());
		//}
		if (photonView.IsMine)
		{
			//camera.LookAt=this.transform;
			//camera.Follow=this.transform;
		}
		if (!photonView.IsMine)
			Destroy(this);
	}

	public void Update()
	{
		Accelate();
		Rotate();
		Fire();
	}

	private void Accelate()
	{
		float vInput = Input.GetAxis("Vertical");

		Doggy.Accelate(vInput);
		
	}

	private void Rotate()
	{
		float hInput = Input.GetAxis("Horizontal");

		Doggy.Rotate(hInput);
	}

	private void Fire()
	{
		if (Input.GetButtonDown("Fire1"))
			Doggy.Fire();
	}
}
}