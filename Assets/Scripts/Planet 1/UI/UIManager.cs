using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject lineStroyPanel;
    [SerializeField]
    private GameObject planetsPanel;
    
    void Start()
    {
        ChangeState(mainMenuPanel.name);
    }
    public void PlayButton()
    {
        if (LineStory.controlNumber == 0)
        {
            ChangeState(lineStroyPanel.name);
            
            
            LineStory.controlNumber = 1;
            FindObjectOfType<LevelCompletedControl>().SaveObject();

            return;
        }
        ChangeState(planetsPanel.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private void ChangeState(string name)
    {
        lineStroyPanel.SetActive(name.Equals(lineStroyPanel.name));
        planetsPanel.SetActive(name.Equals(planetsPanel.name));
    }

    public void LoadingPlanetFirst(string index)
    {
        SceneManager.LoadScene($"Planet{index}");
    }
}
