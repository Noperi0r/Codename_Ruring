using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public ObjectPoolManager _poolManager;
    public static ObjectPoolManager PoolManager
    {
        get { return Instance._poolManager; }
    }

    SoundManager _soundManager;
    
    public PatternReader _patternReader;
    public static LevelMode _levelMode;

    public static Action Success = null;
    public static Action Fail = null;
    public static Action GameClear = null;
    public static Action GameOver = null;
    public static Action<float> BgmVolume = null;
    public static Action<float> EffectVolume = null;
    
    public static Dictionary<LevelMode, List<MusicData>> MusicPattern { get; private set; }= new Dictionary<LevelMode, List<MusicData>>();

    string _introSceneName = "IntroScene";
    string _startSceneName = "StartScene";
    string _lobbySceneName = "LobbyScene";
    string _mainSceneName = "MainGame";

    int _totalScore;
    int _successScore;

    public static int _maxScore_Easy;
    public static int _maxScore_Normal;
    public static int _maxScore_Hard;

    public static int _playerLife = 3;
    public int _playerMaxLife = 3;

    private void Awake()
    {
        base.Awake();

        if (!BGMManager.Instance.IsPlaying(EBGMType.BGM))
            BGMManager.Instance.PlayBGM(EBGMType.BGM);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //_soundManager = SoundManager.Instance;

        _successScore = 100;

        GameClear -= ComputeBestScore;
        GameClear += ComputeBestScore;
    }

    void Update()
    {
        // TEST: Scene load
/*        if (Input.GetKeyDown(KeyCode.V))
        {
            SoundManager.Instance.PlaySound(ESoundType.ClickButton);
*//*            print("Scene loaded from GameManager");
            SceneManager.LoadScene(_mainSceneName);*//*
        }*/
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _mainSceneName)
        {
            BGMManager.Instance.StopBGM(EBGMType.BGM);
            BGMManager.Instance.PlayBGM(EBGMType.StarBubble);

            Cursor.visible = false;
            _playerLife = _playerMaxLife;

            _poolManager = FindObjectOfType<ObjectPoolManager>();

            if (_poolManager)
            {
                for(int i=0; i<_poolManager._enemies.Length; ++i)
                {
                    _poolManager._enemies[i].OnDecided.AddListener(HandleScore);
                }
                _poolManager.PoolsOff();
            }
        }
        else
        {
            BGMManager.Instance.StopBGM(EBGMType.StarBubble);

            if (!BGMManager.Instance.IsPlaying(EBGMType.BGM))
                BGMManager.Instance.PlayBGM(EBGMType.BGM);

            Cursor.visible = true;
            _poolManager = null;
        }

        if(scene.name == _lobbySceneName)
        {
            _patternReader = FindObjectOfType<PatternReader>(); 
        }
    }

    void HandleScore(EHitState hitState)
    {
        switch (hitState)
        {
            case EHitState.Fail:
                if (--_playerLife <= 0)
                {
                    //print("GAME OVER");
                    GameOver?.Invoke();
                }
                //print("FAIL SCORE ADDED");
                Success?.Invoke();
                break;

            case EHitState.Success:
                _totalScore += _successScore;
                //print("SUCCESS SCORE ADDED");
                Fail?.Invoke();
                break;
            default:
                break;
        }

    }

    void ComputeBestScore()
    {
        switch (_levelMode)
        {
            case LevelMode.Easy:
                _maxScore_Easy = _totalScore > _maxScore_Easy ? _totalScore : _maxScore_Easy;
                break;
            case LevelMode.Normal:
                _maxScore_Normal = _totalScore > _maxScore_Normal ? _totalScore : _maxScore_Normal;
                break;
            case LevelMode.Hard:
                _maxScore_Hard = _totalScore > _maxScore_Hard ? _totalScore : _maxScore_Hard;
                break;
        }

    }
}
