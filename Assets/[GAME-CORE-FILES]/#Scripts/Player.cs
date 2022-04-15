using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform collectZone;
    [SerializeField] private List<GameObject> collectibles = new List<GameObject>();
    [SerializeField] private int carryLimit;
    private Animator animator;

    private void Awake()
    {
        EventManager.OnPlayerCreated?.Invoke(this);
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (collectibles.Count > 0) animator.SetBool("Carry", true);
        else animator.SetBool("Carry", false);
    }


    private void AddCollectible(GameObject obj)
    {
        if (collectibles.Contains(obj)) return;
        if (collectibles.Count == carryLimit) return;
        Quaternion rot;
        float ySpacing = collectibles.Count * 0.2f;
        Vector3 spawnPos = new Vector3(collectZone.position.x,collectZone.position.y + ySpacing, collectZone.position.z);
        GameObject collectible = Instantiate(obj, spawnPos, transform.rotation);
        collectible.transform.parent = collectZone;
        collectibles.Add(collectible);
        Destroy(obj);
    }

    public int GetCollectiblesCount()
    {
        return collectibles.Count;
    }

    public void RemoveCollectible()
    {
        if (collectibles.Count == 0) return;
        GameObject obj = collectibles[collectibles.Count - 1];
        collectibles.Remove(obj);
        Destroy(obj);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            AddCollectible(other.gameObject);
        }
    }
}
