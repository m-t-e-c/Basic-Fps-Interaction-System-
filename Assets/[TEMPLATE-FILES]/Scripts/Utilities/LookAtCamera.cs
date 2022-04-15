using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;
    [SerializeField] private bool onlyYAxis;

    void Start() => cam = Camera.main.transform;

    private void Update()
    {
        if (onlyYAxis)
        {
            Vector3 targetPostition = new Vector3(cam.position.x, transform.position.y, cam.position.z);
            this.transform.LookAt(targetPostition);
        }
        else transform.LookAt(cam);
    }
}
