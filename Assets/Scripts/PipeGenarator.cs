using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeGenarator : MonoBehaviour
{

    public float genDuration = 2.0f;
    private static GameObject pipeDownObject;
    private static GameObject pipeUpObject;
    private bool isStarted = false;

    public void Start()
    {
        pipeDownObject = Resources.Load("Prefabs/pipe_down") as GameObject;
        pipeUpObject = Resources.Load("Prefabs/pipe_up") as GameObject;
        if (pipeDownObject == null)
        {
            Debug.Log("Cant load prefab pipe.");
        }
        
    }

    public void Update()
    {
        if (!isStarted && GameState.instance.state == GameState.StateType.Running)
        {
            StartCoroutine(MakeNewPipe());
            isStarted = true;
        }
    }

    IEnumerator MakeNewPipe()
    {
        Random.seed = GameState.instance.seed;
        while (true)
        {
            yield return new WaitForSeconds(genDuration);
            if (GameState.instance.state == GameState.StateType.Dead)
            {
                isStarted = false;
                break;
            }
            if (GameState.instance.m_ability)
            {
                genDuration = .5f;
            }
            else
            {
                genDuration = 1.5f;
            }
            GameObject newPipe = GameObject.Instantiate(pipeDownObject);
            GameObject newPipeup = GameObject.Instantiate(pipeUpObject);
            float height = Random.Range(-3f, -1.5f);
            newPipe.transform.position = new Vector3(3, height, 6);
            newPipeup.transform.position = new Vector3(3, 6 + height, 6);
        }
    }
}
