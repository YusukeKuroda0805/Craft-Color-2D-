using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTitle : MonoBehaviour {
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    [SerializeField] private string SceneName;
    [SerializeField] private int SoundID;

    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.JoystickButton9) == true)
        {
          
            audioSource.clip = audioClips[SoundID];
            audioSource.Play();
            Invoke("SceneMove", 0.3f);
        }
    }

    void SceneMove()
    {
        SceneManager.LoadScene(SceneName);
    }
}
