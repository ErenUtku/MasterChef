using Storage;
using UnityEngine;
using Data;

namespace Controllers
{
    public class LevelManager : MonoBehaviour
    {
        #region DELEGATE

        public delegate void LevelLoadHandler(LevelFacade levelData);

        public delegate void LevelStartHandler(LevelFacade levelData);

        public delegate void LevelStageCompleteHandler(LevelFacade levelData, int stageIndex = 0);

        public delegate void LevelCompleteHandler(LevelFacade levelData);

        public delegate void LevelFailHandler(LevelFacade levelData);

        #endregion

        #region EVENTS
        
        public static LevelLoadHandler OnLevelLoad;
        
        public static LevelStartHandler OnLevelStart;
        
        public static LevelStageCompleteHandler OnLevelStageComplete;
        
        public static LevelCompleteHandler OnLevelComplete;
        
        public static LevelFailHandler OnLevelFail;

        #endregion

        #region PUBLIC FIELDS / PROPS

        public static LevelManager Instance { get; private set; }

        #endregion

        #region SERIALIZE PRIVATE FIELDS
        
        [SerializeField] private LevelSource levelSource;

        //container
        [SerializeField] private GameObject levelSpawnPoint;
        
        [SerializeField] private int loopLevelsStartIndex = 1;
        
        [SerializeField] private bool loopLevelGetRandom = true;

        #endregion

        #region PRIVATE FIELDS

        private GameObject _activeLevel;

        #endregion

        #region PRIVATE METHODS

        private void CheckRepeatLevelIndex()
        {
            if (loopLevelsStartIndex < levelSource.levelData.Length) return;
            loopLevelsStartIndex = 0;
        }

        private GameObject GetLevel()
        {
            if (PlayerPrefsController.GetLevelIndex() >= levelSource.levelData.Length)
            {
                if (loopLevelGetRandom)
                {
                    var levelIndex = Random.Range(loopLevelsStartIndex, levelSource.levelData.Length - 1);
                    PlayerPrefsController.SetLevelIndex(levelIndex);
                }
            }

            var level = levelSource.levelData[PlayerPrefsController.GetLevelIndex()];

            var levelData = level.GetComponent<LevelFacade>();

            levelData.levelIndex = PlayerPrefsController.GetLevelIndex();
            levelData.levelNumber = PlayerPrefsController.GetLevelNumber();

            return level;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        ///     S?radaki level'? y�kleyen metod
        /// </summary>
        public void LevelLoad()
        {
            _activeLevel = Instantiate(GetLevel(), levelSpawnPoint.transform, false);
            OnLevelLoad?.Invoke(_activeLevel.GetComponent<LevelFacade>());

            UIManager.instance.SetReceiptUI();
        }

        /// <summary>
        ///     Son y�klenen level'? ba?latan method
        /// </summary>
        public void LevelStart()
        {
            OnLevelStart?.Invoke(_activeLevel.GetComponent<LevelFacade>());
        }

        /// <summary>
        /// Y�klenen level i�erisinde stage'ler var ise her stage tamamland???nda �a?r?lacak methods
        /// </summary>
        public void LevelStageComplete(int stageIndex = 0)
        {
            OnLevelStageComplete?.Invoke(_activeLevel.GetComponent<LevelFacade>(), stageIndex);
        }

        /// <summary>
        ///     Oynanan level tamamland???nda �a?r?lacak olan methods
        /// </summary>
        public void LevelComplete()
        {
            // Sonraki level index de?eri atan?yor
            PlayerPrefsController.SetLevelIndex(PlayerPrefsController.GetLevelIndex() + 1);

            // Sonraki level numaras? atan?yor
            PlayerPrefsController.SetLevelNumber(PlayerPrefsController.GetLevelNumber() + 1);

            OnLevelComplete?.Invoke(_activeLevel.GetComponent<LevelFacade>());

            StoreReceiptData.instance.BringReceiptDataAndDelete();

            Application.LoadLevel(Application.loadedLevel);
        }

        /// <summary>
        ///     Oynanan level ba?ar?s?z oldu?unda �a?r?lacak olan method
        /// </summary>
        public void LevelFail()
        {
            OnLevelFail?.Invoke(_activeLevel.GetComponent<LevelFacade>());
        }

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            CheckRepeatLevelIndex();
            Instance = this;
        }

        private void Start() => LevelLoad();

        #endregion
    }
}