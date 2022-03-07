using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem outParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Untagged")
        {
            outParticle.Play();
        }
    }
}
