using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS SCRIPT IS LEFT IN THIS PROJECT FOR FUTURE WORK ONLY.  CURRENTLY NOT UTILIZED IN SOLUTION.
public class Asteroid : MonoBehaviour
{
    public float minScale = 2.4f;
    public float maxScale = 6f;
    [SerializeField]
    float rotationOffset = 50f;


    Transform myT;
    Vector3 randomRotation;


    void Awake()
    {
        myT = transform;
    }

    void Start()
    {
        Debug.Log("Starting Roids");

        //// random size
        //Vector3 scale = Vector3.one;
        //scale.x = Random.Range(minScale, maxScale);
        //scale.y = Random.Range(minScale, maxScale);
        //scale.z = Random.Range(minScale, maxScale);

        //myT.localScale = scale;

        // random rotation
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }


    void Update()
    {
        Debug.Log("Updating Roids");

        myT.Rotate(randomRotation * Time.deltaTime);

    }

    private void OnDestroy()
    {

    }


}
