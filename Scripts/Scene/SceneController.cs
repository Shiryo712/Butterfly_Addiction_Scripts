using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// Scene遷移自体ではなく、Sceneの遷移までの全体の流れを管理する(Sceneの遷移の管理はTitleSceneManagerで行う)
public class SceneController : MonoBehaviour
{
    // prefab化したCanvas
    [SerializeField]
    GameObject fadeCanvas;


    void Start()
    {
        if (!FadeController.isFadeInstantiate)
        {
            Instantiate(fadeCanvas);
            Invoke("FindFadeObject", 0.02f);
        }
        else
        {
            Debug.Log("isInstantiate = true");
            Invoke("FindFadeObject", 0.02f);
        }
    }

    void FindFadeObject()
    {
        fadeCanvas = GameObject.FindGameObjectWithTag("Fade");
        // FadeInフラグを立てる
        fadeCanvas.GetComponent<FadeController>().FadeIn();
    }

    public async void SceneChange(int sceneIndex)
    {
        fadeCanvas.GetComponent<FadeController>().FadeOut();
        await Task.Delay(450);
        SceneManager.LoadScene(sceneIndex);
    }
}
