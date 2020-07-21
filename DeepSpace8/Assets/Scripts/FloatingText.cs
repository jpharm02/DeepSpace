using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 Offset = new Vector3(0, 200, 0); 

    void Start()
    {
        Destroy(gameObject, DestroyTime);

        //transform.localPosition += Offset;
    }

}
