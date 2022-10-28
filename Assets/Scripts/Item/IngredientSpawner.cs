using Level;
using UnityEngine;
using System.Collections.Generic;
namespace Item
{
    public class IngredientSpawner : MonoBehaviour
    {
        [Header("Items(POOLING)")]
        [SerializeField] private List<GameObject> spawnObjects;

        private LevelFacade levelFacade;
        private void Start()
        {
            levelFacade = LevelFacade.instance;
            SpawnAllIngredient();
        }

        private void SpawnAllIngredient()
        {
            foreach (var objects in spawnObjects)
            {
                var randomSeedX = Random.Range((-levelFacade.BorderX()), levelFacade.BorderX());
                var randomSeedY = Random.Range(-levelFacade.BorderY(), levelFacade.BorderY());
                var spawnLocation = new Vector3(randomSeedX, randomSeedY, 2);
                objects.SetActive(true);
                objects.transform.position = spawnLocation;
            }
        }
    }
}
