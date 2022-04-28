using UnityEngine;

public interface IInteractible
{
    public Rigidbody m_Rigidbody { get; set; }
    public Vector3 m_StartPos { get; set; }
    public bool m_Interacted { get; set; }

    public void Interact();
}