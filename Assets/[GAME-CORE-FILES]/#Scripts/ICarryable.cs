using UnityEngine;

public interface ICarryable
{
    public Rigidbody m_Rigidbody { get; set; }
    public Transform m_Transform { get; set; }
    public Collider m_Collider { get; set; }
    public bool m_IsCarrying { get; set; }

    public void Carry();
    public void Drop(Vector3 DropDirection, float DropForce);
}
