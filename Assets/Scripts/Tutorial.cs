using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _tutorialPicParent;
    int _totalPageNum;
    int _curPageNum;

    [SerializeField] Canvas _canvas;
    bool pass = false;

    void Start()
    {
        _totalPageNum = _tutorialPicParent.transform.childCount;
        _curPageNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            SoundManager.Instance.PlaySound(ESoundType.ClickButton);
            _tutorialPicParent.transform.GetChild(_curPageNum++)?.gameObject.SetActive(false);
            if(_curPageNum >= _totalPageNum) 
            {
                Debug.Log("tutorial enD");
                _canvas.gameObject.SetActive(true);
                transform.gameObject.SetActive(false);
                SceneManager.LoadScene("MainGame");
            }
        }
    }
}
