using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public GameObject NotePrefab;
    private List<GameObject> notes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(int lane){
        Vector3 pos = GetLanePosition(lane);
        GameObject note = Instantiate(NotePrefab, pos, Quaternion.identity);
        Rigidbody2D noteRB = note.GetComponent<Rigidbody2D>();

        note.GetComponent<Note>().SetLane(lane);

        noteRB.velocity = new Vector3(0, -4, 0);
        notes.Add(note);
    }

    private Vector3 GetLanePosition(int lane){
        float width = ScreenSize.GetScreenToWorldWidth;
        float startingX = -width / 2;
        float margin = (width / 5f)/2;

        return new Vector3(startingX - margin + (width / 5) * (lane + 1), 10, 0);
    }
}
