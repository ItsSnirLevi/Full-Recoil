using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundOverScreen : MonoBehaviour {

    public Text killsText;
    public Text scoreText;
    public Text whoWonText;
    public GameObject oldScoreboard;
    public GameObject oldKills;
    private AudioSource music;

    public void Setup(int kills)
    {
        Invoke("PauseGame", 1f);       
        ShowMouseCursor();
        oldScoreboard.SetActive(false);
        oldKills.SetActive(false);       
        scoreText.text = BattleController.instance.scoreSOLegion.Value + " | " + BattleController.instance.scoreSOSpecOps.Value;
        whoWonText.text = (BattleController.instance.scoreSOLegion.Value > BattleController.instance.scoreSOSpecOps.Value) ? "Legion Win" : "Spec-Ops Win";
        killsText.text = kills.ToString();
        gameObject.SetActive(true);
        music = GetComponent<AudioSource>();
        music.Play();
    }

    private void Update()
    {
        ShowMouseCursor();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;     // pause the game
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        BattleController.instance.scoreSOLegion.Value = 0;
        BattleController.instance.scoreSOSpecOps.Value = 0;
        BattleController.instance.ResetKills();
        BattleController.instance.ResetFunds();
        BattleController.instance.ResetItems();
        LevelLoader.instance.LoadNext(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        ShowMouseCursor();
        LevelLoader.instance.LoadNext(0);
    }

    public void ShowMouseCursor()
    {
        BattleController.instance._vp_FPInput.MouseCursorForced = true;
        BattleController.instance._vp_FPInput.MouseCursorBlocksMouseLook = true;
        vp_Utility.LockCursor = false;
    }
}
