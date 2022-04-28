using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform m_Body;
    [SerializeField] private InputManager inputManager;

    [SerializeField]
    private float m_yLookSens = 2f;

    [SerializeField]
    private bool m_useReverseLook;

    [SerializeField]
    [Range(0.01f, 1.0f)]
    private float m_yLerpAmount = 1f;
    float yAxis = 0;
    float xAxis = 0;

    private void LateUpdate()
    {
        LookAround();
    }

    private void LookAround()
    {
        // Head Rotation.
        float mouseY = m_useReverseLook ? inputManager.m_MouseY : -inputManager.m_MouseY;
        yAxis += mouseY * m_yLookSens * 100f * Time.deltaTime;
        yAxis = Mathf.Clamp(yAxis, -60f, 90f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(yAxis, 0, 0), m_yLerpAmount);

        // Body Rotation.
        xAxis += inputManager.m_MouseX * m_yLookSens * 100f * Time.deltaTime;
        m_Body.rotation = Quaternion.Lerp(m_Body.rotation, Quaternion.Euler(0, xAxis, 0), m_yLerpAmount);
    }
}
