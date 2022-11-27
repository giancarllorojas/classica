using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyState {
    Untapped,
    Tapped
}

public class Keys {
    public KeyState key1 = KeyState.Untapped;
    public KeyState key2 = KeyState.Untapped;
    public KeyState key3 = KeyState.Untapped;
    public KeyState key4 = KeyState.Untapped;
    public KeyState key5 = KeyState.Untapped;

    public Keys(TouchPhase? k1, TouchPhase? k2, TouchPhase? k3, TouchPhase? k4, TouchPhase? k5){
        key1 = k1 == TouchPhase.Began || k1 == TouchPhase.Moved || k1 == TouchPhase.Stationary ? KeyState.Tapped : KeyState.Untapped;
        key2 = k2 == TouchPhase.Began || k2 == TouchPhase.Moved || k2 == TouchPhase.Stationary ? KeyState.Tapped : KeyState.Untapped;
        key3 = k3 == TouchPhase.Began || k3 == TouchPhase.Moved || k3 == TouchPhase.Stationary ? KeyState.Tapped : KeyState.Untapped;
        key4 = k4 == TouchPhase.Began || k4 == TouchPhase.Moved || k4 == TouchPhase.Stationary ? KeyState.Tapped : KeyState.Untapped;
        key5 = k5 == TouchPhase.Began || k5 == TouchPhase.Moved || k5 == TouchPhase.Stationary ? KeyState.Tapped : KeyState.Untapped;
    }

    public bool IsPressed(int lane){
        switch(lane){
            case 0:
                return key1 == KeyState.Tapped;
            case 1:
                return key2 == KeyState.Tapped;
            case 2:
                return key3 == KeyState.Tapped;
            case 3:
                return key4 == KeyState.Tapped;
            case 4:
                return key5 == KeyState.Tapped;
            default:
                return false;
        }
    }
}

[ExecuteInEditMode]
public class KeysManager : MonoBehaviour
{
    private GameObject keysSprite;
    private SpriteRenderer spriteRenderer;

    private float spriteHeight;
    private float spriteWidth;

    private float keyMargin;
    private float keySize;

    // Start is called before the first frame update
    void Start()
    {
        keysSprite = GameObject.Find("PianoKeys");
        spriteRenderer = keysSprite.GetComponent<SpriteRenderer>();

        float width = ScreenSize.GetScreenToWorldWidth;
        keysSprite.transform.localScale = new Vector3(width, width + 3, 1);
        
        spriteHeight = spriteRenderer.bounds.size.y / ScreenSize.GetScreenToWorldHeight;
        spriteWidth = spriteRenderer.bounds.size.x / ScreenSize.GetScreenToWorldWidth;

        keyMargin = (1 - spriteWidth) / 2;
        keySize = (spriteWidth / 5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public float GetHeight(){
        return spriteHeight;
    }

    public Keys GetTouches()
    {
        TouchPhase? key1TouchPhase = null;
        TouchPhase? key2TouchPhase = null;
        TouchPhase? key3TouchPhase = null;
        TouchPhase? key4TouchPhase = null;
        TouchPhase? key5TouchPhase = null;

        foreach(Touch touch in Input.touches){
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            touchPosition.x = (touchPosition.x / ScreenSize.GetScreenToWorldWidth) + 0.5f;
            touchPosition.y = (touchPosition.y / ScreenSize.GetScreenToWorldHeight) + 0.5f;

            if(touchPosition.y <= spriteHeight){
                key1TouchPhase = touchPosition.x <= (keySize * 1) + keyMargin ? touch.phase as TouchPhase? : null;
                key2TouchPhase = touchPosition.x <= (keySize * 2) + keyMargin ? touch.phase as TouchPhase? : null;
                key3TouchPhase = touchPosition.x <= (keySize * 3) + keyMargin ? touch.phase as TouchPhase? : null;
                key4TouchPhase = touchPosition.x <= (keySize * 4) + keyMargin ? touch.phase as TouchPhase? : null;
                key5TouchPhase = touchPosition.x <= (keySize * 5) + keyMargin ? touch.phase as TouchPhase? : null;
            }
        }

        return new Keys(key1TouchPhase, key2TouchPhase, key3TouchPhase, key4TouchPhase, key5TouchPhase);
    }

    void OnDrawGizmosSelected()
    {
        GameObject keysSprite = GameObject.Find("PianoKeys");
        SpriteRenderer sRenderer = keysSprite.GetComponent<SpriteRenderer>();

        float spriteHeight = sRenderer.bounds.size.y / ScreenSize.GetScreenToWorldHeight;
        float spriteWidth = sRenderer.bounds.size.x / ScreenSize.GetScreenToWorldWidth;

        float keyMargin = (1 - spriteWidth) / 2;
        float keySize = (spriteWidth / 5f);

        Camera camera = GameObject.Find("Camera").GetComponent<Camera>();
        Vector3 origin = camera.ViewportToWorldPoint(new Vector3(0, spriteHeight, 0));
        Vector3 to = camera.ViewportToWorldPoint(new Vector3(1, spriteHeight, 0));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, to);

        Gizmos.DrawLine(camera.ViewportToWorldPoint(new Vector3((keySize * 1) + keyMargin, 0, 0)), camera.ViewportToWorldPoint(new Vector3((keySize * 1) + keyMargin, spriteHeight, 0)));
        Gizmos.DrawLine(camera.ViewportToWorldPoint(new Vector3((keySize * 2) + keyMargin, 0, 0)), camera.ViewportToWorldPoint(new Vector3((keySize * 2) + keyMargin, spriteHeight, 0)));
        Gizmos.DrawLine(camera.ViewportToWorldPoint(new Vector3((keySize * 3) + keyMargin, 0, 0)), camera.ViewportToWorldPoint(new Vector3((keySize * 3) + keyMargin, spriteHeight, 0)));
        Gizmos.DrawLine(camera.ViewportToWorldPoint(new Vector3((keySize * 4) + keyMargin, 0, 0)), camera.ViewportToWorldPoint(new Vector3((keySize * 4) + keyMargin, spriteHeight, 0)));
    }
}
