using System.Collections.Generic;
using _MobControl.Scripts.Controller;
using UnityEngine;

namespace _MobControl.Scripts.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public int GetLevel => LevelNumber;
        public static LevelManager Instance { get; private set; } 
        private int LevelNumber
        {
            get => PlayerPrefs.GetInt("level", 0);
            set => PlayerPrefs.SetInt("level", LevelNumber + 1 >= levelList.Count ? 0 : value);
        } 
        
        [SerializeField] private List<LevelController> levelList;

        private LevelController _currentLevel;
        
        private void Start()
        {
            Instance ??= this;
            Load();
        }

        private void Save(bool success)
        {
            if (success)
                LevelNumber += 1;
        }

        public void Load()
        {
            if (_currentLevel is not null)
                Destroy(_currentLevel.gameObject);

            _currentLevel = Instantiate(levelList[LevelNumber]);
            _currentLevel.Initialize();
        }

        public void FinishGame(bool success)
        {
            Save(success);
        }
    }
}