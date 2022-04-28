using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D m_CursorTexture;
    [SerializeField] protected bool m_UseCustomCursor;
    [SerializeField] private bool m_HideCursor;

    private void Awake()
    {
        if (m_UseCustomCursor && m_CursorTexture)
            Cursor.SetCursor(m_CursorTexture, Vector2.zero, CursorMode.ForceSoftware);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = m_HideCursor ? false : true;
    }
}