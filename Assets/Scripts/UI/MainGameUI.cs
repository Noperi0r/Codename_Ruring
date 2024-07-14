using System.Collections;
using System.Collections.Generic;
using System.Text;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameUI : UIManager
{
    public TextMeshProUGUI ComboText;
    [SerializeField] public GameObject[] hearts = new GameObject[3];
    public int heartCount = 3;
    [SerializeField] public GameObject failPopup;
    [SerializeField] public GameObject clearPopup;
    

    void OnEnable()
    {
        SceneManager.sceneLoaded += InitGO;
        GameManager.Fail -= HeartBreak;
        GameManager.Fail += HeartBreak;
        GameManager.GameOver -= GameOverPopup;
        GameManager.GameOver += GameOverPopup;
        GameManager.GameClear -= GameClearPopup;
        GameManager.GameClear += GameClearPopup;
        heartCount = GameManager._playerLife;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= InitGO;
    }

    public void InitGO(Scene scene, LoadSceneMode mode)
    {
        hearts[0] = transform.GetChild(7).gameObject;
        hearts[1] = transform.GetChild(6).gameObject;
        hearts[2] = transform.GetChild(5).gameObject;
        failPopup = transform.GetChild(11).gameObject;
        clearPopup = transform.GetChild(12).gameObject;
    }

    void Update()
    {
        ComboText.text = MusicGame.Combo.ToString();
    }

    public void HeartBreak()
    {
        if (heartCount > 0)
        {
            SoundManager.Instance.PlaySound(ESoundType.HeartBreak);
            hearts[heartCount - 1].SetActive(false);
            heartCount--;
        }
    }

    public void GameOverPopup()
    {
        //BGMManager.Instance.StopBGM(EBGMType.StarBubble);
        SoundManager.Instance.PlaySound(ESoundType.LoseEffect);
        
            failPopup.SetActive(true);
        Cursor.visible = true;
    }
    
    public void GameClearPopup()
    {
        SoundManager.Instance.PlaySound(ESoundType.WinEffect);
        clearPopup.SetActive(true);
        Cursor.visible = true;
    }
}
