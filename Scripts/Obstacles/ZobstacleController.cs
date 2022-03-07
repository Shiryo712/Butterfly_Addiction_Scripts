using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZobstacleController : MonoBehaviour
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
            new Vector3(objPos.x, objPos.y, Mathf.Sin(Time.time) * power + objPos.z);
    }
}
