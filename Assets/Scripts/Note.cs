using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum NoteHitState {
    Miss,
    Ok,
    Good,
    Perfect
}

public class Note : MonoBehaviour
{
    private int lane;
    private Camera camera;
    private KeysManager keysManager;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Camera").GetComponent<Camera>(); 
    }

    private NoteHitState GetHitState(float HitDistanceFromCenter){
        if(HitDistanceFromCenter < 0.3){
            return NoteHitState.Perfect;
        }else if(HitDistanceFromCenter < 0.6){
            return NoteHitState.Good;
        }else {
            return NoteHitState.Ok;
        }
        
    }

    public void Hit(float HitDistanceFromCenter){
        NoteHitState hitState = GetHitState(HitDistanceFromCenter);
        Debug.Log($"Hit {hitState} note in lane {lane}");
    }

    public void Miss(){
        Debug.Log("Missed note in lane " + lane);
        //Destroy(gameObject);
    }

    public void SetLane(int lane){
        this.lane = lane;
    }

    public int GetLane(){
        return this.lane;
    }
}
