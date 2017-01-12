using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;

public class BirdController : MonoBehaviour {

    public float Speed = 15.0f;
    private static Rigidbody2D _rigidBody2D;

    private BoxCollider2D _boxCollider2D;

    // Use this for initialization
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (_rigidBody2D.IsSleeping() && GameState.instance.state == GameState.StateType.Running && !GameState.instance.m_ability)
        {
            _rigidBody2D.WakeUp();
            Debug.Log("Bird Mass Wake up");
        }

        if (GameState.instance.m_netStatus == 1 && GameState.instance.state == GameState.StateType.Running)
        {
            NetworkClient.instance.Send("Jump" + GameState.instance.m_name + ":" + transform.position.y.ToString());
        }

        if ( !GameState.instance.m_ability && GameState.instance.state == GameState.StateType.Running && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ))
        {
            Debug.Log("Space Key Down");
            //_rigidBody2D.velocity = Vector2.up * Speed;
            Jump();
            
            //if (GameState.instance.m_netStatus == 1) NetworkClient.instance.SendJump();
            //else Jump();
        }

        //if (Input.GetKeyDown(KeyCode.M) && GameState.instance.state == GameState.StateType.Running)
        if (GameState.instance.m_ability && GameState.instance.state == GameState.StateType.Running)
        {
            //GameState.instance.m_ability = true;
            _rigidBody2D.Sleep();
            _boxCollider2D.enabled = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //if (Input.GetKeyDown(KeyCode.N) && GameState.instance.state == GameState.StateType.Running)
        if (!GameState.instance.m_ability && GameState.instance.state == GameState.StateType.Running)
        {
            //GameState.instance.m_ability = false;
            _rigidBody2D.WakeUp();
            _boxCollider2D.enabled = true;
            transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);
        }

        /** Debug
        if (GameState.instance.state == GameState.StateType.Running && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Dead landing");
            GameState.instance.state = GameState.StateType.Dead;
            panelEnd.SetActive(true);
            showScore(GameState.instance.score);
            showBestScore(GameState.instance.score);
        }
        
        if (GameState.instance.state == GameState.StateType.Running)
        {
            if (GameState.instance.score != score)
            {
                delScore(score / 2);
                //delScore(score/2);
                score = GameState.instance.score;
                showScore(score / 2, 0f, 2f);
            }
        }
        
        if (GameState.instance.state == GameState.StateType.Running && transform.position.y < -1.0f)
        {
            GameState.instance.state = GameState.StateType.Dead;
            panelEnd.SetActive(true);
            //delScore(score/2);
            showScore(GameState.instance.score / 2);
            showBestScore(GameState.instance.score / 2);
        }

        if (GameState.instance.state == GameState.StateType.Dead && Input.GetKeyDown(KeyCode.P))
        {
            GameState.instance.reset();
            panelEnd.SetActive(false);
            transform.position = new Vector3(0f, 1f, 0f);
        }
        **/
    }

    public void Jump()
    {
        _rigidBody2D.velocity = Vector2.up * Speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameState.instance.m_ability) return;
        if (GameState.instance.state == GameState.StateType.Dead) return;
        Debug.Log("Game Over");
        GameState.instance.state = GameState.StateType.Dead;
        if (GameState.instance.m_curScore > GameState.instance.m_bstScore)
            GameState.instance.m_bstScore = GameState.instance.m_curScore;
        LuaFramework.Util.CallGlobalLuaFunction("SendGlobalMessage", "ENUM_DISABLE_SCORE_UI");
        LuaFramework.Util.CallGlobalLuaFunction("SendGlobalMessage", "ENUM_SHOW_OVER_UI");
    }
    /** Former Version
    void showScore(int score, float x = 1.1f, float y = 1.2f)
    {
        string s = score.ToString();
        int len = s.Length;
        Vector3 pos = new Vector3(x, y, -1f);
        for (int i = len - 1; i >= 0; i--)
        {
            GameObject numObj = Resources.Load("Prefabs/number_" + s[i]) as GameObject;
            GameObject number = GameObject.Instantiate(numObj);
            number.transform.position = pos;
            pos += new Vector3(-0.2f, 0.0f, 0.0f);
        }
    }

    void showBestScore(int score)
    {
        showScore(score, 1.1f, 0.6f);
    }

    void delScore(int score)
    {
        string s = score.ToString();
        Debug.Log("Delete " + s);
        int len = s.Length;
        for (int i = 0; i < len; i++)
        {
            GameObject go = GameObject.Find("number_" + s[i] + "(Clone)");
            if (go != null)
            {
                //Debug.Log("go null " + s[i] + " " + score.ToString());
                Destroy(go.gameObject);
                //go = GameObject.Find("number_" + s[i] + "(Clone)");
            }
        }
    }
    */
}
