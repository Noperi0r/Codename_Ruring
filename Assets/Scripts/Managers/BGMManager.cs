using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BGMManager : MonoBehaviour
{
    AudioClip[] _soundEffects;
    GameObject[] _speakers;

    string _bgmPath = "Sound/BGM/";
    string _prefabPath = "Prefab/Speaker/SoundManager";

    float _bgmVolume = 0.6f;

    // Create singleton instance
    public static BGMManager Instance
    {
        get
        {
            if (!_instance)
            {
                var singleton = new GameObject("BGMManager", typeof(BGMManager));
                _instance = singleton.GetComponent<BGMManager>();
                DontDestroyOnLoad(singleton);
            }

            return _instance;
        }
    }
    static BGMManager _instance;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _soundEffects = Resources.LoadAll<AudioClip>(_bgmPath);

        _speakers = new GameObject[_soundEffects.Length];
        for (int i = 0; i < _soundEffects.Length; ++i)
        {
            _speakers[i] = Instantiate(Resources.Load<GameObject>(_prefabPath), transform);
            
            AudioSource source = _speakers[i].GetComponent<AudioSource>();
            source.clip = _soundEffects[i];
            source.loop = true;
            source.volume = _bgmVolume; 
            GameManager.BgmVolume += (volume) =>
            {
                SetVolumn(source, volume);
                Debug.Log($"{volume}으로바꿈...");
            };
        }
    }
    
    public void SetVolumn(AudioSource source, float volume)
    {
        source.volume = volume;
    }

    public void PlayBGM(EBGMType sound)
    {
        _speakers[(int)sound].GetComponent<AudioSource>().Play();
    }

    public void StopBGM(EBGMType sound)
    {
        _speakers[(int)sound].GetComponent<AudioSource>().Stop();
    }

    public bool IsPlaying(EBGMType sound)
    {
        if (_speakers[(int)sound].GetComponent<AudioSource>().isPlaying)
        {
            return true;
        }
        return false;
    }

}
