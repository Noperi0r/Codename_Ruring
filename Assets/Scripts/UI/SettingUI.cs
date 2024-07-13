using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider effectSlider;
    void Start()
    {
        bgmSlider.onValueChanged.AddListener(delegate{GameManager.BgmVolume.Invoke(bgmSlider.value);});
        effectSlider.onValueChanged.AddListener(delegate{GameManager.EffectVolume.Invoke(bgmSlider.value);});
    }
}
