using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ExitPanel : MonoBehaviour
{
    [SerializeField] private Button btnYes;
    [SerializeField] private Button btnNo;

    [SerializeField] private Gamemanager gamemanager = null;
    [SerializeField] private TMP_Text txtCoinAmount = null;

    private void Start()
    {
        btnYes.onClick.AddListener(GoToHomeScene);
        btnNo.onClick.AddListener(CloseThisPanel);
    }

    private void GoToHomeScene() 
    {
        if (gamemanager != null && txtCoinAmount != null)
            SceneManager.LoadScene("MainScene");
        else
            Application.Quit();
    }

    private void CloseThisPanel()
    {
        if (gamemanager != null && txtCoinAmount != null)
        {
            gamemanager.SetIsShowedPanel(false);
            gamemanager.enemy.GetEnemyAnim().enabled = true;
            gamemanager.enemy.SetNavmeshHide(true);
        }

        gameObject.SetActive(false);
    }

    public void SetCoinAmount(int coinAmount)
    {
        if (txtCoinAmount != null && gamemanager != null)
            txtCoinAmount.text = coinAmount.ToString();
    }
}
