using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class SoundManagement : MonoBehaviour, IPointerEnterHandler
{
    [Header("Audio Source and Clip for Button Hover Sound")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (source != null && hoverSound != null)
        {
            // PlayOneShot: arka planda baska bir ses caliniyorsa onu kesmeden sesi oynatir.
            source.PlayOneShot(hoverSound);
        }
    }
}