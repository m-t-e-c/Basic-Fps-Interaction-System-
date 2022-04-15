using UnityEngine;

[CreateAssetMenu(menuName = "TEMPLATE ASSETS/ New Level", fileName = "Level")]
public class Level : ScriptableObject
{
    [Header("=== Level Settings ===")]
    public GameObject world;
    public string levelName;
    public int levelIndex;

    [Header("=== Skybox Settings ===")]
    public Material skybox;
    public Color32 skyboxColor = Color.white;
    public Color32 fogColor = Color.grey;
    public bool useFog = false;

    public void Load()
    {
        if (world != null) Instantiate(world);
    }
}
