using UnityEngine;
using System.Collections;

public abstract class AnimatedBase : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    protected Coroutine animationCoroutine;
    protected int animationFrame;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public abstract void ChangeAnimation(Sprite[] newSprites);

    public virtual void Restart()
    {
        animationFrame = -1;
        StopAllCoroutines();
        OnDisable();
        OnEnable();
    }

    protected abstract IEnumerator PlayAnimation(Sprite[] sprites);

    protected virtual void OnDisable()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }
    }

    protected virtual void OnEnable()
    {
       if (idleSprites != null && idleSprites.Length > 0)
        {
            animationCoroutine = StartCoroutine(PlayAnimation(idleSprites));
        }
    }
}
