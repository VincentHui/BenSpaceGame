using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class STEER_seekPlayer : MonoBehaviour {
	// steerable body;
	// Use this for initialization
	public Transform p_target;
	void Start () {
		// p_target = GameFactory.Instance.Player;
		GetComponent<steerable>().AddSteeringAction("seekPlayer",SeekPlayer);
	}
	Vector3 SeekPlayer(steerableBase p_steerable)
	{
		return Vector3.ClampMagnitude(steerableBase.seek(p_target.position,p_steerable), p_steerable.MaxForce);
		// return Vector3.zero;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
