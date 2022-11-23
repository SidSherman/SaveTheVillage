using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _kmetCountLabel;
    [SerializeField] private TextMeshProUGUI _knightCountLabel;
    [SerializeField] private TextMeshProUGUI _FoodProductionFullLabel;
    [SerializeField] private TextMeshProUGUI _FoodDemandslabel;
    [SerializeField] private TextMeshProUGUI _FoodProductionLabel;
    [SerializeField] private TextMeshProUGUI _FoodCountLabel;
    [SerializeField] private TextMeshProUGUI _BanditsCountLabel;
    [SerializeField] private TextMeshProUGUI _KmetPrice;
    [SerializeField] private TextMeshProUGUI _KnightPrice;
    [SerializeField] private TextMeshProUGUI _CurrentDay;
    [SerializeField] private TextMeshProUGUI _CurrentRaid;

    [SerializeField] private Button _trainKmetButton;
    [SerializeField] private Button _trainKnightButton;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _blockPanel;

    [SerializeField] private GameManager _manager;

    [SerializeField] private string _rulesString;
    [SerializeField] private TextMeshProUGUI _rulesText;

    void Start()
    {

        _rulesText.text = $"\tВы - глава деревни.\r\n\t" +
            $"С недавнего времени в ваших краях появились бандиты. " +
            $"Для защиты от них вы решили построить стены и выкопать ров. " +
            $"Однако, это дело не быстрое и займет около {_manager.DaysToWin} дней. " +
            $"Вы можете ускорить строительство, наняв опытную бригаду строителей, " +
            $"но на это требуется {_manager.FoodToBuldWalls} пшеницы. " +
            $"Один крестьянин производит {_manager.FoodProductionByOneKmet} пшеницу.\r\n\t" +
            $"Разведчик сообщил, что бандиты через {_manager.RaidDelay} дня главарь бандитов начнет отправлять рейды на деревню. " +
            $"Для защиты от бандитов можно нанять рыцаря. Один рыцарь берет {_manager.FoodDemandsByOneKnight} пшеницу" +
            $" в качестве оплаты испособен победить {_manager.BandNeedsToKillKnight} бандитов.\r\n\t" +
            $"{_manager.KmetsNeedsToKillBandit} крестянина могу справиться с одним бандитом" +
            $"3аметьте, что на найм крестьянина и тренировку рыцаря уходит время, а с каждым рейдом количество бандитов увеличивается на 1.\r\n\t" +
            $"Если в деревне останется меньше крестьян, чем необходимо для защиты деревни, то деревня будет разрушена" +
            $"Yдачи, спасти деревню.";
    
    }

    public void UpdateUI(int kmentCount, int knightCount,int foodProductionFull, int foodDemands, int foodProduction, 
        int foodCount, int banditCount, int kmetPrice, int knightPrice, int currentRaid, int currentDay)
    {
        _kmetCountLabel.text = kmentCount.ToString();
        _knightCountLabel.text = knightCount.ToString();
        _FoodProductionFullLabel.text = foodProductionFull.ToString();
        _FoodDemandslabel.text = foodDemands.ToString();
        _FoodProductionLabel.text = foodProduction.ToString();
        _FoodCountLabel.text = foodCount.ToString();
        _BanditsCountLabel.text = banditCount.ToString();

        _KmetPrice.text = kmetPrice.ToString();
        _KnightPrice.text = knightPrice.ToString();

        _CurrentDay.text = currentDay.ToString();
        _CurrentRaid.text = currentRaid.ToString();
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

    public void ResumePauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            Time.timeScale = 0f;
          
            _menuPanel.SetActive(true);
            _startPanel.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            
            _menuPanel.SetActive(false);
            _startPanel.SetActive(false);
        }
       
    }

    public void ShowWinPanel()
    {
        Time.timeScale = 0f;
        _menuPanel.SetActive(true);
     
        _winPanel.SetActive(true);
    }

    public void ShowFailPanel()
    {
        Time.timeScale = 0f;
        _menuPanel.SetActive(true);
        
        _failPanel.SetActive(true);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}

