using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    //MusicGame과 Timer은 한 게임 오브젝트에 존재해야 함
    public class MusicGame : MonoBehaviour
    {
        private List<MusicData> _musicPattern;
        private Timer timer;
        private int musicIndex = 0;

        private LevelMode _currentLevel;
        private float _decisionTime = 2.0f;

        public static int Combo = 0;
        public static int MaxCombo = 0;
        public void Awake()
        {
            _musicPattern = GameManager.MusicPattern[LevelMode.Normal];
            timer = gameObject.GetComponent<Timer>();
            timer.RestartTimer();

            GameManager._playerLife = 3;
            Combo = 0;
            MaxCombo = 0;
            
            GameManager.Success -= ComboUp;
            GameManager.Success += ComboUp;
            GameManager.Fail -= ComboFail;
            GameManager.Fail += ComboFail;
            
            Debug.Log(_musicPattern.Count);
            BGMManager.Instance.PlayBGM(EBGMType.StarBubble);
        }
    
        void FixedUpdate()
        {
            if (musicIndex >= _musicPattern.Count)
            {
                GameManager.GameClear.Invoke();
                return;
            }

            //Debug.Log($"{_musicPattern[musicIndex].time}, {timer.currentTime}");

            if (_musicPattern[musicIndex].time - _decisionTime <= timer.currentTime)
            {
                GameManager.Alerting.Alert(musicIndex.ToString(), AlertMode.Pop, 0.5f);

                int spawnPoint = _musicPattern[musicIndex].spawnPoint;

                GameObject spawner = GameManager.PoolManager.GetSpawner(spawnPoint);
                spawner.GetComponent<Spawner>().ReleaseEnemy(_musicPattern[musicIndex].speed, _decisionTime, spawnPoint);

                musicIndex++;
            }
        }

        public void ComboUp()
        {
            Combo++;
        }

        public void ComboFail()
        {
            if (Combo > MaxCombo)
                MaxCombo = Combo;
            Combo = 0;
        }

        public void DecideDecisionTime()
        {
            switch(_currentLevel) 
            {
                case LevelMode.Easy:
                    _decisionTime = 2.5f;
                    break;
                case LevelMode.Normal:
                    _decisionTime = 2f;
                    break;
                case LevelMode.Hard:
                    _decisionTime = 1.5f;
                    break;

            }
        }
    }

}