
using TMPro;
using UnityEngine;
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

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;

}
