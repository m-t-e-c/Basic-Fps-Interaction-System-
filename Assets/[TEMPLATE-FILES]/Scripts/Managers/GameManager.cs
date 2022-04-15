using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("=== GAME SETTINGS ===")]
    [Range(30f,120f)]
    [SerializeField] private int GAME_TARGET_FPS = 60;

    /* References */
    public GameState gameState;
    [SerializeField] private bool useJoystick;
    [HideInInspector] public Joystick joystick;
    private InputManager inputManager;

    #region Unity Methods
    private void Awake()
    {
        Application.targetFrameRate = GAME_TARGET_FPS;
        EventManager.OnGameManagerCreated?.Invoke(this);
    }
    private void Start()
    {
        EventManager.OnGameStateChanged?.Invoke(GameState.IN_MENU);
    }
    #endregion

    #region Action Methods
    private void OnAnyKeyPressed()
    {
        if(gameState == GameState.IN_MENU) EventManager.OnGameStateChanged?.Invoke(GameState.STARTED);
    }
    private void OnGameStateChanged(GameState state){ gameState = state; }
    private void OnInputManagerCreated(InputManager x) => inputManager = x;
    private void OnJoystickCreated(Joystick x) => joystick = x;


    private void OnEnable()
    {
        EventManager.OnGameStateChanged += OnGameStateChanged;
        EventManager.OnInputManagerCreated += OnInputManagerCreated;
        EventManager.OnJoystickCreated += OnJoystickCreated;
        EventManager.OnAnyKeyPressed += OnAnyKeyPressed;
    }

    private void OnDisable()
    {
        EventManager.OnGameStateChanged -= OnGameStateChanged;
        EventManager.OnInputManagerCreated -= OnInputManagerCreated;
        EventManager.OnJoystickCreated -= OnJoystickCreated;
        EventManager.OnAnyKeyPressed -= OnAnyKeyPressed;
    }
    #endregion
}
