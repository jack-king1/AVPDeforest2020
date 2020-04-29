using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animation animation;
    public Timer t;
    public bool playAnim;

    void Start()
    {
        animation = GetComponent<Animation>();
        t = new Timer( );
        t.Init(4f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if(t.Count() && playAnim)
        {
            //Debug.Log("Animation Playing");
            animation.Play();
            playAnim = false;
        }
        else if(playAnim == false && t.timer > 0 )
        {
            playAnim = true;
        }
    }
}
