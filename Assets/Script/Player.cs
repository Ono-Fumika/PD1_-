using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    // 移動速度
    private float speed_;
    Plane plane = new Plane();
    float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        speed_ = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // カメラとマウスの位置を元にRayを準備
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
        plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
        if (plane.Raycast(ray, out distance))
        {

            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
        }

        // 移動
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(speed_, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed_, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, speed_) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, speed_) * Time.deltaTime;
        }
    }

}
