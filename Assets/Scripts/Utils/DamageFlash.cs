using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material defMaterial;
    private Coroutine flashCoroutine;

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration = 0.125f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defMaterial = spriteRenderer.material;
    }

    private void Update(){}

    public void Flash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }
    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = defMaterial;
        flashCoroutine = null;
    }
}
