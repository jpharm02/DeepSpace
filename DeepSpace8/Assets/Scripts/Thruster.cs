using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides a light trail when ship is accelerating.
[RequireComponent(typeof(TrailRenderer))]
public class Thruster : MonoBehaviour
{
    TrailRenderer tr;

    private void Awake()
    {
        tr = GetComponent<TrailRenderer>();
    }

    public void Activate(bool activate = true)
    {
        if (activate)
        {
            tr.enabled = true;
        }
        else
        {
            tr.enabled = false;
        }
    }

}
