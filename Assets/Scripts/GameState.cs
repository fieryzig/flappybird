using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public enum StateType
    {
        Init, Running, Dead
    };

    public int m_curScore = 0;
    public int m_bstScore = 0;
    public bool m_ability = false;
    public int m_id = -1;
    public string m_name = "";
    public int seed = 0;

    public GameObject enemy = GameObject.Find("enemy");

    /* Net Status
     * 0 : Haven't connected
     * 1 : Connected
     * 2 : Play in Local Mode
     */
    public int m_netStatus = 0; 

    private StateType _state = StateType.Init;
    
    public StateType m_prestate = StateType.Init;
    public StateType state
    {
        get
        {
            return _state;
        }

        set
        {
            m_prestate = _state;
            _state = value;
        }
    }

    // Constructor
    private GameState() {}
    public static readonly GameState instance = new GameState();
}
