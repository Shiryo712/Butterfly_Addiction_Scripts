using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scene遷移を管理する
public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    SceneController sceneController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("LoadNextStage", 0.1f);
        }
    }

    // シーンをロードする
    private void LoadNextStage()
    {
        // 次シーンのインデックスの計算
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // シーンの切り替え
        sceneController.SceneChange(nextSceneIndex);
    }
}
