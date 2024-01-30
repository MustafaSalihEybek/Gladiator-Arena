using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Mainmanager : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnExit;

    [SerializeField] private Animator cameraAnim;
    [SerializeField] private Animator canvasAnim;

    [SerializeField] private TMP_Text txtCoinAmount;
    [SerializeField] private GameObject exitPanel;

    private int userCoinAmount;

    private void Start()
    {
        btnPlay.onClick.AddListener(GoToPlayScene);
        btnSettings.onClick.AddListener(ShowSettingsPanel);
        btnExit.onClick.AddListener(ShowExitPanel);

        userCoinAmount = PlayerPrefs.GetInt("UserCoinAmount", 0);
        txtCoinAmount.text = userCoinAmount.ToString();

        cameraAnim.SetTrigger("PlayAnim");
        canvasAnim.SetTrigger("PlayAnim");
    }

    private void GoToPlayScene() { SceneManager.LoadScene("PlayScene"); }

    private void ShowSettingsPanel() { Debug.Log("Settings"); }

    private void ShowExitPanel() { exitPanel.SetActive(true); }
}
