using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public LevelMode level;
    private RectTransform rt;
    private Button btn;
    private Vector2 originalPosition;
    private Vector2 addPosition = new Vector2(-90, 30);

    public void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        btn = gameObject.GetComponent<Button>();
        originalPosition = rt.anchoredPosition;
        btn.onClick.AddListener(delegate
        {
            GameManager._levelMode = level;
            SceneManager.LoadScene("Scenes/Game/MainGame");
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.DOAnchorPos(originalPosition + addPosition, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.DOAnchorPos(originalPosition, 0f);

    }
}
