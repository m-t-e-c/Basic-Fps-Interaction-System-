using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementRestrictor : MonoBehaviour
{
    [SerializeField] private Vector3 max;
    [SerializeField] private Vector3 min;

    [SerializeField] private bool deactive;
    [SerializeField] private bool clampX;
    [SerializeField] private bool clampY;
    [SerializeField] private bool clampZ;

    private void Update()
    {
        if (deactive) return;
        Vector3 restrictedPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(clampX) restrictedPos.x = Mathf.Clamp(restrictedPos.x, min.x, max.x);
        if(clampY) restrictedPos.y = Mathf.Clamp(restrictedPos.y, min.x, max.y);
        if(clampZ) restrictedPos.z = Mathf.Clamp(restrictedPos.z, min.x, max.z);
        transform.position = restrictedPos;
    }
}