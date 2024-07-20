using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{
    public bool isLeft;
    private KeyCode _upKey;
    private KeyCode _downKey;
    private KeyCode _rightKey;
    private KeyCode _leftKey;
    private float _speed = 10f;
    private float _rightPaddleSpeed = 3.2f;
    public Transform ball;

    void Start()
    {
        if (isLeft)
        {
            _upKey = KeyCode.W;
            _downKey = KeyCode.S;
            _rightKey = KeyCode.D;
            _leftKey = KeyCode.A;
        }
    }

    void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (isLeft)
            {
                HandleTouchInput();
                HandleTiltInput();
            }
            else
            {
                AutoMove();
            }

        }
        else
        {
            if (isLeft)
            {
                if (Input.GetKey(_upKey))
                {
                    MoveUp(_speed);
                }
                else if (Input.GetKey(_downKey))
                {
                    MoveDown(_speed);
                }
                else if (Input.GetKey(_rightKey))
                {
                    StartCoroutine(_rotateRight());
                }
                else if (Input.GetKey(_leftKey))
                {
                    StartCoroutine(_rotateLeft());
                }
            }
            else
            {
                AutoMove();
            }
        }
    }

    private void HandleTouchInput()
    {
        if (isLeft) { 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
            if (touchPosition.x < 0)  // Ensures the touch is on the left side of the screen
            {
                transform.position = new Vector3(transform.position.x, touchPosition.y, transform.position.z);
            }
        }
        }
    }

    private void HandleTiltInput()
    {
        if (isLeft) { 
        float tilt = Input.acceleration.x;
        if (tilt > 0.1f)
        {

                StartCoroutine(goRight());
            }
        else if (tilt < -0.1f)
        {
                //StartCoroutine(_rotateRight());
                StartCoroutine(goRight());
            }
        }
    }

    private IEnumerator goRight()
    {
        float tilt = Input.acceleration.x;
        if (tilt > 0)
        {
            transform.Rotate(0, 0, -6f);
            yield return new WaitForSecondsRealtime(0.1f);
            transform.Rotate(0, 0, 6f);
        }
        if (tilt < 0)
        {
            transform.Rotate(0, 0, 6f);
            yield return new WaitForSecondsRealtime(0.1f);
            transform.Rotate(0, 0, -6f);
        }
        yield return new WaitForSecondsRealtime(0.1f);
//        transform.Rotate(0, 0, -0.5f);
    }


    private IEnumerator _rotateRight()
    {
        transform.Rotate(0, 0, 0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        transform.Rotate(0, 0, -0.5f);
    }

    private IEnumerator _rotateLeft()
    {
        transform.Rotate(0, 0, -0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        transform.Rotate(0, 0, 0.5f);
    }

    public void AutoMove()
    {
        if (ball.position.y > transform.position.y)
        {
            MoveUp(_rightPaddleSpeed);
        }
        else if (ball.position.y < transform.position.y)
        {
            MoveDown(_rightPaddleSpeed);
        }
    }

    void MoveUp(float ballSpeed)
    {
        transform.Translate(Vector2.up * ballSpeed * Time.deltaTime);
        if (transform.position.y > 4.25f)
        {
            Vector3 p = transform.position;
            p.y = 4.25f;
            transform.position = p;
        }
        if (isLeft)
        {
            if (transform.position.x > -8 || transform.position.x < -8)
            {
                Vector3 thisX = transform.position;
                thisX.x = -8;
                transform.position = thisX;
            }
        }
    }

    void MoveDown(float ballSpeed)
    {
        transform.Translate(-Vector2.up * ballSpeed * Time.deltaTime);
        if (transform.position.y < -4.25f)
        {
            Vector3 p = transform.position;
            p.y = -4.25f;
            transform.position = p;
        }
        if (isLeft)
        {
            if (transform.position.x > -8 || transform.position.x < -8)
            {
                Vector3 thisX = transform.position;
                thisX.x = -8;
                transform.position = thisX;
            }
        }
    }
}
