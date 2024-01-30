using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private WonPanel wonPanel;
    [SerializeField] private LosePanel losePanel;
    [SerializeField] private ExitPanel exitPanel;

    [SerializeField] private Player player;
    [SerializeField] public Enemy enemy;
    [SerializeField] private Button btnBack;
    [SerializeField] private TMP_Text txtCoinAmount;

    private bool isShowedPanel = false;
    private bool isCalledInvoke = false;
    private int coinAmount = 0;
    private int userCoinAmount;

    private void Start() 
    {
        btnBack.onClick.AddListener(ShowExitPanel);
        userCoinAmount = PlayerPrefs.GetInt("UserCoinAmount", 0);
        coinAmount = Random.Range(50, 151);
        txtCoinAmount.text = coinAmount.ToString();
    }

    private void Update()
    {
        if (enemy.GetIsEnemyDead())
        {
            if (!player.GetIsPlayerDead())
            {
                if (!isCalledInvoke)
                {
                    isCalledInvoke = true;
                    Invoke("ShowWonPanel", 2f);
                }
            }
            else
            {
                if (!isCalledInvoke)
                {
                    isCalledInvoke = true;
                    Invoke("ShowLosePanel", 2f);
                }
            }
        }
        else
        {
            if (player.GetIsPlayerDead())
            {
                if (!isCalledInvoke)
                {
                    isCalledInvoke = true;
                    Invoke("ShowLosePanel", 2f);
                }
            }
        }
    }

    public bool GetIsShowedPanel() => isShowedPanel;

    public bool SetIsShowedPanel(bool state) => isShowedPanel = state;

    private void ShowWonPanel()
    {
        isShowedPanel = true;
        wonPanel.SetCoinAmount(coinAmount);
        SaveEarnedCoinAmount(coinAmount);
        wonPanel.gameObject.SetActive(true);
    }

    private void ShowLosePanel()
    {
        isShowedPanel = true;
        losePanel.gameObject.SetActive(true);
    }

    private void ShowExitPanel()
    {
        isShowedPanel = true;
        enemy.GetEnemyAnim().enabled = false;
        enemy.SetNavmeshHide(false);
        exitPanel.SetCoinAmount(coinAmount);
        exitPanel.gameObject.SetActive(true);
    }

    private void SaveEarnedCoinAmount(int coin) { PlayerPrefs.SetInt("UserCoinAmount", (userCoinAmount + coin)); }
}
