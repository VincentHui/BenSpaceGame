using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate Vector3 getForce(steerableBase p_body);
public struct RayCastCheckResult{
    public RaycastHit RayHit;
    public GameObject steerGameObject;
    public GameObject HitGameObject;
    public float DeltaValue;
    public bool Hit;
}

public class steerableBase : MonoBehaviour {
    protected List<getForce> ConsumablesToRemove = new List<getForce>();
    public static bool paused = false;
    protected Dictionary<string, getForce> actionMap = new Dictionary<string, getForce>();
    protected List<getForce> ConsumableActionMap = new List<getForce>(10);
    public float Speed = 2.0f;
    public float mass = 1f;
    public float MaxForce = 0.7f;
    public float drag = 0f;
    public bool Rotate = true;
    public float RotationSlerpValue = 0.05f;
    protected Vector3 _velocity, _acceleration;
    public Vector3 velocity { get { return _velocity; } set { _velocity = value; } }
    public Vector3 acceleration { get { return _acceleration; } }
    public LayerMask collideWith;
    public UnityEvent<RayCastCheckResult[]> OnCollide;
    protected float timeConstant = 200f;
    public static Vector3 seek(Vector3 p_target, steerableBase p_steer)
    {
        Vector3 desired = p_target - p_steer.transform.position;
        desired.Normalize();
        desired *= p_steer.Speed;

        //Reynolds’s formula for steering force
        var steer = desired - p_steer.velocity;
        steer = Vector3.ClampMagnitude(steer, p_steer.MaxForce);
        return steer;
        // p_steer.applyForce(steer);
    }
    public static Vector3 seekDirection(Vector3 p_direction, steerableBase p_steer)
    {
        Vector3 desired = p_direction;
        desired.Normalize();
        desired *= p_steer.Speed;

        //Reynolds’s formula for steering force
        var steer = desired - p_steer.velocity;
        steer = Vector3.ClampMagnitude(steer, p_steer.MaxForce);
        return steer;
        // p_steer.applyForce(steer);
    }
    public static Vector3 seekArrive(Vector3 p_target, steerableBase p_steer, float p_slowingRadius)
    {
        Vector3 desired = p_target - p_steer.transform.position;
        var distance = desired.magnitude;
        if (distance > p_slowingRadius)
        {
            desired.Normalize();
            desired *= p_steer.Speed;
        }
        else
        {
            // Outside the slowing area.
            desired.Normalize();
            desired *= p_steer.Speed * (distance / p_slowingRadius);
            // desired = normalize(desired_velocity) * max_velocity
        }
        //Reynolds’s formula for steering force
        var steer = desired - p_steer.velocity;
        steer = Vector3.ClampMagnitude(steer, p_steer.MaxForce);
        return steer;
        // p_steer.applyForce(steer);
    }
    public void AddSteeringAction(string p_name, getForce p_getforce)
    {
        actionMap.Add(p_name, p_getforce);
    }
    public void RemoveSteeringAction(string p_name)
    {
        actionMap.Remove(p_name);
    }
    public void AddConsumableSteeringAction(getForce p_getforce)
    {
        ConsumableActionMap.Add(p_getforce);
    }
    protected void applyForce(Vector3 p_force)
    {
        p_force /= mass;
        _acceleration += p_force;
    }
    protected Vector3 iterateActions(Vector3 temp, Dictionary<string, getForce>.Enumerator _Enem)
    {
        while (_Enem.MoveNext())
        {
            temp = temp + (_Enem.Current.Value(this)) * Time.deltaTime * timeConstant;
        }
        return temp;
    }

