using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Joystick
    public static Action<Joystick> OnJoystickCreated;

    // Game Manager
    public static Action<GameManager> OnGameManagerCreated;
    public static Action<GameState> OnGameStateChanged;

    // Player
    public static Action<Player> OnPlayerCreated;

    // Input Manager
    public static Action<InputManager> OnInputManagerCreated;
    public static Action OnAnyKeyPressed;

    // Level Manager
    public static Action<LevelManager> OnLevelManagerCreated;

    // UI Manager
    public static Action<UIManager> OnUIManagerCreated;
    public static Action<string> OnUIButtonClicked;

    // Fishing Area
    public static Action<FishingArea> OnFishCatched;
}
