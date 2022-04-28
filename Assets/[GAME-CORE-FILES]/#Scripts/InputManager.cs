using UnityEngine;

public class InputManager : MonoBehaviour
{
   public float m_MouseX { get; private set; }
   public float m_MouseY { get; private set; }
    public float m_Horizontal { get; private set; }
    public float m_Vertical { get; private set; }

    private void Update()
    {
        InputListener();
    }

    private void InputListener()
    {
        m_MouseX = Input.GetAxis("Mouse X");
        m_MouseY = Input.GetAxis("Mouse Y");

        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");
    }
}
