using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate),0f);
    }

    private void OnDisable()
    {
        CancelInvoke("Animate");
    }

    private void Animate() {
        frame++;
        if (frame >= sprites.Length) {
            Debug.Log("Welcome");
            frame = 0;
        }

        if (frame > 0 && frame < sprites.Length) {
            Debug.Log("name");
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate),1f/GameManager.Instance.gameSpeed);
    }
}
