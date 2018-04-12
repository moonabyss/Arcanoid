using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseRelease : MonoBehaviour, IPointerUpHandler
{
    public GameObject soundPrefab;
    public AudioClip clip;

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject sound = Instantiate(soundPrefab, transform);
        sound.GetComponent<AudioSource>().clip = clip;
        sound.GetComponent<AudioSource>().Play();
        Destroy(sound, sound.GetComponent<AudioSource>().clip.length);
    }
}
