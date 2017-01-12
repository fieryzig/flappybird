using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;
using System;

namespace LuaFramework
{
    public static class LuaHelper
    {

        /// <summary>
        /// getType
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public static System.Type GetType(string classname)
        {
            Assembly assb = Assembly.GetExecutingAssembly();  //.GetExecutingAssembly();
            System.Type t = null;
            t = assb.GetType(classname); ;
            if (t == null)
            {
                t = assb.GetType(classname);
            }
            return t;
        }

        /// <summary>
        /// 面板管理器
        /// </summary>
        public static PanelManager GetPanelManager()
        {
            return AppFacade.GetManager<PanelManager>();
        }

        /// <summary>
        /// 资源管理器
        /// </summary>
        public static ResourceManager GetResManager()
        {
            return AppFacade.GetManager<ResourceManager>();
        }

        public static void StartGame()
        {
            if (GameState.instance.m_netStatus == 2)
            {
                GameState.instance.state = GameState.StateType.Running;
                GameObject bird = GameObject.Find("bird");
                bird.transform.position = new Vector3(-1.2f, 0f, 5f);
                GameState.instance.m_curScore = 0;
            }
            else
            {
                //GameState.instance.state = GameState.StateType.Running;
                //GameObject bird = GameObject.Find("bird");
                //GameState.instance.m_curScore = 0;
                //NetworkClient.instance.StartRecvThread();
            }
            
        }

        public static int GetCurScore()
        {
            return GameState.instance.m_curScore;
        }

        public static int GetBstScore()
        {
            return GameState.instance.m_bstScore;
        }

        public static void ResetGame()
        {
            if (GameState.instance.m_netStatus == 2)
            {
                GameState.instance.state = GameState.StateType.Running;
                GameObject bird = GameObject.Find("bird");
                bird.transform.position = new Vector3(0f, 0f, 5f);
                bird.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                GameState.instance.m_curScore = 0;
            }
            else
            {
                GameState.instance.m_netStatus = 0;
                GameObject bird = GameObject.Find("bird");
                bird.transform.position = new Vector3(0f, 0f, 5f);
                bird.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                bird.GetComponent<Rigidbody2D>().Sleep();
                GameState.instance.m_curScore = 0;
                GameState.instance.enemy.SetActive(false);
                NetworkClient.instance.Connect();
                LuaFramework.Util.CallGlobalLuaFunction("SendGlobalMessage", "ENUM_SHOW_LOGIN_UI");
            }
            
        }

        public static void StartAbility()
        {
            GameState.instance.m_ability = true;
        }

        public static void CloseAbility()
        {
            GameState.instance.m_ability = false;
        }

        public static void SetBestScore(int num)
        {
            GameState.instance.m_bstScore = num;
        }

        public static int getNetStatus()
        {
            return GameState.instance.m_netStatus;
        }

        public static void setNetStatus(int status)
        {
            GameState.instance.m_netStatus = status;
        }

        public static void setUserName(string name)
        {
            GameState.instance.m_name = name;
        }

        public static string getUserName()
        {
            return GameState.instance.m_name;
        }
    }
}