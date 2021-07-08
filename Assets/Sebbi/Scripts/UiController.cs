using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _winMenu;

    private bool _isShowing;
    private bool _winShown;
    
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Sebbi");
    }
    
    public void QuitGame()
    {
        Time.timeScale = 1;
        Debug.Log("Game has been quit.");
        SceneManager.LoadScene("Start");
    }

    public void ClosePauseMenu()
    {
        if (_pauseMenu)
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
            _isShowing = false;
        }
    }

    public void ShowWinMenu()
    {
        if (_winMenu && !_winShown)
        {
            _winMenu.SetActive(true);
            Time.timeScale = 0;
            _isShowing = true;
            _winShown = true;
        }
    }
    
    public void CloseWinMenu()
    {
        if (_winMenu)
        {
            _winMenu.SetActive(false);
            Time.timeScale = 1;
            _isShowing = false;
        }
    }

    private void Start()
    {
        CardCountController.OnAllCardsFound += ShowWinMenu;
    }

    private void OnDestroy()
    {
        CardCountController.OnAllCardsFound -= ShowWinMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenu && !_winMenu)
            {
                return;
            }

            if (_isShowing)
            {
                ClosePauseMenu();
                CloseWinMenu();
            }
            else
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
                _isShowing = true;
            }
        }
    }
}
