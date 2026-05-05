
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private int totalTarget;
    [SerializeField] private int coin;
    [SerializeField] private int remainingTarget = 0;
    [SerializeField] private bool isAllEnemyKilled = false;

    [SerializeField] private TextMeshProUGUI currentTarget;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private RectTransform winpanel;
    [Header(" Win Panel Button")]
    [SerializeField] private Button nextlevelBtn;
    [SerializeField] private Button mainmenuBtn;

    private void Onable()
    {
        nextlevelBtn?.onClick.AddListener(LoadNextlevel);
        mainmenuBtn?.onClick.AddListener(LoadmainMenu);
    }

    private void LoadmainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextlevel()
    {
        if (PlayerDataManager.Instance.data.level <= 2)
        {
            SceneManager.LoadScene(PlayerDataManager.Instance.data.level);
        }
        else
        {
            AdMobManager.Instance.TryShowInterstitial();
            SceneManager.LoadScene(0);

        }
    }

    internal void AddEnemyKilled()
    {
        remainingTarget++;
        UpdateTheUI();

    }

    void Start()
    {
        UpdateTheUI();
    }

    void UpdateTheUI()
    {
        currentTarget.text = remainingTarget + "/" + totalTarget;
        coinText.text = PlayerDataManager.Instance.data.coins.ToString();
        if (remainingTarget >= totalTarget)
        {
            isAllEnemyKilled = true;
        }

    }

    public bool AllEnemyKilled()
    {
        return isAllEnemyKilled;
    }
    internal void AddCoin()
    {
        PlayerDataManager.Instance.data.coins++;
        PlayerDataManager.Instance.Save();
        UpdateTheUI();
    }
    public void SetWinPanel(bool state)
    {
        winpanel.gameObject.SetActive(state);
    }

}
