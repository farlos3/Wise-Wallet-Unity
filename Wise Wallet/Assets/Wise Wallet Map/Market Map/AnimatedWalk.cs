using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedWalk : AnimatedBase
{
    public float animationTime = 0.25f;
    public bool loop = true;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        if (idleSprites == null || walkSprites == null)
        {
            Debug.LogError("Sprite arrays not assigned!");
            return;
        }
    }

    protected override IEnumerator PlayAnimation(Sprite[] sprites)
    {
        while (true)
        {
            if (spriteRenderer.enabled && sprites != null && sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[animationFrame];
                animationFrame = (animationFrame + 1) % sprites.Length;

                if (!loop && animationFrame == 0)
                {
                    yield break;
                }
            }

            yield return new WaitForSeconds(animationTime);
        }
    }

    public override void ChangeAnimation(Sprite[] newSprites)
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationFrame = 0;
        animationCoroutine = StartCoroutine(PlayAnimation(newSprites));
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (idleSprites != null && idleSprites.Length > 0)
        {
            animationCoroutine = StartCoroutine(PlayAnimation(idleSprites));
        }
    }
}
