using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent onReset;
    public GameObject readyPanel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActive = false;

    public ShooterRotator shooterRotator;
    public CamFollow cam;

    private int score = 0;
    private const string BEST_SCORE = "BestScore";
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine("RoundRoutine");
        UpdateUI();
    }
    
    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }
    
    private void UpdateBestScore() // 값 저장.
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    private int GetBestScore() // 값 꺼내오기.
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    private void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "Best Score : " + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void ResetRound()
    {
        score = 0;
        UpdateUI();
        StartCoroutine("RoundRoutine");
    }

    private IEnumerator RoundRoutine()
    {
        //Ready
        onReset.Invoke();
        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;
        messageText.text = "Ready...";
        yield return new WaitForSeconds(3f);

        //Play
        isRoundActive=true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true; // OnEnable()

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while (isRoundActive) //exit when ball is destroyed
        { 
            yield return null;
        }

        // EnD
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait for next round...";

        yield return new WaitForSeconds(3f);

        ResetRound();
    }
}
