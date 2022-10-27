using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Storage;
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
        
        var ChestIndex = PlayerPrefsController.GetLevelChest();
        if (ChestIndex == 1)
        {
            ActiveChest(true);
        }
        else
        {
            ActiveChest(false);
        }

    }
   
    public void ActiveChest(bool value)
    {
        chestActive = value;
    }
}