    protected Vector3 iterateConsumable(Vector3 temp, List<getForce>.Enumerator _consumableEnem, List<getForce> _ConsumablesToRemove)
    {
        while (_consumableEnem.MoveNext())
        {
            temp = temp + (_consumableEnem.Current(this)) * Time.deltaTime * timeConstant;
            _ConsumablesToRemove.Add(_consumableEnem.Current);
        }
        foreach (var item in _ConsumablesToRemove)
        {
            ConsumableActionMap.Remove(item);
        }
        return temp;
    }
    protected RayCastCheckResult RayCastCheckX(float p_DeltaMovementX, Vector3 p_origin, float p_skinWidth, int p_layerMak)
    {
        RayCastCheckResult result = new RayCastCheckResult();
        result.steerGameObject = gameObject;
        result.RayHit = new RaycastHit();
        var isGoingRight = p_DeltaMovementX > 0;
        var rayDirection = isGoingRight ? Vector3.right : Vector3.left;
        var tempRay = new Ray(p_origin, rayDirection);
        var rayDistance = Mathf.Abs(p_DeltaMovementX) + p_skinWidth;
        // X = new RaycastHit();
        var _raycastHit = Physics.Raycast(tempRay, out result.RayHit, rayDistance, p_layerMak);
        if (_raycastHit)
        {
            result.Hit = true;
            result.HitGameObject = result.RayHit.transform.gameObject;
            p_DeltaMovementX = result.RayHit.point.z - p_origin.z;
            //now set speed away from the contact point slightly
            if (isGoingRight)
            {
                p_DeltaMovementX -= p_skinWidth;
            }
            else
            {
                p_DeltaMovementX += p_skinWidth;
            }
        }
        result.DeltaValue = p_DeltaMovementX;
        return result;
    }
    protected RayCastCheckResult RayCastCheckZ(float p_DeltaMovementZ, Vector3 p_origin, float p_skinWidth, int p_layerMak)
    {
        RayCastCheckResult result = new RayCastCheckResult();
        result.steerGameObject = gameObject;
        result.RayHit = new RaycastHit();
        var isGoingForward = p_DeltaMovementZ > 0;
        var rayDirection = isGoingForward ? Vector3.forward : Vector3.back;
        var tempRay = new Ray(p_origin, rayDirection);
        var rayDistance = Mathf.Abs(p_DeltaMovementZ) + p_skinWidth;
        // Z = new RaycastHit();
        var _raycastHit = Physics.Raycast(tempRay, out result.RayHit, rayDistance, p_layerMak);
        if (_raycastHit)
        {
            result.Hit = true;
            result.HitGameObject = result.RayHit.transform.gameObject;
            p_DeltaMovementZ = result.RayHit.point.z - p_origin.z;
            //now set speed away from the contact point slightly
            if (isGoingForward)
            {
                p_DeltaMovementZ -= p_skinWidth;
            }
            else
            {
                p_DeltaMovementZ += p_skinWidth;
            }
        }
        result.DeltaValue = p_DeltaMovementZ;
        return result;
    }

    public bool CheckInCircle(float p_pointx, float p_pointy, float p_radius)
    {
        var boundry = Mathf.Sqrt(Mathf.Pow(p_pointx, 2) + Mathf.Pow(p_pointy, 2));
        return false;
    }
}


public class steerable : steerableBase
{

    public float SkinWidth;

    void Update()
    {
        if (paused) return;
        Vector3 tempVelocity = Vector3.zero;
        var _enemerator = actionMap.GetEnumerator();
        tempVelocity = iterateActions(tempVelocity, _enemerator);
        var _consumableEnem = ConsumableActionMap.GetEnumerator();
        tempVelocity = iterateConsumable(tempVelocity, _consumableEnem, ConsumablesToRemove);
        applyForce(tempVelocity);
        _velocity += acceleration;
        _velocity = drag == 0f ? _velocity : Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime);
        //collision checks!
        Vector3 deltaMovement = velocity * Time.deltaTime;
        if (collideWith != 0)
        {
            var resultZ = RayCastCheckZ(deltaMovement.z, transform.position, SkinWidth, collideWith);
            deltaMovement.z = resultZ.DeltaValue;
            var resultX = RayCastCheckX(deltaMovement.x, transform.position, SkinWidth, collideWith);
            deltaMovement.x = resultX.DeltaValue;
            if ((resultZ.Hit || resultX.Hit) && OnCollide != null) OnCollide.Invoke(new RayCastCheckResult[2] { resultZ, resultX });
        }
        transform.position += deltaMovement;
        if (velocity.normalized != Vector3.zero && Rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity.normalized), RotationSlerpValue);
        }
        _acceleration = Vector3.zero;
    }
}
