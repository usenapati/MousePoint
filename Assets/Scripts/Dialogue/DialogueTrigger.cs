using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("VisualCue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Start()
    {
        visualCue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider entered");
        if (other.gameObject.tag == "Player") // Corrected line
        {
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") // Corrected line
        {
            visualCue.SetActive(false);
        }
    }
}