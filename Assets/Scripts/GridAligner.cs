using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridAligner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject line1 = GameObject.Find("Line1");
        GameObject line2 = GameObject.Find("Line2");
        GameObject line3 = GameObject.Find("Line3");
        GameObject line4 = GameObject.Find("Line4");
        GameObject line5 = GameObject.Find("Line5");

        float width = ScreenSize.GetScreenToWorldWidth;
        float startingX = -width / 2;
        float margin = (width / 5f)/2;

        line1.transform.position = new Vector3(startingX - margin + (width / 5) * 1, 0, 0);
        line2.transform.position = new Vector3(startingX - margin + (width / 5) * 2, 0, 0);
        line3.transform.position = new Vector3(startingX - margin + (width / 5) * 3, 0, 0);
        line4.transform.position = new Vector3(startingX - margin + (width / 5) * 4, 0, 0);
        line5.transform.position = new Vector3(startingX - margin + (width / 5) * 5, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Start();
    }
}
