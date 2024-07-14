using System.Collections;
using System.Collections.Generic;
using System.Net;
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

        private float _decisionTime = 2.0f;

        public static int Combo = 0;
        public static int MaxCombo = 0;
        public void Awake()
        {
            timer = gameObject.GetComponent<Timer>();
            
            GameManager.Success -= ComboUp;
            GameManager.Success += ComboUp;
            GameManager.Fail -= ComboFail;
            GameManager.Fail += ComboFail;
        }

        public void OnEnable()
        {
            _musicPattern = GameManager.MusicPattern[GameManager._levelMode];

            timer.RestartTimer();
            
            GameManager._playerLife = 3;
            Combo = 0;
            MaxCombo = 0;
            musicIndex = 0;
            
            BGMManager.Instance.PlayBGM(EBGMType.StarBubble);
        }
    
        void FixedUpdate()
        {
            GameManager._playerLife = 3;
            if (musicIndex >= _musicPattern.Count)
            {
                if (SoundManager.Instance.IsPlaying(ESoundType.Cheers)) return;
                
                SoundManager.Instance.PlaySound(ESoundType.Cheers);
                StartCoroutine("CheckEnd");
                return;
            }

            if (_musicPattern[musicIndex].time - _decisionTime <= timer.currentTime)
            {
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
            switch(GameManager._levelMode) 
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

        IEnumerator CheckEnd()
        {
            yield return null;
            musicIndex = 0;
            while (true)
            {
                if (!SoundManager.Instance.IsPlaying(ESoundType.Cheers))
                {
                    break;
                }

                yield return null;
            }
            SoundManager.Instance.PlaySound(ESoundType.ScorePopup);
            GameManager.GameClear?.Invoke();
        }

    }

}