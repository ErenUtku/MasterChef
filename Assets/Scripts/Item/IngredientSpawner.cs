using Level;
using UnityEngine;

namespace Item
{
    public class IngredientSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnableIngredient[] spawnableIngredient;

        private LevelFacade levelFacade;
        private void Start()
        {
            levelFacade = LevelFacade.instance;
            SpawnAllIngredient();
        }

        private void SpawnAllIngredient()
        {
            foreach (var ingredient in spawnableIngredient)
            {
                for (int i = 0; i < ingredient.spawnAmount; i++)
                {
                    var randomSeedX = Random.Range((-levelFacade.BorderX()), levelFacade.BorderX());
                    var randomSeedY = Random.Range(-levelFacade.BorderY(),levelFacade.BorderY());
                    var spawnLocation = new Vector3(randomSeedX, randomSeedY, 2);
                    var spawnedIngredient = Instantiate(ingredient.spawnIngredient, spawnLocation, Quaternion.identity);
                    spawnedIngredient.transform.parent = this.transform;
                }
            }
        }
    }
}
