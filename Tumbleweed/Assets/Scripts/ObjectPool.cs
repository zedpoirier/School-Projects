// Author: Zed Poirier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Instantiates a pool of prefabs that can be accesed externally.
/// </summary>
public class ObjectPool : MonoBehaviour
{

    [Header("Pool Parameters")]
    [Tooltip("Max item count in this pool")]    public int poolCount = 20;
    [Tooltip("Max lifetime for each item")]     public float lifetime = 30f;
    [Tooltip("Prefab for spawning")]            public GameObject item;
    [Tooltip("List to contain the items")]      private List<GameObject> itemPool = new List<GameObject>();

    void Start() {
        if (item.GetComponent<Lifetime>() == null) {
            item.AddComponent<Lifetime>();
        }
        for (int i = 0; i < poolCount; i++) {
            itemPool.Add(Instantiate(item, transform));
            itemPool[i].gameObject.SetActive(false);
        }
    }

    // Spawn
    /// <summary> Loops through the list of items to find the first unactive item
    /// and activate it. Will do nothing if it finds no unactive items. </summary>
    /// <param name="spawnPoint">Spawn location for the item.</param>
    /// <returns>The first active item in the pool.</returns>
    public GameObject Spawn(Vector3 spawnPoint)
    {
        GameObject item;
        for (int i = 0; i < poolCount; i++) {
            if (!itemPool[i].gameObject.activeSelf) {
                item = itemPool[i];
                item.SetActive(true);
                item.transform.position = spawnPoint;
                item.GetComponent<Lifetime>().lifetime = lifetime;
                return item;
            }
        }
        Debug.LogError("You need more items in this pool. Increase the poolCount variable.", this);
        return null;
    }
}
