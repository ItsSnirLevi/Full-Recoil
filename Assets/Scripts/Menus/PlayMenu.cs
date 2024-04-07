using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour {

    public static bool isSpecOps = false;       // default is legion
    public static int amountOfSoldiers = 5;     // default is 5
    public static int scoreToWin = 3;           // default is 3
    public static int roundTime = 2;            // default is 2
    
    [SerializeField]
    private Slider amountOfSoldiersSlider;
    [SerializeField]
    private Text amountOfSoldiersDisplay;

    public Slider roundTimeSlider;
    public Text roundTimeText;

    [Header("Scriptables")]
    public ScoreSOLegion scoreSOLegion;
    public ScoreSOSpecOps scoreSOSpecOps;
    public TrackerSO tracker;
    public FundsSO funds;
    public PlayerController playerController;

    private int nextSceneIndex = 1;

    private void Start()
    {
        amountOfSoldiersSlider.onValueChanged.AddListener((v) =>
        {
            amountOfSoldiersDisplay.text = v.ToString();
            amountOfSoldiers = (int)v;
        });
    }

    public void ChangeTeams()
    {
        isSpecOps = !isSpecOps;
        //Debug.Log("isSpecOps- " + isSpecOps);        
    }

    public void StartButton()
    {
        scoreSOLegion.Value = 0;
        scoreSOSpecOps.Value = 0;
        tracker.Value = 0;
        funds.Value = 250;
        playerController.ResetValues();
        //BattleController.instance.ResetKills();
        LevelLoader.instance.LoadNext(nextSceneIndex);
    }

    public void SetScoreToWin(int value)
    {
        scoreToWin = value;
    }

    public void SetRoundTime()
    {
        roundTime = (int)roundTimeSlider.value;
        roundTimeText.text = roundTime.ToString();
    }

    public void SetNextScene(int index)
    {
        nextSceneIndex = index;
    }

}
