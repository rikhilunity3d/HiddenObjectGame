using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip Click;
    public AudioClip Hint;
    public AudioClip Complete;
    public AudioClip Fail;

    public static SFXManager instance;
    public bool soundToogle = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

}
