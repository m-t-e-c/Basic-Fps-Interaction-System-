using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public bool m_Initialized { get; private set; }


    [SerializeField]
    protected CharacterController m_CharacterController;

    [SerializeField]
    protected InputManager inputManager;

    [SerializeField]
    protected float m_GroundHeight;

    [Header("Gravity Settings")]
    
    [SerializeField] 
    protected float m_GravityAmount = -10.0f;
    [SerializeField] 

    private void Awake()
    {
        m_Initialized = true;
    }
}
