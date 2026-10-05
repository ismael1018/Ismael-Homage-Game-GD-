using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TMP_Text scoreText;
    public TMP_Text messageText;
    public int goalsToWin = 5;
    public int triesAllowed = 7;
    public AudioClip goalSound;
    public AudioClip missSound;

    int goals, triesTaken;
    bool scoredThisKick, gameOver;
    AudioSource audioSource;

    public bool GameOver => gameOver;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        messageText.text = "";
        UpdateUI();
    }

    public void GoalScored()
    {
        if (scoredThisKick || gameOver) return;
        scoredThisKick = true;
        goals++;
        Play(goalSound);
        if (goals >= goalsToWin) EndGame("YOU WIN! Press R to play again");
        UpdateUI();
    }

    public void KickTaken() { triesTaken++; UpdateUI(); }

    public void KickFinished()
    {
        if (!scoredThisKick && !gameOver) Play(missSound);
        scoredThisKick = false;
        if (!gameOver && triesTaken >= triesAllowed)
            EndGame("YOU LOSE! Press R to try again");
        UpdateUI();
    }

    public void Restart()
    {
        goals = 0; triesTaken = 0;
        scoredThisKick = false; gameOver = false;
        messageText.text = "";
        UpdateUI();
    }

    void Play(AudioClip clip)
    {
        if (clip != null) audioSource.PlayOneShot(clip);
    }

    void EndGame(string msg) { gameOver = true; messageText.text = msg; }

    void UpdateUI()
    {
        scoreText.text = "Goals: " + goals + " / " + goalsToWin +
                         "    Tries left: " + (triesAllowed - triesTaken);
    }
}