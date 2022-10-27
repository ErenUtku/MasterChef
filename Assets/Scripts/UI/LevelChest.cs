using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelChest : MonoBehaviour
    {
        [SerializeField] private MenuUIManager menuUIManager;
        [SerializeField] private Button levelChestBtn;

        private void Start()
        {
            //Button Configuration
            levelChestBtn.onClick.AddListener(GetCoin);

            if (LevelChestManager.instance.chestActive)
            {
                levelChestBtn.interactable = true;
            }
        }

        private void GetCoin()
        {
            UIManager.instance.AddCoin(15);
            levelChestBtn.interactable = false;
            menuUIManager.UpdateCurrency();
            Storage.PlayerPrefsController.SetLevelChest(0);
        }
    }
}
