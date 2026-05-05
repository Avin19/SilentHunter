using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiMainMEnu : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform gamePlay;
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private RectTransform spinTowin;
    [SerializeField] private RectTransform settingPanel;

    [Header("Button")]
    [SerializeField] private Button watchAdd;
    [SerializeField] private Button playBtn;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button elimiateBtn;
    [SerializeField] private Button taskBtn;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI eliminatesText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private Slider slider;

    void OnEnable()
    {
        playBtn?.onClick.AddListener(LoadGameplayScene);
        watchAdd?.onClick.AddListener(WatchAds);
        shopBtn?.onClick.AddListener(LoadShopPanel);
        settingBtn?.onClick.AddListener(SettingPanelOpen);
        elimiateBtn?.onClick.AddListener(ElimiateEnemy);
        taskBtn?.onClick.AddListener(TaskButton);
        slider.onValueChanged.AddListener(SliderChange);
    }

    private void SliderChange(float arg0)
    {
        slider.maxValue = PlayerDataManager.Instance.data.xpToLevelUp;
        slider.value = PlayerDataManager.Instance.data.xp / PlayerDataManager.Instance.data.xpToLevelUp;
        if (PlayerDataManager.Instance.data.xp >= PlayerDataManager.Instance.data.xpToLevelUp)
        {
            PlayerDataManager.Instance.data.xpToLevelUp *= PlayerDataManager.Instance.data.level;
        }
    }


    private void TaskButton()
    {

    }

    private void ElimiateEnemy()
    {
        if (PlayerDataManager.Instance.data.enemykilled < 40)
        {

            AdMobManager.Instance.ShowRewarded(() => PlayerDataManager.Instance.data.coins += 10);
        }
        else
        {
            AdMobManager.Instance.ShowRewarded(() => PlayerDataManager.Instance.data.coins += 200);
            PlayerDataManager.Instance.data.nextenemykillingtarget = PlayerDataManager.Instance.data.level * 40;
        }
    }


    private void SettingPanelOpen()
    {
        mainMenu.gameObject.SetActive(true);
        settingPanel.gameObject.SetActive(true);
        shopPanel.gameObject.SetActive(false);

    }

    void Start()
    {
        userNameText.text = PlayerDataManager.Instance.data.username;
        levelText.text = " Level " + PlayerDataManager.Instance.data.level;
        coinText.text = PlayerDataManager.Instance.data.coins.ToString();
        eliminatesText.text = $"Eliminates 40 targets {PlayerDataManager.Instance.data.enemykilled} / {PlayerDataManager.Instance.data.nextenemykillingtarget}";
        xpText.text = $"{PlayerDataManager.Instance.data.xp} / {PlayerDataManager.Instance.data.xpToLevelUp}";
        slider.maxValue = PlayerDataManager.Instance.data.xpToLevelUp;
        slider.value = PlayerDataManager.Instance.data.xp / PlayerDataManager.Instance.data.xpToLevelUp;
    }


    private void LoadShopPanel()
    {
        mainMenu.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(true);
    }

    private void WatchAds()
    {
        AdMobManager.Instance.ShowRewarded(() => PlayerDataManager.Instance.data.coins += 200);
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene(PlayerDataManager.Instance.data.level);
    }

}
