using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushWiggle : MonoBehaviour
{
    public float wiggleDistance = 0.1f;
    public float wiggleSpeed = 10f;
    public int wiggleCount = 4;

    private AudioSource audioSource;
    private Vector3 originalPosition;
    private float wiggleTimer = 0f;
    private int currentWiggle = 0;
    private bool isWiggling = false;

    private void Start()
    {
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void Update()
    {
        if (isWiggling)
        {
            wiggleTimer += Time.deltaTime * wiggleSpeed;

            // Move left and right
            float delta = wiggleDistance * Mathf.Sin(wiggleTimer);
            transform.position = originalPosition + new Vector3(delta, 0, 0);

            // Check if the wiggle count is reached
            if (wiggleTimer > Mathf.PI * currentWiggle)
            {
                currentWiggle++;
            }

            if (currentWiggle > wiggleCount)
            {
                isWiggling = false;
                transform.position = originalPosition; // Reset position
                wiggleTimer = 0f;
                currentWiggle = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Play the sound effect every time the player enters
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Start wiggling if not already wiggling
            if (!isWiggling)
            {
                isWiggling = true;
            }
        }
    }
}