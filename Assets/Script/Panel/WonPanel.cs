using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WonPanel : MonoBehaviour
{
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnReplay;

    [SerializeField] private TMP_Text txtCoinAmount;

    private void Start()
    {
        btnHome.onClick.AddListener(GoToHomeScene);
        btnReplay.onClick.AddListener(GoToPlayScene);
    }

    private void GoToHomeScene() { LoadScene("MainScene"); }

    private void GoToPlayScene() { LoadScene("PlayScene"); }

    private void LoadScene(string sceneName) { SceneManager.LoadScene(sceneName); }

    public void SetCoinAmount(int coinAmount) { txtCoinAmount.text = coinAmount.ToString(); }
}
