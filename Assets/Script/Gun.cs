using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    // プレイヤー(子)
    public GameObject Sphere;
    // プレイヤータグ
    private string Tag = "Player";
    // プレイヤーが持っているかのフラグ
    private bool havePlayer_ = false;

    Plane plane = new Plane();
    float distance = 0;


    // 銃のゲームオブジェクト
    public GameObject gun;
    // ライン
    private LineRenderer lineRenderer;
    // 始点
    Vector3 firstPosition;
    // 終点
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

       
    }

    // Update is called once per frame
    void Update()
    {

        // プレイヤーが持っている時
        if (havePlayer_)
        {
            // カメラとマウスの位置を元にRayを準備
            var ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);

            // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (plane.Raycast(ray_, out distance))
            {

                // 距離を元に交点を算出して、交点の方を向く
                var lookPoint = ray_.GetPoint(distance);
                transform.LookAt(lookPoint);
            }

            // クリックされたら
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

                // Rayを生成
               // Ray ray = new Ray(gameObject.transform.position, worldMousePos);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                // Raycastの結果を格納する変数
                RaycastHit hit;

                firstPosition = gun.transform.position;

                // Rayがオブジェクトにヒットした場合
                if (Physics.Raycast(ray, out hit))
                {
                    // ヒットした位置にラインを引く
                    targetPosition = hit.point;
                }
                else
                {
                    // 何もヒットしない場合、レイの方向に一定の距離だけラインを延ばす
                    targetPosition = ray.GetPoint(100.0f);
                }

                lineRenderer.SetPosition(0, firstPosition);
                lineRenderer.SetPosition(1, targetPosition);
                Debug.DrawRay(ray.origin, ray.direction * 30, UnityEngine.Color.red, 5.0f);

            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == Tag)
        {
            // 持っているかフラグをオンにする
            havePlayer_ = true;
            // 銃をプレイヤーの正面に配置
            Vector3 offset = new Vector3(1.0f, 0.5f, 0.5f); // 銃の位置のオフセット
            Vector3 pos = Sphere.transform.position + Sphere.transform.TransformDirection(offset);
            transform.position = pos;


            // 銃をプレイヤーの子にする
            gameObject.transform.parent = Sphere.gameObject.transform;
        }

    }
}
