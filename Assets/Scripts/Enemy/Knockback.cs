using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField]private float power = 3500f;

    public UnityEvent begin, end;

    public void Knock(GameObject sender)
    {
        StopAllCoroutines();
        begin?.Invoke();
        Vector2 dir = (transform.position - sender.transform.position).normalized;
        body.AddForce(dir * power, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.16f);
        body.velocity = Vector3.zero;
        end?.Invoke();
    }
}
