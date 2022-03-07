using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    // Canvas:ON/OF
    public static bool isFadeInstantiate = false;

    //フェードイン処理
    public bool isFadeIn = false;
    //フェードアウト処理
    public bool isFadeOut = false;

    public float alpha = 0.0f;
    public float fadeSpeed = 0.2f;


    private void Start()
    {
        // 起動時
        if (!isFadeInstantiate)
        {
            Debug.Log("isFadeInstantiate = false");
            DontDestroyOnLoad(this);
            isFadeInstantiate = true;
        }
        else
        {
            Debug.Log("isFadeInstantiate = true");
            Destroy(this);
        }
    }


    private void Update()
    {
        if (isFadeIn)
        {
            alpha -= Time.deltaTime / fadeSpeed;
            if(alpha <= 0.0f)
            {
                isFadeIn = false;
                alpha = 0.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
        else if (isFadeOut)
        {
            alpha += Time.deltaTime / fadeSpeed;
            if(alpha >= 1.0f)
            {
                isFadeOut = false;
                alpha = 1.0f;
            }
            this.GetComponentInChildren<Image>().color = new Color(0.0f, 0.0f, 0.0f, alpha);
        }
    }


    public void FadeIn()
    {
        isFadeIn = true;
        isFadeOut = false;
    }


    public void FadeOut()
    {
        isFadeOut = true;
        isFadeIn = false;
    }
}
