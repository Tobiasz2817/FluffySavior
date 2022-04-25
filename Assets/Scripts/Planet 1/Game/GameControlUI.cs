using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject LoadingGo;
    public void ExitToLobby()
    {
        LoadingGo.SetActive(true);
        SceneManager.LoadSceneAsync("UI");
    }

    public void PlayAgainButton()
    {
        LoadingGo.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void NextMissionButton()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentIndex + 1);
    }
}
