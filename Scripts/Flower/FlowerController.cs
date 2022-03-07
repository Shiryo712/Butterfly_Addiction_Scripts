using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    // 成功/失敗時のパーティクル
    [SerializeField] ParticleSystem successParticle;

    // 衝突音
    [SerializeField] AudioClip flowerMusic;

    AudioSource audioSource;

    [SerializeField]Light pointLight;

    // 衝突判定(パーティクル用)
    bool isTouched;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        pointLight.gameObject.SetActive(false);

        isTouched = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(isTouched == false)
        {
            successParticle.Play();
            audioSource.PlayOneShot(flowerMusic);
            pointLight.gameObject.SetActive(true);

            isTouched = true;
        }
    }
}
