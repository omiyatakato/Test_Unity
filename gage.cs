using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gage : MonoBehaviour
{
    public float fallSpeed = 5f;        // 落下速度
    public float cal_gage = 10f;     // 衝突時に増えるゲージ量
    public float Full_stomach_gage;
    public Player_Move Player_value;
    private void Start()
    {
        if (Player_value == null)
        {
            // 自動で探す（Playerタグを設定しておく）
            Player_value = GameObject.FindWithTag("Player").GetComponent<Player_Move>();
        }
    }
    void Update()
    {
        if (Player_value == null) return; // 念のため null チェック

        if (!Player_value.Bonus)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.down * 1.5f * fallSpeed * Time.deltaTime;
        }
        if (transform.position.y < -6f) // 画面外で削除
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのスクリプトにアクセスしてゲージを増やす
            Player_Move player = other.GetComponent<Player_Move>();
            if (player != null)
            {
                player.IncreaseGauge(cal_gage, Full_stomach_gage);
            }

            // 当たったらオブジェクト削除
            Destroy(gameObject);
        }
    }
}
