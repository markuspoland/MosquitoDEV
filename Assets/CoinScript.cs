using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RDG;

public class CoinScript : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Collider collider;
    AudioSource audioSource;
    [SerializeField] AudioClip coinSound;
    [SerializeField] ParticleSystem coinParticle;
    float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(5, 0, 0) * rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vibration.Vibrate(40);
            Instantiate(coinParticle, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(coinSound);
            meshRenderer.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, coinSound.length);
        }
    }
}
