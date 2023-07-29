using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    private AudioSource _deathAudio;
    // Start is called before the first frame update
    void Start()
    {
        _deathAudio = GetComponent<AudioSource>();
        StartCoroutine(DestroyAfter());
    }

    private IEnumerator DestroyAfter() {
        Debug.Log("A");
        _deathAudio.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    
}
