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
        bestScoreText.text = MusicGame.MaxCombo.ToString();
        
        Sequence seq = DOTween.Sequence();
        seq.Append(currentScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo).SetDelay(2f));
        seq.Append(bestScore.DOAnchorPos(new Vector2(0f, 0f), 0.5f).SetEase(Ease.OutExpo));
        seq.Play();
    }
}
