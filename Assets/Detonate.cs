using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonate : MonoBehaviour
{
    AudioSource audio;
    public GameObject explosion;
    float radius=5f;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        StartCoroutine(Denot());
    }

    public IEnumerator Denot()
    {
        yield return new WaitForSeconds(3f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(20f, transform.position, radius, 0.1f, ForceMode.Impulse);
            }
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        audio.Play();        
    }
}
