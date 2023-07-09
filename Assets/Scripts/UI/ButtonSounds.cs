using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        //create an AudioSource component if it doesn't exist
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //disable the play on awake option to prevent unintended sound playback
        audioSource.playOnAwake = false;

        //add the button click event listener
        button.onClick.AddListener(PlayClickSound);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //play the hover sound when the mouse pointer enters the button
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //play the click sound when the button is clicked
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    private void PlayClickSound()
    {
        // Play the click sound when the button is clicked
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}