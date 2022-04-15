using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{

    [SerializeField] private Transform[] floatings;
    [SerializeField] private float underWaterDrag = 3f;
    [SerializeField] private float underWaterAngularDrag = 1f;


    [SerializeField] private float airDrag = 0f;
    [SerializeField] private float airAngularDrag = 0.05f;

    [SerializeField] private float floatingPower = 15f;
    [SerializeField] private bool underWater;

    private OceanManager oceanManager;

    private int floatingUnderWater;
    private Rigidbody rb;

    private void Start()
    {
        oceanManager = FindObjectOfType<OceanManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        floatingUnderWater = 0;
        for (int i = 0; i < floatings.Length; i++)
        {
            float diff = floatings[i].position.y - oceanManager.WaterHeightAtPosition(floatings[i].position);

            if (diff < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floatings[i].position, ForceMode.Force);
                floatingUnderWater++;
                if (!underWater)
                {
                    underWater = true;
                    SwitchState(true);
                }
            }
        }

        if (underWater && floatingUnderWater == 0)
        {
            underWater = false;
            SwitchState(false);
        }
    }

    private void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }

}
