using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropFood : MonoBehaviour
{
    // This empty list will be populated in the enemy prefab to have the food options.
    public GameObject[] foods;
    // ayy as the form of a decimal what are the odds of us dropping a food item.
    [SerializeField] float odds = 0.2f;

    private void OnDestroy()
    {
        float Rand = Random.value;
        if (Rand < odds)
        {
            // Create a singular food object at the dying position if we win the spin lottery
            Instantiate(foods[Random.Range(0, foods.Length - 1)], transform.position, Quaternion.identity);
        }
    }
}
