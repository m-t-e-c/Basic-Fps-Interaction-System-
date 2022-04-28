using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player_Interaction : MonoBehaviour {

    private Camera cam;
    private Ray interactionRay;

    [SerializeField]
    [Range(1f, 20f)]
    private float m_InteractionDistance;

    [SerializeField]
    private LayerMask m_InteractionLayer;

    [SerializeField] 
    private GameObject m_InteractionUI;

    [SerializeField] 
    private Image m_InteractionFill;

    [SerializeField]
    private bool m_InteractibleObjectExist;

    [SerializeField]
    private float m_InteractionHoldingTime;

    private float m_InteractionWaitTime;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        interactionRay = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(interactionRay.origin, interactionRay.direction, out RaycastHit interactionHit, m_InteractionDistance, m_InteractionLayer);
        if (interactionHit.transform && interactionHit.transform.TryGetComponent(out IInteractible interactible))
        {
            m_InteractibleObjectExist = true;
            m_InteractionUI.transform.DOScale(Vector3.one, 0.25f);
            m_InteractionFill.fillAmount = m_InteractionWaitTime.Remap01(0, m_InteractionHoldingTime);
            if (Input.GetKey(KeyCode.E) && interactible.m_Interacted == false)
            {
                m_InteractionWaitTime += Time.deltaTime;
                if (m_InteractionWaitTime >= m_InteractionHoldingTime)
                {
                    interactible.Interact();
                    m_InteractionUI.transform.DOScale(Vector3.zero, 0.25f);
                }
            }
            else m_InteractionWaitTime = 0;
        }
        else
        {
            m_InteractibleObjectExist = false;
            m_InteractionUI.transform.DOScale(Vector3.zero, 0.25f);
        }
    }
}
