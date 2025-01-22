using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPassText : MonoBehaviour
{
    [SerializeField] int howManyChanging = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.LeanMoveLocalY(howManyChanging, 1).setEaseInOutQuart().setLoopPingPong();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
