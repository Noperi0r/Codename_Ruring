using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class MainGameUI : UIManager
{
    public TextMeshProUGUI ComboText;
    public GameObject[] hearts = new GameObject[3];
    public int heartCount = 3;
    public GameObject failPopup;
    public GameObject clearPopup;
    void Start()
    {
        GameManager.Fail -= HeartBreak;
        GameManager.Fail += HeartBreak;
        GameManager.GameOver -= GameOverPopup;
        GameManager.GameOver += GameOverPopup;
        GameManager.GameClear -= GameClearPopup;
        GameManager.GameClear += GameClearPopup;
        heartCount = GameManager._playerLife;
    }

    void Update()
    {
        ComboText.text = MusicGame.Combo.ToString();
    }

    public void HeartBreak()
    {
        SoundManager.Instance.PlaySound(ESoundType.HeartBreak);
        hearts[heartCount - 1].SetActive(false);
        heartCount--;
    }

    public void GameOverPopup()
    {
        BGMManager.Instance.StopBGM(EBGMType.BGM);
        SoundManager.Instance.PlaySound(ESoundType.LoseEffect);

        failPopup.SetActive(true);
    } 
    
    public void GameClearPopup()
    {
        SoundManager.Instance.PlaySound(ESoundType.WinEffect);
        clearPopup.SetActive(true);
    }
}
