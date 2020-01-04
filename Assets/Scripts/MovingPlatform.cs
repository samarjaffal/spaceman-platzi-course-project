using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject target = null;
    private Vector3 offset;
    private Vector3 starPosition;
    private Animator animator;
    // void Start()
    // {
    //     target = null;
    //     offset = Vector3.zero;
    //     animator = GetComponent<Animator>();
    //     starPosition = this.transform.position;
    // }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Player"){
            animator.enabled = true;
            // target = col.gameObject;
            // offset = target.transform.position - starPosition;
            col.transform.parent = this.transform;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player"){
            // target = null;
            // offset = Vector3.zero;
            // animator.enabled = false; 
            col.transform.parent = null;   
        }
        
    }
    // void LateUpdate()
    // {
    //     if (target != null)
    //     {
    //         target.transform.position = this.transform.position + offset;
    //     }
    // }

}
