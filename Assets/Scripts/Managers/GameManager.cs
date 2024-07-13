using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UI;
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
    
    private AlertManager alert;
    public static AlertManager Alerting { get { return Instance.alert; } }

    public PatternReader _patternReader;
    public LevelMode _levelMode;

    public static Action Success = null;
    public static Action Fail = null;
    public static Action GameClear = null;
    public static Action GameOver = null;

    public static Action<float> BgmVolume = null;
    public static Action<float> EffectVolume = null;
    
    public static Dictionary<LevelMode, List<MusicData>> MusicPattern { get; private set; }= new Dictionary<LevelMode, List<MusicData>>();

    string _sceneShootingName = "Scene_Shooting";

    int _totalScore;
    int _successScore;

    public static int _playerLife = 3;
    public int _playerMaxLife = 3;

    private void Awake()
    {
        base.Awake();
        alert = new AlertManager();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //_soundManager = SoundManager.Instance;

        _successScore = 100;
    }

    void Update()
    {
        // TEST: Scene load 
        if (Input.GetKeyDown(KeyCode.V))
        {
            print("Scene loaded from GameManager");
            SceneManager.LoadScene(_sceneShootingName);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _sceneShootingName)
        {
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
            Cursor.visible = true;
            _poolManager = null;
            
        }
        
        if(scene.name == "Lobby")
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
                    print("GAME OVER");
                    GameOver?.Invoke();
                }
                print("FAIL SCORE ADDED");
                Success?.Invoke();
                break;

            case EHitState.Success:
                _totalScore += _successScore;
                print("SUCCESS SCORE ADDED");
                Fail?.Invoke();
                break;
            default:
                break;
        }

    }
}
