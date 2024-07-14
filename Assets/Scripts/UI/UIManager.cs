using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //보여줘야 할 모든 UI는 이미 Canvas 위에 올라가있다고 가정한다. (Enable로 켤 수 있게)
    public void PopUI(GameObject obj)
    {
        Cursor.visible = true;  
        SoundManager.Instance.PlaySound(ESoundType.ClickButton);
        obj.SetActive(true);
    } 

    public void DeleteUI(GameObject obj)
    {
        if (SceneManager.GetActiveScene().name == GameManager._mainSceneName)
            Cursor.visible = false;

        SoundManager.Instance.PlaySound(ESoundType.ClickButton);
        obj.SetActive(false);
    }
}
