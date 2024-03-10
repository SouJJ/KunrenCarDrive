using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] GameObject ClearUI;
    public AudioClip goalSE;
    AudioSource aud;

    void Start()
    {
        this.ClearUI.SetActive(false);
        this.aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Car passed through the goal!");
            this.ClearUI.SetActive(true);
            this.aud.PlayOneShot(this.goalSE);
        }
    }
}