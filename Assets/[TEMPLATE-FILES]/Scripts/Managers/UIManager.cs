using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [Header("=== UI Button Functions ===")]
    [SerializeField] private List<UICanvas> uiList = new List<UICanvas>();
    public UIState uiState;

    private void Awake()
    {
        EventManager.OnUIManagerCreated?.Invoke(this);
    }

    public void OpenUI(UIState state, bool closeOthers = true)
    {
        foreach (UICanvas uiCanvas in uiList)
        {
            if (uiCanvas.uiState == state) uiCanvas.ShowUI();
            else
            {
                if(closeOthers) uiCanvas.HideUI();
            }
        }
    }

    public void ExecuteFunction(string func)
    {
        string nameFilter = "_" + func.ToUpper() + "_";
        switch (nameFilter)
        {
            case "_PAUSE_GAME_":
                OpenPause();
                break;
            case "_UNPAUSE_GAME_":
                ClosePause();
                break;
            case "_RESTART_GAME_":
                break;
            case "_EXIT_GAME_":
                break;
        }
    }

    #region UIButton Methods

    private void OpenPause()
    {
        EventManager.OnGameStateChanged?.Invoke(GameState.PAUSED);
    }
    private void ClosePause()
    {
        EventManager.OnGameStateChanged?.Invoke(GameState.STARTED);
    }

    #endregion


    #region Action Methods 
    private void OnUIButtonClicked(string button_function)
    {
        ExecuteFunction(button_function);
    }
    private void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.IN_MENU:
                OpenUI(UIState.MAIN_MENU_SCREEN);
                break;
            case GameState.STARTED:
                OpenUI(UIState.GAMEPLAY_SCREEN);
                break;
            case GameState.PAUSED:
                OpenUI(UIState.PAUSE_MENU_SCREEN, false);
                break;
            case GameState.FINISHED_WIN:
                OpenUI(UIState.WIN_SCREEN);
                break;
            case GameState.FINISHED_LOSE:
                OpenUI(UIState.LOSE_SCREEN);
                break;
        }
    }

    private void OnEnable()
    {
        EventManager.OnGameStateChanged += OnGameStateChanged;
        EventManager.OnUIButtonClicked += OnUIButtonClicked;
    }

    private void OnDisable()
    {
        EventManager.OnGameStateChanged -= OnGameStateChanged;
        EventManager.OnUIButtonClicked -= OnUIButtonClicked;
    }
    #endregion
}