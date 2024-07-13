using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LobbyTest : MonoBehaviour
{
    void Start()
    {
        Invoke("StartGame", 1f);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_Shooting");
    }
}
