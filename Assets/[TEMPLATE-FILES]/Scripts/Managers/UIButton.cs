using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private string function_string;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => EventManager.OnUIButtonClicked?.Invoke(function_string));
    }
}
