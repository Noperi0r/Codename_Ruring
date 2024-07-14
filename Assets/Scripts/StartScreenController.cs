using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    [SerializeField] GameObject _howToImage;
    [SerializeField] Canvas _canvas;

    public void GoLobby()
    {
        _canvas.enabled = false;
        _howToImage.SetActive(false);
    }

    public void ShowHowTo()
    {
        _canvas.enabled = false;
        _howToImage.SetActive(true); 
    }

    public void Exit()
    {

    }

}
