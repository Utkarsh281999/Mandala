using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(AudioSource))]
public class MandalaController : MonoBehaviour
{
    [Header("Mandala Settings")]
    [SerializeField] private GameObject mandalaObject;
    [SerializeField] private int rotationSpeed = 10;
    [SerializeField] private float initialScale = 1.0f;
    
    [Header("Color Settings")]
    [SerializeField] private Color initialColor = Color.blue;
    [SerializeField] private Material mandalaMaterial;
    
    [Header("Timeline Control")]
    [SerializeField] private PlayableDirector timelineDirector;
    [SerializeField] private AudioSource audioSource;
    
    // Shape complexity controls
    [Range(3, 24)]
    [SerializeField] private int segments = 8;
    [Range(1, 10)]
    [SerializeField] private int layers = 3;
    
    // Private variables
    private Transform mandalaTransform;
    private float currentScale;
    private float targetScale;
    private Color targetColor;
    
    void Start()
    {
        // Get components
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
            
        // Initialize mandala if it exists
        if (mandalaObject != null)
        {
            mandalaTransform = mandalaObject.transform;
            currentScale = initialScale;
            targetScale = initialScale;
            
            // Set initial material color
            if (mandalaMaterial != null)
            {
                mandalaMaterial.color = initialColor;
                targetColor = initialColor;
            }
        }
        else
        {
            Debug.LogError("Mandala object not assigned!");
        }
        
        // Initialize Timeline
        if (timelineDirector != null)
        {
            // Optional: Synchronize timeline with audio
            // timelineDirector.time = 0;
            // timelineDirector.Play();
        }
    }
    
    void Update()
    {
        if (mandalaTransform == null) return;
        
        // Basic rotation animation
        mandalaTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        // Smooth scale transitions
        currentScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * 2.0f);
        mandalaTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
        
        // Smooth color transitions
        if (mandalaMaterial != null)
        {
            mandalaMaterial.color = Color.Lerp(mandalaMaterial.color, targetColor, Time.deltaTime * 2.0f);
        }
    }
    
    // Public methods to control mandala from Timeline or other scripts
    
    public void SetScale(float scale)
    {
        targetScale = scale;
    }
    
    public void SetColor(Color color)
    {
        targetColor = color;
    }
    
    public void SetComplexity(int newSegments, int newLayers)
    {
        segments = Mathf.Clamp(newSegments, 3, 24);
        layers = Mathf.Clamp(newLayers, 1, 10);
        
        // Here you would update the mandala mesh or pattern
        // This would connect to your mandala generation code
        UpdateMandalaShape();
    }
    
    public void PlayAudio()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    
    public void PauseAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
    
    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
    
    // This method would be expanded to generate or update the mandala pattern
    private void UpdateMandalaShape()
    {
        // Placeholder for mesh generation or pattern updates
        Debug.Log($"Updating mandala with {segments} segments and {layers} layers");
        
        // Your mandala generation code would go here
        // This might involve updating a procedural mesh or
        // modifying a shader parameter
    }
}