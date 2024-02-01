using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomJump : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpDuration = 0.5f;
    private Vector3 originalPosition;
    private bool isJumping = false;
     private AudioSource audioSource;

    void Start()
    {
        originalPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isJumping)
        {
            StartCoroutine(Jump());
        }
         if (audioSource != null)
            {
                audioSource.Play();
            }

    }

    IEnumerator Jump()
    {
        isJumping = true;
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(startPosition.x, startPosition.y + jumpHeight, startPosition.z);

        while (elapsedTime < jumpDuration)
        {
            // Calculate the current position using a sinusoidal function to create a smooth jump and fall motion
            float height = Mathf.Sin(Mathf.PI * (elapsedTime / jumpDuration));
            transform.position = Vector3.Lerp(startPosition, endPosition, height);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Optionally smooth out the landing (not strictly necessary if the jump ends at the peak)
        transform.position = originalPosition;
        isJumping = false;
    }
}
