using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCut : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public IEnumerator EuthanizeSelf()
    {
        boxCollider.enabled = true;
        rb.useGravity = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
