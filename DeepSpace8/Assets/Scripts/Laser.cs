using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Red laser emitted out of space ship. This laser is currently only used to show where the space ship is pointing.
// Some of the methods are not currently utilized and are left for future work purposes.
[DisallowMultipleComponent]
[RequireComponent(typeof(Light))]
[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField]
    float maxDistance = 300f;
    [SerializeField]
    float laserOffTime = .05f;
    LineRenderer lr;
    Light laserLight;
    bool canFire;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        laserLight = GetComponent<Light>();
    }

    void Start()
    {
        lr.enabled = false;
        laserLight.enabled = false;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.red);
    }

    // THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
    Vector3 CastRay()
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            Debug.Log("We hit: " + hit.transform.name);
            return hit.point;
        }

        Debug.Log("We missed...");
        return transform.position + (transform.forward * maxDistance);
    }

    // THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
    public void FireLaser(Vector3 targetPosition)
    {
        if (canFire)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, CastRay());
            lr.enabled = true;
            laserLight.enabled = true;
            canFire = false;
        }

        Invoke("TurnOffLaser", laserOffTime);
    }

    // THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
    void TurnOffLaser()
    {
        lr.enabled = false;
        laserLight.enabled = false;
        canFire = true;
    }

    public float Distance
    {
        get { return maxDistance; }
    }
}
