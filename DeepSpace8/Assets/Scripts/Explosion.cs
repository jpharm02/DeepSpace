using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Provides particle features when colliding with obstacles.
[DisallowMultipleComponent]
public class Explosion : MonoBehaviour
{
    public GameObject explosion;

    public void IveBeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            IveBeenHit(contact.point);
        }
    }
}
