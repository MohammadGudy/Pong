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

    private int _scoreLeft;
    private int _scoreRight;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        _scoreLeft = 0;
        _scoreRight = 0;
        InitializeBall();
        StartCoroutine(_CountingDown()); 
    }

    // Update is called once per frame


    public void IncreaseScore(bool left)
    {
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
    }



    public void InitializeBall()
    {
        float angle = Random.Range(0f, 30f);
        float r = Random.Range(0f, 1f);
        if (r < 0.25f)
        {
            angle = 180f - angle;
        } else if (r < 0.5f)
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
        // freeze game
        Time.timeScale = 0;

        // countdown
        for (int c = 3; c > 0; c--)
        {
            countdownText.text = c.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countdownWrapper.SetActive(false);
        // reset the time scale
        Time.timeScale = 1;
    }
}
