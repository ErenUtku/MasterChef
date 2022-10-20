using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private SpawnableIngredient[] spawnableIngredient;

    private LevelFacade levelfacade;
    private void Start()
    {
        levelfacade = LevelFacade.instance;
        SpawnAllIngredient();
    }

    private void SpawnAllIngredient()
    {
        foreach (var ingredient in spawnableIngredient)
        {
            for (int i = 0; i < ingredient.spawnAmount; i++)
            {
                var RandomSeedX = Random.Range((-levelfacade.BorderX()), levelfacade.BorderX());
                var RandomSeedY = Random.Range(-levelfacade.BorderY(),levelfacade.BorderY());
                var SpawnLocation = new Vector3(RandomSeedX, RandomSeedY, 2);
                var SpawnedIngredient = Instantiate(ingredient.spawnIngredient, SpawnLocation, Quaternion.identity);
                SpawnedIngredient.transform.parent = this.transform;
            }
        }
    }
}
