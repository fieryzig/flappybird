using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;
using System.Timers;

public class Network : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameState.instance.enemy.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (GameState.instance.m_netStatus != 1) return;
        //Timer timer = new Timer(1000);
        //timer.Elapsed += new ElapsedEventHandler(beating);
        //timer.AutoReset = true;
        //timer.Enabled = true;

        string raw_data = "";
        try
        {
            raw_data = NetworkClient.instance.Recv();
        }
        catch
        {
            return;
        }
        string[] str = raw_data.Split(';');
        for (int i = 0; i < str.Length; i++)
        {
            if (GameState.instance.m_netStatus != 1) return;
            string s = str[i];
            if (s == "") return;
            Debug.Log(s);
            if (s[0] == 'L')
            {
                string username = s.Substring(5, s.Length - 5);
                if (username != GameState.instance.m_name && GameState.instance.state != GameState.StateType.Running)
                {
                    GameState.instance.enemy.SetActive(true);
                    GameState.instance.enemy.transform.position = new Vector3(-1.2f, 0f, 6f);
                    NetworkClient.instance.Send("Login" + GameState.instance.m_name);
                    GameObject.Find("bird").transform.position = new Vector3(-1.2f, 0f, 5f);
                    GameState.instance.state = GameState.StateType.Running;
                }
            }
            else if (s[0] == 'J')
            {
                string username = s.Substring(4, s.Length - 4);
                string[] tmp = username.Split(':');
                username = tmp[0];
                float y = float.Parse(tmp[1]);
                GameObject go;
                /*
                if (username == GameState.instance.m_name)
                    go = GameObject.Find("bird");
                else
                    go = GameState.instance.enemy;
                
                var pos = go.transform.position;
                pos.y = y;
                go.transform.position = pos;
                */
                if (username != GameState.instance.m_name)
                {
                    var pos = GameState.instance.enemy.transform.position;
                    pos.y = y;
                    GameState.instance.enemy.transform.position = pos;
                }
            }
            else if (s[0] == 'O')
            {
                string username = s.Substring(4, s.Length - 4);
                NetworkClient.instance.Close();
                GameState.instance.m_netStatus = 0;
                GameState.instance.state = GameState.StateType.Dead;
                GameState.instance.enemy.SetActive(false);
                if (username != GameState.instance.m_name)
                {
                    LuaFramework.Util.CallGlobalLuaFunction("SendGlobalMessage", "ENUM_DISABLE_SCORE_UI");
                    LuaFramework.Util.CallGlobalLuaFunction("SendGlobalMessage", "ENUM_SHOW_OVER_UI");
                }
            }
        }
    }

//    void beating(object source, System.Timers.ElapsedEventArgs e)
//    {
//        try
//        {
//            NetworkClient.instance.Send("Beating");
//        }
//        catch
//        {
//            NetworkClient.instance.Connect("192.168.12.27", 7788);
//        }
//    }
}
