using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniMapIMG : MonoBehaviour
{
    public Image img;
    private float timePrev;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        timePrev = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       if((Time.time - timePrev) >= 0.3f)
        {
            img.enabled = !img.enabled;
            timePrev = Time.time;
        }
    }
}
