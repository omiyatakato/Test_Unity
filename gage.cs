using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gage : MonoBehaviour
{
    public float fallSpeed = 5f;        // �������x
    public float cal_gage = 10f;     // �Փˎ��ɑ�����Q�[�W��
    public float Full_stomach_gage;
    public Player_Move Player_value;
    private void Start()
    {
        if (Player_value == null)
        {
            // �����ŒT���iPlayer�^�O��ݒ肵�Ă����j
            Player_value = GameObject.FindWithTag("Player").GetComponent<Player_Move>();
        }
    }
    void Update()
    {
        if (Player_value == null) return; // �O�̂��� null �`�F�b�N

        if (!Player_value.Bonus)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * 1.5f * fallSpeed * Time.deltaTime;
        }
        if (transform.position.y < -6f) // ��ʊO�ō폜
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �v���C���[�̃X�N���v�g�ɃA�N�Z�X���ăQ�[�W�𑝂₷
            Player_Move player = other.GetComponent<Player_Move>();
            if (player != null)
            {
                player.IncreaseGauge(cal_gage, Full_stomach_gage);
            }

            // ����������I�u�W�F�N�g�폜
            Destroy(gameObject);
        }
    }
}
