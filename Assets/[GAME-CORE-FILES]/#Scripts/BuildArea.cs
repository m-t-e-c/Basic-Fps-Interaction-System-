using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> unlockableParts = new List<GameObject>();
    [SerializeField] private List<int> unlockValues = new List<int>();
    [SerializeField] private GameObject ghostObject;
    [SerializeField] private TextMeshProUGUI requiredCollectibleText;
    [SerializeField] private bool isBuilded;
    [SerializeField] private float fillDistance = 2f;
    [SerializeField] private float fillTime;
    [SerializeField] private int requiredCollectibleAmount = 20;
    [SerializeField] private int collected;

    public int unlockIndex;
    private float waitTime;

    private Player player;

    private void Update()
    {
        requiredCollectibleText.SetText(collected + "/" + requiredCollectibleAmount);
        IsPlayerInsideBuildZone();
        BuildCheck();
    }

    private bool IsPlayerInsideBuildZone()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < fillDistance) return true;
        return false;
    }

    private void BuildCheck()
    {
        if (isBuilded) return;
        if (!IsPlayerInsideBuildZone()) return;
        waitTime += Time.deltaTime;
        if(waitTime >= fillTime)
        {
            waitTime = 0;
            if (player.GetCollectiblesCount() == 0) return;
            player.RemoveCollectible();
            collected++;
            UnlockCheck();
            if (collected == requiredCollectibleAmount) isBuilded = true;
        }
    }

    private void UnlockCheck()
    {
        if(unlockValues.Contains(collected))
        {
            unlockableParts[unlockIndex].SetActive(true);
            ghostObject.SetActive(false);
            if (unlockIndex == unlockValues.Count - 1) return;
            unlockIndex++;
        }
    }

    #region Action Methods
    private void OnPlayerCreated(Player x) => player = x;

    private void OnEnable()
    {
        EventManager.OnPlayerCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerCreated -= OnPlayerCreated;
    }

    #endregion
}
