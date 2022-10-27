using Storage;
using UnityEngine;

namespace UI
{
    public class LevelChestManager : MonoBehaviour
    {
        public bool chestActive;
  
        public static LevelChestManager instance;
        private void Awake()
        {

            #region Singleton
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
            }
            #endregion
        
            var chestIndex = PlayerPrefsController.GetLevelChest();
            ActiveChest(chestIndex == 1);
        }

        private void ActiveChest(bool value)
        {
            chestActive = value;
        }
    }
}
