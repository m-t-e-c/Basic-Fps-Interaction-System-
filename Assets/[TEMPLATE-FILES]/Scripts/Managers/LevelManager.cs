using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels = new List<Level>();
    [SerializeField] private int levelIndex = 0;


    private void Awake()
    {
        EventManager.OnLevelManagerCreated?.Invoke(this);
    }

    [ContextMenu("Load Next Level")]
    public void NextLevel()
    {
        if(levels.Count > 0)
        {
            levels[levelIndex].Load();
        }
    }
}
