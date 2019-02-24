using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    public Transform BG1, BG2;
    public float scrollSpeed;

    private float bgWidth;     //how wide is the background image, set to private
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        bgWidth = BG1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        BG1.position = new Vector3(BG1.position.x - (scrollSpeed * Time.deltaTime), BG1.position.y, BG2.position.z); // duration for each frame to happen
        BG2.position -= new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f); //does same as above

        if (BG1.position.x < -bgWidth - 1) //have we moved so far as to be off screen 
        {
            BG1.position += new Vector3(bgWidth * 2f, 0f, 0f);
        }
        if (BG2.position.x < -bgWidth - 1) //have we moved so far as to be off screen 
        {
            BG2.position += new Vector3(bgWidth * 2f, 0f, 0f);
        }
    }
}
