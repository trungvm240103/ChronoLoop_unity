using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPlaybackManager : MonoBehaviour
{
    [System.Serializable]
    public class ActionRecord
    {
        public string actionType; 
        public float time;        
    }

    public List<ActionRecord> recordedActions = new List<ActionRecord>();
    private bool isRecording = false;
    private bool isPlaying = false;
    private float startTime;
    private int currentActionIndex = 0;

    public PlayerController player;

    void Update()
    {
        
        if (isRecording)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                RecordAction("MoveLeft");
            if (Input.GetKeyDown(KeyCode.RightArrow))
                RecordAction("MoveRight");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                    RecordAction("JumpLeft");
                else if (Input.GetKey(KeyCode.RightArrow))
                    RecordAction("JumpRight");
                else
                    RecordAction("JumpUp");
            }
        }
    }

    public void StartRecording()
    {
        if (isPlaying) return;

        isRecording = true;
        recordedActions.Clear(); 
        startTime = Time.time;

        player.SetControl(false); 
    }

    public void StopRecording()
    {
        isRecording = false;
        player.SetControl(true); 
    }

    public void StartPlayback()
    {
        if (isRecording || recordedActions.Count == 0) return;

        isPlaying = true;
        currentActionIndex = 0;
        player.SetControl(false); 
        StartCoroutine(PlaybackRoutine());
    }

    private IEnumerator PlaybackRoutine()
    {
        while (currentActionIndex < recordedActions.Count)
        {
            var action = recordedActions[currentActionIndex];

          
            if (currentActionIndex == 0)
                yield return new WaitForSeconds(action.time);
            else
                yield return new WaitForSeconds(action.time - recordedActions[currentActionIndex - 1].time);

            
            ExecuteAction(action.actionType);

            currentActionIndex++;
        }

        isPlaying = false;
        player.SetControl(true); 
    }

    private void RecordAction(string actionType)
    {
        recordedActions.Add(new ActionRecord
        {
            actionType = actionType,
            time = Time.time - startTime
        });
    }

    private void ExecuteAction(string actionType)
    {
        switch (actionType)
        {
            case "MoveLeft":
                player.Move(-1);
                break;
            case "MoveRight":
                player.Move(1);
                break;
            case "JumpLeft":
                player.JumpAndMove(-1); 
                break;
            case "JumpRight":
                player.JumpAndMove(1); 
                break;
            case "JumpUp":
                player.JumpAndMove(0); 
                break;
        }
    }
}
