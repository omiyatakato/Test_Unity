using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    public GameObject[] prefabs;     // 落としたい素材を複数入れる
    public float spawnInterval = 1f; // 生成間隔（秒）
    public float fallSpeed = 5f;     // 落下速度
    public float spawnRangeX = 8f;   // X方向のランダム幅
    public float spawnHeight = 6f;   // 生成Y位置

    private float timer = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }

        // 生成済みのオブジェクトを下に移動
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Falling"))
        {
            obj.transform.position += Vector3.down * fallSpeed * Time.deltaTime;

            // 画面外に行ったら削除
            if (obj.transform.position.y < -spawnHeight)
            {
                Destroy(obj);
            }
        }
    }

    void SpawnObject()
    {
        if (prefabs.Length == 0) return;

        // ランダムに素材を選ぶ
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        // 生成位置をランダム
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnHeight, 0f);

        // 生成
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        // 「Falling」タグをつけてUpdateで動かせるようにする
        obj.tag = "Falling";
    }
}
