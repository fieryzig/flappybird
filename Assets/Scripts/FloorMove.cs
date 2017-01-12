using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour {

    public float Speed = 2.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameState.instance.state == GameState.StateType.Dead) return;
        transform.position += Vector3.left * Speed * Time.deltaTime;
        if (transform.position.x <= -1.5f)
        {
            transform.position = new Vector3(1.5f, -2.9f, 5f);
        }

        if (GameState.instance.m_ability)
        {
            Speed = 10.0f;
        }
        else
        {
            Speed = 2.0f;
        }
    }
}
