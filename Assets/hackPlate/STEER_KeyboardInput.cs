using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STEER_KeyboardInput : MonoBehaviour {
	public  KeyCode left = KeyCode.LeftArrow;
    public  KeyCode right = KeyCode.RightArrow;
    public  KeyCode up = KeyCode.UpArrow;
    public  KeyCode down = KeyCode.DownArrow;
    public KeyCode Jump = KeyCode.Space;
    public Vector3 Rotation = new Vector3(0,45,0);
    private Vector3 MoveDir;

    // Use this for initialization
    void Start () {
		 GetComponent<steerable>().AddSteeringAction("InputMovePlayer",SeekDirection);
	}
	
	// Update is called once per frame
	void Update () {
		MoveDir = Vector3.zero;
        var rotation = Quaternion.Euler(Rotation);
        //get direction from input
        if (Input.GetKey(left)) { MoveDir += Vector3.left; }
        if (Input.GetKey(right)) { MoveDir += Vector3.right; }
        if (Input.GetKey(up)) { MoveDir += Vector3.forward; }
        if (Input.GetKey(down)) { MoveDir += Vector3.back; }
        if (Input.GetKeyDown(Jump)) { gameObject.PublishGlobal("PLAYER_JUMP", gameObject); }
        //gameObject.PublishGlobal("PLAYER_MOVE", MoveDir);
        MoveDir = rotation * MoveDir;
        MoveDir = MoveDir.normalized;
	}

	Vector3 SeekDirection(steerableBase p_steerable)
	{
		return Vector3.ClampMagnitude(steerableBase.seek(p_steerable.transform.position + MoveDir,p_steerable), p_steerable.MaxForce);
	}
}
