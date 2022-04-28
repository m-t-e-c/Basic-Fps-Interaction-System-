using UnityEngine;

public class TempBox : MonoBehaviour, IInteractible, ICarryable
{
    public Vector3 m_StartPos { get; set; }
    public float m_InteractionHoldingTime { get; set; }
    public bool m_Interacted { get; set; }
    public Rigidbody m_Rigidbody { get; set; }
    public Transform m_Transform { get; set; }
    public Collider m_Collider { get; set; }
    public bool m_IsCarrying { get; set; }

    private void Start()
    {
        m_InteractionHoldingTime = 2f;
        m_StartPos = transform.position;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Transform = transform;
        m_Collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        m_Interacted = enabled;
        Player_Carry.onCarryObject?.Invoke(this);
    }

    public void Carry()
    {
        m_Rigidbody.isKinematic = true;
        m_Collider.enabled = false;
        m_IsCarrying = true;
    }

    public void Drop(Vector3 DropDir, float DropForce)
    {
        m_Rigidbody.isKinematic = false;
        m_Collider.enabled = true;
        m_IsCarrying = false;
        m_Interacted = false;
        m_Rigidbody.AddForce(DropDir * DropForce);
    }
}
