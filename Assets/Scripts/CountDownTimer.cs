using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

    public float timeValue = 0f;
    [SerializeField]
    private Text timerText;

	// Update is called once per frame
	void Update ()
    {
        if (timeValue >= 0)
            timeValue -= Time.deltaTime;
        else
            BattleController.instance.ReloadScene();
        
        DisplayTime(timeValue);
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;
        else if (timeToDisplay > 0)
            timeToDisplay++;

        if (timeToDisplay <= 6f)
            timerText.color = Color.red;
        else if (timeToDisplay <= 16f)
            timerText.color = Color.yellow;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
