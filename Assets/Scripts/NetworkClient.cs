using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LuaFramework
{
    public class NetworkClient
    {
        private NetworkClient() {}
        public static readonly NetworkClient instance = new NetworkClient();

        private Socket clientSocket;

        public int Connect(string raw_ip = "192.168.12.41", int port = 7788)
        {
            IPAddress ip = IPAddress.Parse(raw_ip);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, port));
                Debug.Log("NetworkClient Connected Successfully.");
            }
            catch
            {
                Debug.Log("Failed to connect.");
                return -1;
            }
            //clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            clientSocket.ReceiveTimeout = 10;
            clientSocket.Blocking = false;
            return 0;
        }

        public void Close()
        {
            clientSocket.Close();
        }

        public void Send(string s)
        {
            s = s + ";";
            clientSocket.Send(Encoding.ASCII.GetBytes(s));
            Debug.Log("Send: " + s);
        }

        public string Recv()
        {
            byte[] result = new byte[1024];
            int length = clientSocket.Receive(result);
            if (length == 0) return "";
            string s = Encoding.ASCII.GetString(result, 0, length);
            Debug.Log("Recv: " + s);
            return s;
        }

        public void SendJump()
        {
            string s = "Jump" + GameState.instance.m_name.ToString();
            Send(s);
            //clientSocket.Send(Encoding.ASCII.GetBytes(s));
            //Debug.Log("Send Jump:" + s);
        }
        /*
        public void StartRecvThread()
        {
            Thread t = new Thread(recvThread);
            t.Start();
        }
        
        void recvThread()
        {
            while(true)
            {
                string s = Recv();
                Debug.Log(s);
                if (s[0]=='L')
                {
                    string username = s.Substring(5, s.Length - 5);
                    if (username != GameState.instance.m_name)
                    {
                        GameObject.Find("Enemy").SetActive(true);
                        Send("Login" + GameState.instance.m_name);
                        GameState.instance.state = GameState.StateType.Running;
                    }
                }
                else if (s[0]=='J')
                {
                    string username = s.Substring(4, s.Length - 4);
                    GameObject go;
                    if (username == GameState.instance.m_name)
                        go = GameObject.Find("bird");
                    else
                        go = GameObject.Find("Enemy");
                    go.GetComponent<BirdController>().Jump();
                }
                else if (s[0]=='O')
                {
                    string username = s.Substring(4, s.Length - 4);
                    if (username != GameState.instance.m_name)
                    {
                        GameObject.Find("Enemy").SetActive(false);
                    }
                }
            }
        }
        */

    }
}