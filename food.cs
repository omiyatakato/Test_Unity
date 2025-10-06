using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public GameObject[] prefabs;     // ���Ƃ������f�ނ𕡐������
    public float spawnInterval = 1f; // �����Ԋu�i�b�j
    public float fallSpeed = 5f;     // �������x
    public float spawnRangeX = 8f;   // X�����̃����_����
    public float spawnHeight = 6f;   // ����Y�ʒu

    private float timer = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }

        // �����ς݂̃I�u�W�F�N�g�����Ɉړ�
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Falling"))
        {
            obj.transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            // ��ʊO�ɍs������폜
            if (obj.transform.position.y < -spawnHeight)
            {
                Destroy(obj);
            }
        }
    }

    void SpawnObject()
    {
        if (prefabs.Length == 0) return;

        // �����_���ɑf�ނ�I��
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // �����ʒu�������_��
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnHeight, 0f);

        // ����
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        // �uFalling�v�^�O������Update�œ�������悤�ɂ���
        obj.tag = "Falling";
    }
}
