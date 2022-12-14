using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private NotesManager notesManager;
    private KeysManager keysManager;
    private Animator hitTextAnimator;
    private TextMeshProUGUI hitText;

    Camera camera;

    private float timer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        notesManager = GameObject.Find("Notes").GetComponent<NotesManager>();
        keysManager = GameObject.Find("Keys").GetComponent<KeysManager>();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        hitTextAnimator = GameObject.Find("HitUI/Canvas/HitText").GetComponent<Animator>();
        hitText = GameObject.Find("HitUI/Canvas/HitText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer - Time.deltaTime <= 0){
            // random between 0 and 4
            int lane = Random.Range(0, 4);
            notesManager.Spawn(lane);
            timer = 2f;
        }else{
            timer -= Time.deltaTime;
        }
    }

    void FixedUpdate(){
        CheckNotes();
    }

    void CheckNotes(){
        LayerMask mask = LayerMask.GetMask("Notes");

        Keys touches = keysManager.GetTouches();

        float screenW = ScreenSize.GetScreenToWorldWidth;
        float startPoint = -screenW / 2;

        float height = camera.ViewportToWorldPoint(new Vector3(0, keysManager.GetHeight(), 0)).y;

        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(startPoint, height,0), Vector3.right, screenW, mask);
        foreach(RaycastHit2D hit in hits){
            if(hit.collider != null){
                float HitDistanceFromCenter = Mathf.Abs(hit.transform.position.y - hit.point.y);

                Note note = hit.collider.gameObject.GetComponent<Note>();
                if(note != null){
                    if(touches.IsPressed(note.GetLane())){
                        NoteHitState? hitState = note.Hit(HitDistanceFromCenter);

                        if(hitState != null){
                            if(hitState == NoteHitState.Perfect){
                                hitText.text = "Perfect";
                            }else if(hitState == NoteHitState.Good){
                                hitText.text = "Good";
                            }else if(hitState == NoteHitState.Ok){
                                hitText.text = "Ok";
                            }

                            hitTextAnimator.Play("HitStatusAnimation", 0,0);
                        }
                    }
                }
            }
        }

        RaycastHit2D[] hitsMiss = Physics2D.RaycastAll(new Vector3(startPoint, height + (height/1.5f),0), Vector3.right, screenW, mask);

        foreach(RaycastHit2D hit in hitsMiss){
            if(hit.collider != null){
                Note note = hit.collider.gameObject.GetComponent<Note>();
                if(note != null){
                    note.Miss();
                }
            }
        }

        Debug.DrawLine(new Vector3(startPoint, height + (height/1.5f),0), new Vector3(startPoint + screenW, height + (height/1.5f),0), Color.red, 20f);
    }
}
