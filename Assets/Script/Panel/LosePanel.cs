using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReplay;

    private void Start()
    {
        btnHome.onClick.AddListener(GoToHomeScene);
        btnReplay.onClick.AddListener(GoToPlayScene);
    }

    private void GoToHomeScene() { LoadScene("MainScene"); }

    private void GoToPlayScene() { LoadScene("PlayScene"); }

    private void LoadScene(string sceneName) { SceneManager.LoadScene(sceneName); }
}
