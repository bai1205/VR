using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudio : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance)
        {
        }
    }
}
