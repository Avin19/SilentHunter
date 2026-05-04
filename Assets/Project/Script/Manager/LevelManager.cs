
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    [SerializeField] private int totalTarget;
    [SerializeField] private int coin;
    [SerializeField] private int remainingTarget = 0;
    [SerializeField] private bool isAllEnemyKilled = false;

    [SerializeField] private TextMeshProUGUI currentTarget;
    [SerializeField] private TextMeshProUGUI coinText;

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
        coinText.text = coin.ToString();
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
        coin++;
        UpdateTheUI();
    }

}
