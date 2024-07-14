using System.Collections;
using System.Collections.Generic;
using System.Text;
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
        hearts[0] = transform.Find("Heart3").gameObject;
        hearts[1] = transform.Find("Heart2").gameObject;
        hearts[2] = transform.Find("Heart1").gameObject;
        failPopup = transform.Find("GameOver").gameObject;
        clearPopup = transform.Find("GameClear").gameObject;
        Debug.Log(clearPopup);
    }
    void OnEnable()
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
        if (heartCount > 0)
        {
            hearts[heartCount - 1].SetActive(false);
            heartCount--;
        }
    }

    public void GameOverPopup()
    {
        failPopup.SetActive(true);
        Cursor.visible = true;
    }
    
    public void GameClearPopup()
    {
        clearPopup.SetActive(true);
        Cursor.visible = true;
    }
}
