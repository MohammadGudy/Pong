using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Rigidbody2D ball;
    public Text scoreLeftText;
    public Text scoreRightText;
    public GameObject countdownWrapper;
    public Text countdownText;
    public GameObject winnerWrapper;
    public Text winnerText;

    private int _scoreLeft;
    private int _scoreRight;
    private const int WINNING_SCORE = 10;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        _scoreLeft = 0;
        _scoreRight = 0;
        winnerWrapper.SetActive(false);
        InitializeBall();
        StartCoroutine(_CountingDown());
    }

    public void IncreaseScore(bool left)
    {
        Debug.Log("IncreaseScore called. Left: " + left);
        if (left)
        {
            _scoreLeft++;
            scoreLeftText.text = _scoreLeft.ToString();
        }
        else
        {
            _scoreRight++;
            scoreRightText.text = _scoreRight.ToString();
        }

        if (_scoreLeft >= WINNING_SCORE || _scoreRight >= WINNING_SCORE)
        {
            EndGame();
        }
        else
        {
            InitializeBall();
        }
    }

    public void InitializeBall()
    {
        Debug.Log("InitializeBall called");
        float angle = Random.Range(0f, 30f);
        float r = Random.Range(0f, 1f);
        if (r < 0.25f)
        {
            angle = 180f - angle;
        }
        else if (r < 0.5f)
        {
            angle += 180f;
        }
        else if (r < 0.75f)
        {
            angle = 360f - angle;
        }
        angle *= Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        ball.velocity = dir.normalized * 10f;
        ball.transform.position = Vector3.zero;
    }

    private IEnumerator _CountingDown()
    {
        Debug.Log("_CountingDown started");
        countdownWrapper.SetActive(true);
        Time.timeScale = 0;
        for (int c = 3; c > 0; c--)
        {
            countdownText.text = c.ToString();
            Debug.Log("Countdown: " + c);
            yield return new WaitForSecondsRealtime(1);
        }
        countdownWrapper.SetActive(false);
        Time.timeScale = 1;
        Debug.Log("_CountingDown ended, game resumed");
    }

    private void EndGame()
    {
        Debug.Log("EndGame called");
        Time.timeScale = 0;
        FindWinner();
        winnerWrapper.SetActive(true);
        StartCoroutine(WaitAndRestart(3)); // Display winner for 3 seconds
    }

    private IEnumerator WaitAndRestart(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        winnerWrapper.SetActive(false);
        _scoreLeft = 0;
        _scoreRight = 0;
        scoreRightText.text = "0";
        scoreLeftText.text = "0";
        StartCoroutine(_CountingDown());
    }

    private void FindWinner()
    {
        Debug.Log("FindWinner called");
        if (_scoreLeft > _scoreRight)
        {
            winnerText.text = "You won the game!";
        }
        else if (_scoreRight > _scoreLeft)
        {
            winnerText.text = "You lost the game...";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }
}
