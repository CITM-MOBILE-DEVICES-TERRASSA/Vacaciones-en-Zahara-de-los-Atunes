using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Texture2D crossTexture; 
    public AudioClip shootSound; 
    private AudioSource audioSource;
    void Start()
    {
        Cursor.visible = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = shootSound;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0)) 
        {
            PlayShootSound();
        }
    }

    void OnGUI()
    {
        if (crossTexture != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            float size = 50f; 
            Rect rect = new Rect(mousePosition.x - size / 2, Screen.height - mousePosition.y - size / 2, size, size);
            GUI.DrawTexture(rect, crossTexture);
        }
    }

    private void PlayShootSound()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.Play();
        }
    }
}
