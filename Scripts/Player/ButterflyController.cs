using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButterflyController : MonoBehaviour
{
    // Bluemorphoオブジェクト
    [SerializeField]
    Transform bluemorpho;

    // 操作量
    [SerializeField] float risePower = 22f;
    [SerializeField] float rotatePower = 8f;
    [SerializeField] float rotateRisePower = 15f;

    // 成功/失敗時のパーティクル
    [SerializeField] ParticleSystem outParticle;
    // 失敗時の音
    [SerializeField] AudioClip outMusic;

    // 衝突可能判定
    bool colDisabled = false;
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Animator
    [SerializeField]
    Animator animator;

    // SceneControllerクラス
    [SerializeField]
    SceneController sceneController;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        InitSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Fly", false);

        if (colDisabled == false)
        {
            RiseObjInput();
            RotateObjInput();
        }
    }


    // 上昇
    private void RiseObjInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Fly", true);
            rigidBody.AddRelativeForce(Vector3.up * risePower);
        }
    }


    // 回転
    private void RotateObjInput()
    {
        if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            Vector3 localAngle = bluemorpho.localEulerAngles;
            localAngle.y = -90f;
            bluemorpho.localEulerAngles = localAngle;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Fly", true);
            Vector3 moveForce = new Vector3(-1 * rotatePower, rotateRisePower, 0);
            rigidBody.AddForce(moveForce);
            
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            Vector3 localAngle = bluemorpho.localEulerAngles;
            localAngle.y = 90f;
            bluemorpho.localEulerAngles = localAngle;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Fly", true);
            Vector3 moveForce = new Vector3(rotatePower, rotateRisePower, 0);
            rigidBody.AddForce(moveForce);
        }
    }


    // 衝突判定
    private void OnCollisionEnter(Collision collision)
    {
        if(colDisabled == true)
        {
            return;
        }
        if(collision.gameObject.tag == "Safety")
        {
            return;
        }
        else if(collision.gameObject.tag == "Success")
        {
            SuccessProcessing();
        }
        else
        {
            OutProcessing();
        }
    }


    // クリア演出
    private void SuccessProcessing()
    {
        colDisabled = true;
        audioSource.Stop();
        Invoke("LoadNextStage", 2f);
    }
    // アウト演出
    private void OutProcessing()
    {
        colDisabled = true;
        audioSource.Stop();
        audioSource.PlayOneShot(outMusic);
        outParticle.Play();
        Invoke("LoadActiveStage", 1f);
    }


    // シーンをロードする
    private void LoadNextStage()
    {
        // 次シーンのインデックスの計算
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        // シーンの切り替え
        sceneController.SceneChange(nextSceneIndex);
    }


    private void LoadActiveStage()
    {
        sceneController.SceneChange(1);
    }

    // 初期化用
    private void InitSpeed()
    {
        this.risePower = 22f;
        this.rotatePower = 8f;
        this.rotateRisePower = 15f; 
    }
}
