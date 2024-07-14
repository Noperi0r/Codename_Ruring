using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] GameObject _tutorialPicParent;
    int _totalPageNum;
    int _curPageNum;

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
                // TODO: 튜토리얼 종료 이벤트
            }
        }
    }
}
