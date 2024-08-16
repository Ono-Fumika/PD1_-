using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Gun : MonoBehaviour
{
    // �v���C���[(�q)
    public GameObject Sphere;
    // �v���C���[�^�O
    private string Tag = "Player";
    // �v���C���[�������Ă��邩�̃t���O
    private bool havePlayer_ = false;

    Plane plane = new Plane();
    float distance = 0;


    // �e�̃Q�[���I�u�W�F�N�g
    public GameObject gun;
    // ���C��
    private LineRenderer lineRenderer;
    // �n�_
    Vector3 firstPosition;
    // �I�_
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

       
    }

    // Update is called once per frame
    void Update()
    {

        // �v���C���[�������Ă��鎞
        if (havePlayer_)
        {
            // �J�����ƃ}�E�X�̈ʒu������Ray������
            var ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);

            // �v���C���[�̍�����Plane���X�V���āA�J�����̏������ɒn�ʔ��肵�ċ������擾
            plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
            if (plane.Raycast(ray_, out distance))
            {

                // ���������Ɍ�_���Z�o���āA��_�̕�������
                var lookPoint = ray_.GetPoint(distance);
                transform.LookAt(lookPoint);
            }

            // �N���b�N���ꂽ��
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

                // Ray�𐶐�
               // Ray ray = new Ray(gameObject.transform.position, worldMousePos);
                Ray ray = Camera.main.ScreenPointToRay(mousePos);

                // Raycast�̌��ʂ��i�[����ϐ�
                RaycastHit hit;

                firstPosition = gun.transform.position;

                // Ray���I�u�W�F�N�g�Ƀq�b�g�����ꍇ
                if (Physics.Raycast(ray, out hit))
                {
                    // �q�b�g�����ʒu�Ƀ��C��������
                    targetPosition = hit.point;
                }
                else
                {
                    // �����q�b�g���Ȃ��ꍇ�A���C�̕����Ɉ��̋����������C�������΂�
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
            // �����Ă��邩�t���O���I���ɂ���
            havePlayer_ = true;
            // �e���v���C���[�̐��ʂɔz�u
            Vector3 offset = new Vector3(1.0f, 0.5f, 0.5f); // �e�̈ʒu�̃I�t�Z�b�g
            Vector3 pos = Sphere.transform.position + Sphere.transform.TransformDirection(offset);
            transform.position = pos;


            // �e���v���C���[�̎q�ɂ���
            gameObject.transform.parent = Sphere.gameObject.transform;
        }

    }
}
