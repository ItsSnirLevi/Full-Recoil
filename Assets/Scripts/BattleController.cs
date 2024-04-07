
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    private void Awake()
    {
        instance = this;
    }

    public const int SPEC_OPS_ID = 0;
    public const int LEGION_ID = 1;

    [Header("Game")]
    public int legionScore = 0;     
    public int specOpsScore = 0;
    public int bestOf;
    public CountDownTimer timer;
    public bool isPaused = false;
    public bool isRoundOver = false;
    public vp_FPInput _vp_FPInput;
    public AudioSource killSfx;

    [Header("Guns")]
    public GameObject revolver;
    public GameObject shotgun;
    public GameObject machinegun;
    public GameObject ar;
    public Transform inventorySpecOps;
    public Transform inventoryLegion;
    private Transform myInventory;

    [Header("Guns Store")]
    public List<GameObject> weapons = new List<GameObject>();
    public List<Transform> specOpsStorePositions = new List<Transform>();
    public List<Transform> legionStorePositions = new List<Transform>();

    [Header("AI")]
    public List<GameObject> LegionSoldiers;
    public List<GameObject> SpecOpsSoldiers;
    public int soldierAmountForEachTeam;
    
    [Header("GUI")]
    public Text scoreText;
    public RoundOverScreen screen;
    public GameObject pauseMenu;
    public ScoreSOLegion scoreSOLegion;
    public ScoreSOSpecOps scoreSOSpecOps;
    public Text killsText;
    public Text fundsText;
    public GameObject hitmarker;
    public AudioSource hitmarkerSound;

    [Header("Player")]
    public GameObject playerSpecOps;
    public Transform specOpsSpawnPlayer;
    public GameObject playerLegion;
    public Transform LegionSpawnPlayer;
    public TrackerSO killTracker;
    public bool isSpecOps;
    public int playerTeamID;
    public FundsSO funds;
    public PlayerController playerController;

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        scoreText.text = scoreSOLegion.Value + " | " + scoreSOSpecOps.Value;
        killsText.text = killTracker.Value.ToString();
        fundsText.text = funds.Value.ToString();
        // get static data from main menu
        isSpecOps = PlayMenu.isSpecOps;
        soldierAmountForEachTeam = PlayMenu.amountOfSoldiers;
        bestOf = PlayMenu.scoreToWin;
        timer.timeValue = PlayMenu.roundTime * 60;

        int specOpsSoldiersCnt = soldierAmountForEachTeam;
        int LegionSoldiersCnt = soldierAmountForEachTeam;
        if (isSpecOps)
        {
            GameObject tmp = Instantiate(playerSpecOps, specOpsSpawnPlayer.position, specOpsSpawnPlayer.rotation);
            _vp_FPInput = tmp.GetComponent<vp_FPInput>();
            specOpsSoldiersCnt--;
            playerTeamID = SPEC_OPS_ID;
            ActivateStore();
            myInventory = inventorySpecOps;
            //myStorePositions = specOpsStorePositions;
        }
        else
        {
            GameObject tmp = Instantiate(playerLegion, LegionSpawnPlayer.position, LegionSpawnPlayer.rotation);
            _vp_FPInput = tmp.GetComponent<vp_FPInput>();
            LegionSoldiersCnt--;
            playerTeamID = LEGION_ID;
            ActivateStore();
            myInventory = inventoryLegion;
            //myStorePositions = legionStorePositions;
        }

        for (int i = 0; i < LegionSoldiersCnt; i++)
        {
            LegionSoldiers[i].SetActive(true);
        }
        for (int i = 0; i < specOpsSoldiersCnt; i++)
        {
            SpecOpsSoldiers[i].SetActive(true);
        }

        // get the guns you bought
        if (playerController.revolver)
            Instantiate(revolver, myInventory.position, myInventory.rotation);            
        if (playerController.shotgun)
            Instantiate(shotgun, myInventory.position, myInventory.rotation);
        if (playerController.machinegun)
            Instantiate(machinegun, myInventory.position, myInventory.rotation);
        if (playerController.assaultRifle)
            Instantiate(ar, myInventory.position, myInventory.rotation);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused && !BattleController.instance.isRoundOver)
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        ShowMouseCursor();        
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        isPaused = true;
        vp_Utility.LockCursor = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        HideMouseCursor();
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void ShowMouseCursor()
    {
        if (_vp_FPInput == null)
            return;

        _vp_FPInput.MouseCursorForced = true;
        _vp_FPInput.MouseCursorBlocksMouseLook = true;
        Cursor.visible = true;
    }

    public void HideMouseCursor()
    {
        if (_vp_FPInput == null)
            return;

        _vp_FPInput.MouseCursorForced = false;
        _vp_FPInput.MouseCursorBlocksMouseLook = false;
        Cursor.visible = false;
    }

    public void UpdateScore(int team)   // we get the team id for the dead soldier
    {
        if (team == SPEC_OPS_ID) { legionScore++; }
        else { specOpsScore++; }
        
        if(legionScore == soldierAmountForEachTeam)
        {
            scoreSOLegion.Value += 1;
            Time.timeScale = 0.5f;
            if (scoreSOLegion.Value == (bestOf / 2) + 1)            
                RoundOver(killTracker.Value);         
            else
                Invoke("ReloadScene", 1);
        }
        else if (specOpsScore == soldierAmountForEachTeam)
        {
            scoreSOSpecOps.Value += 1;
            
            Time.timeScale = 0.5f;
            if (scoreSOSpecOps.Value == (bestOf / 2) + 1)            
                RoundOver(killTracker.Value);
            
            else
                Invoke("ReloadScene", 1);
        }
                
    }

    public void ReloadScene()
    {
        LevelLoader.instance.LoadNext(SceneManager.GetActiveScene().buildIndex);
    }

    private void RoundOver(int kills)
    {
        isRoundOver = true;
        screen.Setup(kills);
    }

    public void AddKill()
    {
        killTracker.Value += 1;
        killsText.text = killTracker.Value + "";
        funds.Value += 250;
        fundsText.text = funds.Value.ToString();
        killSfx.Play();
    }

    public void ResetKills()
    {
        killTracker.Value = 0;
    }

    public void ResetFunds()
    {
        funds.Value = 250;
    }

    public void UsedFunds(int amount)
    {
        funds.Value -= amount;
        fundsText.text = funds.Value.ToString();
    }

    public void ResetItems()
    {
        playerController.ResetValues();
    }

    public int GetKills()
    {
        return killTracker.Value;
    }

    public void HasShotgun(bool value)
    {
        playerController.shotgun = value;
    }

    public void HasRevolver(bool value)
    {
        playerController.revolver = value;
    }

    public void HasMachinegun(bool value)
    {
        playerController.machinegun = value;
    }

    public void HasAR(bool value)
    {
        playerController.assaultRifle = value;
    }

    public void ActivateHitmarker()
    {
        hitmarker.gameObject.SetActive(true);
        hitmarkerSound.Play();
        Invoke("DisableHitmarker", 0.2f);
    }

    public void DisableHitmarker()
    {
        hitmarker.gameObject.SetActive(false);
    }

    public void ActivateStore()
    {
        List<Transform> myStorePositions = (isSpecOps) ? specOpsStorePositions : legionStorePositions;
        for (int i = 0; i < 4; i++)
        {
            if (!playerController.GetIsActiveByIndex(i))            
                Instantiate(weapons[i], myStorePositions[i].position, myStorePositions[i].rotation);                          
        }
    }

}
