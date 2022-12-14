using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIHandler : UIHandlerMain
{

    [SerializeField] private TextMeshProUGUI _kmetCountLabel;
    [SerializeField] private TextMeshProUGUI _knightCountLabel;
    [SerializeField] private TextMeshProUGUI _foodProductionFullLabel;
    [SerializeField] private TextMeshProUGUI _foodDemandslabel;
    [SerializeField] private TextMeshProUGUI _foodProductionLabel;
    [SerializeField] private TextMeshProUGUI _foodCountLabel;
    [SerializeField] private TextMeshProUGUI _banditsCountLabel;
    [SerializeField] private TextMeshProUGUI _kmetPrice;
    [SerializeField] private TextMeshProUGUI _knightPrice;
    [SerializeField] private TextMeshProUGUI _currentDay;
    [SerializeField] private TextMeshProUGUI _currentRaid;
    [SerializeField] private TextMeshProUGUI _rulesText;

    [SerializeField] private Button _trainKmetButton;
    [SerializeField] private Button _trainKnightButton;
    [SerializeField] private Image _pauseBttnImage;

    [SerializeField] private Sprite _pauseBttnActive;
    [SerializeField] private Sprite _pauseBttnInactive;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _blockPanel;

    [SerializeField] private GameManager _manager;



    void Start()
    {
        Time.timeScale = 0f;
        _rulesText.text = $"\tВы - глава деревни.\r\n\t" +
            $"В ваших краях появились бандиты. " +
            $"Для защиты от них необходимо построить стены и выкопать ров. " +
            $"Однако, это дело не быстрое и займет около {_manager.DaysToWin} дней. " +
            $"Вы можете ускорить строительство, наняв опытную бригаду строителей, " +
            $"но на это требуется {_manager.FoodToBuldWalls} пшеницы. " +
            $"Один крестьянин производит {_manager.FoodProductionByOneKmet} пшеницу.\r\n\t" +
            $"Разведчик сообщил, что через {_manager.RaidDelay} дня бандиты начнут на деревню. " +
            $"Для защиты от них нужны рыцари. Один рыцарь берет {_manager.FoodDemandsByOneKnight} пшеницу" +
            $" в качестве оплаты испособен победить {_manager.BandNeedsToKillKnight} бандитов.\r\n\t" +
            $"{_manager.KmetsNeedsToKillBandit} крестянина могу справиться с одним бандитом" +
            $"Найм рыцарей происходит мгновенно, но требует пшеницы. Вы так же можете тренировать рыцарей бесплатно, но на это уходит время." +
            $"К тому же для тренировки рыцаря нужно переобучить крестьянина.\r\n\t" +
            $"С каждым {_manager.BanditsIncreasingRate} рейдом количество бандитов увеличивается на 1." +
            $"Если в деревне останется меньше крестьян, чем необходимо для защиты деревни, то деревня будет захвачена.\r\n\t" +
            $"Удачи, спасти деревню.";
    
    }

    public void UpdateUI(int kmentCount, int knightCount,int foodProductionFull, int foodDemands, int foodProduction, 
        int foodCount, int banditCount, int kmetPrice, int knightPrice, int currentRaid, int currentDay)
    {
        _kmetCountLabel.text = kmentCount.ToString();
        _knightCountLabel.text = knightCount.ToString();
        _foodProductionFullLabel.text = foodProductionFull.ToString();
        _foodDemandslabel.text = foodDemands.ToString();
        _foodProductionLabel.text = foodProduction.ToString();
        _foodCountLabel.text = foodCount.ToString();
        _banditsCountLabel.text = banditCount.ToString();

        _kmetPrice.text = kmetPrice.ToString();
        _knightPrice.text = knightPrice.ToString();

        _currentDay.text = currentDay.ToString();
        _currentRaid.text = currentRaid.ToString();
    }

    public void TrainKmet()
    {
        if(_manager.TrainKmet())      
        {
             _trainKmetButton.interactable = false;
        }
    }

    public void TrainKnight()
    {
        if (_manager.TrainKnights())
        {
             _trainKnightButton.interactable = false;
        }
    }

    public void ResetTrainKmet()
    { 
        _trainKmetButton.interactable = true;
    }

    public void ResetTrainKnight()
    {
        _trainKnightButton.interactable = true;
    }

    public void SetUIOnTheStart()
    {
        _startPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }

    public void OpenRules(bool shouldPause)
    {
        if (shouldPause)
        {
            _menuPanel.SetActive(true);
            _startPanel.SetActive(true);
        }
        else
        {
            _menuPanel.SetActive(false);
            _startPanel.SetActive(false);
        }

        ResumePauseGame();
    }

    public void ResumePauseGame()
    {
        if (Time.timeScale == 0f)
        {
            _pauseBttnImage.sprite = _pauseBttnActive;
            Time.timeScale = 1f;
        }
        else
        {
            _pauseBttnImage.sprite = _pauseBttnInactive;
            Time.timeScale = 0f;
        }

    }

    public void ShowWinPanel(bool shouldShow)
    {
        ResumePauseGame();
        _menuPanel.SetActive(shouldShow);
        _winPanel.SetActive(shouldShow);
    }

    public void ShowFailPanel(bool shouldShow)
    {
        ResumePauseGame();
        _menuPanel.SetActive(shouldShow);
        _failPanel.SetActive(shouldShow);
    }

}

