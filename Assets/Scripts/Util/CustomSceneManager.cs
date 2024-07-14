using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSceneManager : MonoSingleton<CustomSceneManager>
{
    public GameObject Lobby;
    public GameObject MainGame;

    public void MoveSceneToMain()
    {
        Lobby.SetActive(false);
        MainGame.SetActive(true);
        
        GameManager.Instance.OnMainLoad();
    }

    public void MoveSceneToLobby()
    {
        Lobby.SetActive(true);
        MainGame.SetActive(false);
        
        GameManager.Instance.OnLobbyLoad();
    }
}
