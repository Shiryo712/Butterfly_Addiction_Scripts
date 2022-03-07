using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YobstacleController : MonoBehaviour
{
    [SerializeField] float power = 5.0f;
    // ゲーム実行時のオブジェクトの位置
    Vector3 objPos;

    // Start is called before the first frame update
    void Start()
    {
        objPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position =
            new Vector3(objPos.x, Mathf.Sin(Time.time) * power + objPos.y, objPos.z);
    }
}
