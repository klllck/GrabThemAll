using System.Globalization;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private GameObject restartMenu;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI totalCoinsTxt;
    [SerializeField] private TextMeshProUGUI coinsAmountTxt;
    [SerializeField] private TextMeshProUGUI spikesAmountTxt;

    public TextMeshProUGUI TotalCoinsTxt { get => totalCoinsTxt; set => totalCoinsTxt = value; }
    public TextMeshProUGUI CoinsAmountTxt { get => coinsAmountTxt; set => coinsAmountTxt = value; }
    public TextMeshProUGUI SpikesAmountTxt { get => spikesAmountTxt; set => spikesAmountTxt = value; }

    private void Start()
    {
        TotalCoinsTxt.text = "0";
    }

    public void Win()
    {
        restartMenu.SetActive(true);
        winUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void GameOver()
    {
        restartMenu.SetActive(true);
        gameOverUI.SetActive(true);
        winUI.SetActive(false);
    }

    public void Restart()
    {
        restartMenu.SetActive(false);
        var coinsAmount = int.Parse(CoinsAmountTxt.text, NumberStyles.Any, CultureInfo.CurrentCulture);
        var spikesAmount = int.Parse(SpikesAmountTxt.text, NumberStyles.Any, CultureInfo.CurrentCulture);
        Spawner.Instance.Init(coinsAmount, spikesAmount);
    }
}
