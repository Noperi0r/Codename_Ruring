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
        hearts[heartCount - 1].SetActive(false);
        heartCount--;
    }

    public void GameOverPopup()
    {
        failPopup.SetActive(true);
    }
    
    public void GameClearPopup()
    {
        clearPopup.SetActive(true);
    }
}
