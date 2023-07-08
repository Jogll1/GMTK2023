using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float fadeDuration = 2f;
    private float currentDuration = 0f;

    private SpriteRenderer spriteRenderer;
    private Color initialColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.material.color;
    }

    void Update()
    {
        currentDuration += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, currentDuration / fadeDuration);

        Color newColor = initialColor;
        newColor.a = alpha;
        spriteRenderer.material.color = newColor;

        if (currentDuration >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }
}
