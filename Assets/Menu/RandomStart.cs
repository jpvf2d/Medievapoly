using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStart : MonoBehaviour
{
    public Animator[] anim;

    void Start()
    {
        for(int i =0; i < anim.Length; i++)
        {
            if (anim[i] != null)
                {
                    anim[i].Play("Base Layer.Cloud" + (i+1).ToString(), 0, Random.Range(0.3f, 0.7f));
                }
        }
    }
}
