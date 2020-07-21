using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follow Cam script that allows the user to see the space ship through a camera lens while playing the game.
public class FollowCam : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    Vector3 defaultDistance = new Vector3(0f, 2f, -10f);

    [SerializeField]
    float distanceDamp = 1.5f;

    Transform myT;

    public Vector3 velocity = Vector3.one;

    void Awake()
    {
        myT = transform;
    }

    void LateUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        Vector3 toPos = target.localPosition + (target.localRotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref velocity, distanceDamp);
        myT.position = curPos;

        myT.LookAt(target, target.up);
    }

}
