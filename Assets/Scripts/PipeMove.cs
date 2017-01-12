using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMove : MonoBehaviour {

    public float speed = 2.0f;
    private bool achieve = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -2)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < -0.5f && !achieve)
        {
            achieve = true;
            GameState.instance.m_curScore++;
        }

        if (GameState.instance.state == GameState.StateType.Dead)
        {
            Destroy(gameObject);
        }

        if (GameState.instance.m_ability)
        {
            speed = 10f;
        }
        else
        {
            speed = 2.0f;
        }
    }

    void FixedUpdate()
    {
        if (GameState.instance.state == GameState.StateType.Running)
            this.gameObject.transform.position += new Vector3(-1, 0, 0) * speed * Time.fixedDeltaTime;
    }
}
