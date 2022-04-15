using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public UIState uiState;

    public void ShowUI() => gameObject.SetActive(true);
    public void HideUI() => gameObject.SetActive(false);
}
