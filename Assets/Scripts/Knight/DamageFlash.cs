using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration = 0.125f;

    private SpriteRenderer spriteRenderer;
    private Material defMaterial;
    private Coroutine flashCoroutine;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
