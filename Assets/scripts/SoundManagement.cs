using UnityEngine;
using UnityEngine.EventSystems; 

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource source;   
    public AudioClip hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (source != null && hoverSound != null)
        {
            // PlayOneShot: arka planda baska bir ses caliniyorsa onu kesmeden sesi oynatir.
            source.PlayOneShot(hoverSound);
        }


    }
}