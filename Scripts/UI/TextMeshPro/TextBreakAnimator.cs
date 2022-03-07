using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBreakAnimator : MonoBehaviour
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
        if (this.isPlaying || this.isLoop)
        {
            this.time += Time.deltaTime;

            if (this.isLoop)
            {
                if (this.time >= maxTime)
                    this.time -= this.maxTime;
            }
            else
            {
                if (this.time >= this.maxTime)
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
        for (int i = 0; i < textInfo.characterInfo.Length; ++i)
        {
            // 文字情報・メッシュ情報の取得
            var charaInfo = textInfo.characterInfo[i];
            if (!charaInfo.isVisible)
                continue;

            int materialIndex = charaInfo.materialReferenceIndex;
            int vertexIndex = charaInfo.vertexIndex;
            var meshInfo = textInfo.meshInfo[materialIndex];

            // 頂点情報の編集
            Vector3 vertex0 = meshInfo.vertices[vertexIndex + 0];
            Vector3 vertex1 = meshInfo.vertices[vertexIndex + 1];
            Vector3 vertex2 = meshInfo.vertices[vertexIndex + 2];
            Vector3 vertex3 = meshInfo.vertices[vertexIndex + 3];

            // ノイズ生成
            Vector3 rotationNoise = new Vector3(
                Mathf.PerlinNoise(i * 0.1f, 0.4f) * 2.0f - 1.0f,
                Mathf.PerlinNoise(i * 0.2f, 0.5f) * 2.0f - 1.0f,
                Mathf.PerlinNoise(i * 0.3f, 0.6f) * 2.0f - 1.0f);

            Vector3 positionNoise = new Vector3(
                Mathf.PerlinNoise(i * 0.7f, 0.1f) * 2.0f - 1.0f,
                Mathf.PerlinNoise(i * 0.8f, 0.2f) * 2.0f - 1.0f,
                Mathf.PerlinNoise(i * 0.9f, 0.3f) * 2.0f - 1.0f);

            // 回転
            var center = Vector3.Scale(vertex2 - vertex0, Vector3.one * 0.5f) + vertex0;
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(rotationNoise * 360f * time * 5));
            vertex0 = matrix.MultiplyPoint(vertex0 - center) + center;
            vertex1 = matrix.MultiplyPoint(vertex1 - center) + center;
            vertex2 = matrix.MultiplyPoint(vertex2 - center) + center;
            vertex3 = matrix.MultiplyPoint(vertex3 - center) + center;

            // 移動
            positionNoise = positionNoise * 3f * time * 5;
            vertex0 += positionNoise;
            vertex1 += positionNoise;
            vertex2 += positionNoise;
            vertex3 += positionNoise;

            // 代入
            meshInfo.vertices[vertexIndex + 0] = vertex0;
            meshInfo.vertices[vertexIndex + 1] = vertex1;
            meshInfo.vertices[vertexIndex + 2] = vertex2;
            meshInfo.vertices[vertexIndex + 3] = vertex3;
        }

        // ジオメトリ情報の更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
