using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Update()
    {
    }

    public void HandleTouch()
    {
        foreach(Touch touch in Input.touches){
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            touchPosition.x = (touchPosition.x / ScreenSize.GetScreenToWorldWidth) + 0.5f;
            touchPosition.y = (touchPosition.y / ScreenSize.GetScreenToWorldHeight) + 0.5f;

            if(touchPosition.y <= spriteHeight){
                if(touchPosition.x <= (keySize * 1) + keyMargin){
                    key1TouchPhase = touch.phase;
                } else if(touchPosition.x <= (keySize * 2) + keyMargin){
                    key2TouchPhase = touch.phase;
                } else if(touchPosition.x <= (keySize * 3) + keyMargin){
                    key3TouchPhase = touch.phase;
                } else if(touchPosition.x <= (keySize * 4) + keyMargin){
                    key4TouchPhase = touch.phase;
                } else if(touchPosition.x <= (keySize * 5) + keyMargin){
                    key5TouchPhase = touch.phase;
                }
            }
        }

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
