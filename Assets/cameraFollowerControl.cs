using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowerControl : MonoBehaviour
{
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    private Vector3 MoveDir;
    private Follower follower;
    public Vector3 Rotation = new Vector3(0, 45, 0);
    // Start is called before the first frame update
    void Start()
    {
        follower = GetComponent<Follower>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveDir = Vector3.zero;
        var rotation = Quaternion.Euler(Rotation);

        if (Input.GetKey(left)) { MoveDir += Vector3.left; }
        if (Input.GetKey(right)) { MoveDir += Vector3.right; }
        if (Input.GetKey(up)) { MoveDir += Vector3.forward; }
        if (Input.GetKey(down)) { MoveDir += Vector3.back; }
        MoveDir = rotation * MoveDir.normalized ;
        var newPos = follower.transform.position + (MoveDir * 0.2f);
        //follower.target = new Vector3(newPos.x, newPos.y, newPos.z);
    }
}
