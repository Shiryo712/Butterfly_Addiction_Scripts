using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextClearAnimator : MonoBehaviour
{
    private TMP_Text textComponent;

    [SerializeField]
    private bool isLoop = true;

    [SerializeField]
    private float maxTime = 1f;

    private float time = 0f;

    private bool isPlaying = true;


    private void Awake()
    {
        this.textComponent = GetComponent<TMP_Text>();
    }


    // Update is called once per frame
    void Update()
    {
        // 時間制御
        if(this.isPlaying || this.isLoop)
        {
            this.time += Time.deltaTime;

            if (this.isLoop)
            {
                if (this.time >= maxTime)
                    this.time -= this.maxTime;
            }
            else
            {
                if(this.time >= this.maxTime)
                {
                    this.time = this.maxTime;
                    this.isPlaying = false;
                }
            }
        }

        UpdateAnimation(this.time / this.maxTime);
    }


    private void UpdateAnimation(float time)
    {
        // ジオメトリ情報を初期化
        this.textComponent.ForceMeshUpdate(true);
        var textInfo = this.textComponent.textInfo;

        // 文字情報インデックスの範囲設定
        for(int i = 0; i < textInfo.characterInfo.Length; ++i)
        {
            // 文字情報・メッシュ情報の取得
            var charaInfo = textInfo.characterInfo[i];
            if (!charaInfo.isVisible)
                continue;

            int materialIndex = charaInfo.materialReferenceIndex;
            int vertexIndex = charaInfo.vertexIndex;
            var meshInfo = textInfo.meshInfo[materialIndex];

            // 頂点情報の編集
            float t = Mathf.Clamp01(time * 4.0f - i * 0.2f);
            meshInfo.vertices[vertexIndex + 0] += new Vector3(0f, t, 0f);
            meshInfo.vertices[vertexIndex + 1] += new Vector3(0f, t, 0f);
            meshInfo.vertices[vertexIndex + 2] += new Vector3(0f, t, 0f);
            meshInfo.vertices[vertexIndex + 3] += new Vector3(0f, t, 0f);
        }

        // ジオメトリ情報の更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
