using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleManager : MonoBehaviour
{

    public bool isLeft;
    private KeyCode _upKey;
    private KeyCode _downKey;
    private float _speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        _upKey = isLeft ? KeyCode.W : KeyCode.UpArrow;
        _downKey = isLeft ? KeyCode.S : KeyCode.DownArrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(_upKey)) MoveUp();
        if (Input.GetKey(_downKey)) MoveDown();
    }
    void MoveUp()
    {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
        if (transform.position.y > 4.25f)
        {
            Vector3 p = transform.position;
            p.y = 4.25f;
            transform.position = p;
        }
    }
    void MoveDown()
    {
        transform.Translate(-Vector2.up * _speed * Time.deltaTime);
        if (transform.position.y < -4.25f)
        {
            Vector3 p = transform.position;
            p.y = -4.25f;
            transform.position = p;
        }
    }
}
