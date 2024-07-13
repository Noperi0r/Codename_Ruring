using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Game;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public RectTransform currentScore;
    public RectTransform bestScore;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    
    void Start()
    {
        currentScoreText.text = MusicGame.Combo.ToString();

        switch (GameManager._levelMode)
        {
            case LevelMode.Easy:
                bestScoreText.text = GameManager._maxScore_Easy.ToString();
                break;
            case LevelMode.Normal:
                bestScoreText.text = GameManager._maxScore_Normal.ToString();
                break;
            case LevelMode.Hard:
                bestScoreText.text = GameManager._maxScore_Hard.ToString();
                break;
        }

        bestScoreText.text = MusicGame.MaxCombo.ToString();
        
        Sequence seq = DOTween.Sequence();
        seq.Append(currentScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo).SetDelay(2f));
        seq.Append(bestScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo));
        seq.Play();
    }
}
