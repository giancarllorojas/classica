using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteHitState {
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

    private bool dead = false;


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

    public NoteHitState? Hit(float HitDistanceFromCenter){
        if(dead) return null;
        NoteHitState hitState = GetHitState(HitDistanceFromCenter);
        Debug.Log($"Hit {hitState} note in lane {lane}");
        Destroy(gameObject);

        return hitState;
    }

    public void Miss(){
        if(dead) return;

        dead = true;
        Debug.Log("Missed note in lane " + lane);
        Destroy(gameObject);
    }

    public void SetLane(int lane){
        this.lane = lane;
    }

    public int GetLane(){
        return this.lane;
    }
}
