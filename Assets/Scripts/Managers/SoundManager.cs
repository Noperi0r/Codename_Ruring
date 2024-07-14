using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoSingleton<SoundManager>
{
    AudioClip[] _soundEffects;
    GameObject[] _speakers;

    string _effectPath = "Sound/Effect/";
    string _prefabPath = "Prefab/Speaker/SoundManager";

    private float _effectVolume = 0.6f;
    
    void Awake()
    {
        _soundEffects = Resources.LoadAll<AudioClip>(_effectPath);

        _speakers = new GameObject[_soundEffects.Length];
        for(int i=0; i<_soundEffects.Length; ++i)
        {
            _speakers[i] = Instantiate(Resources.Load<GameObject>(_prefabPath), transform);
            _speakers[i].GetComponent<AudioSource>().clip = _soundEffects[i];
                        
            AudioSource source = _speakers[i].GetComponent<AudioSource>();
            source.volume = _effectVolume; 
            GameManager.EffectVolume += (volume) =>
            {
                SetVolume(source, volume);
            };
        }
    }

    public void SetVolume(AudioSource source, float volume)
    {
        source.volume = volume;
    }

    public void PlaySound(ESoundType sound)
    {
        Debug.Log("Playsound ok");
        _speakers[(int)sound].GetComponent<AudioSource>().Play();
    }
    
    public void PlaySound(ESoundType sound, float volume)
    {
        AudioSource source = _speakers[(int)sound].GetComponent<AudioSource>();
        source.volume = volume;
        source.Play();
    }

    public void StopSound(ESoundType sound)
    {
        _speakers[(int)sound].GetComponent<AudioSource>().Stop();
    }

    public bool IsPlaying(ESoundType sound)
    {
        if (_speakers[(int)sound].GetComponent<AudioSource>().isPlaying)
        {
            return true;
        }
        return false;
    }
}