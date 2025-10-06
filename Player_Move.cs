using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Player_Move : MonoBehaviour
{
    public float speed = 5f; // 移動スピード
    private Rigidbody2D rb;
    private float lastAPressTime = 0f;
    private float lastDPressTime = 0f;
    public float dodgeDistance = 3f;   // 回避距離
    public float dodgeSpeed = 10f;     // 回避速度
    public float doubleTapTime = 0.3f; // ダブルクリック判定の時間
    public Slider speedSlider;
    private float lastTapTimeA = -1f;
    private float lastTapTimeD = -1f;
    private bool isDodging = false;
    private Vector3 dodgeTarget;
    private float Kg=50;
    public static float Caloric_intake;//摂取
    public static float Calorie_consumption;//消費
    public GameObject Game_Set_Panel;
    public Text Time_text;
    private float Game_Set=5;
    public Sprite[] Player_Change;
    public SpriteRenderer Player;
    public bool Bonus = false;
    private float Bonus_time;
    public BoxCollider2D playerCollider;
    private Vector2 defaultSize;
    private Vector3 defaultScale;
    public Text Kg_text;
    [SerializeField]  Text comboText;
    private int currentCombo = 0;
    private Tweener scaleTween;
    [SerializeField] private float comboResetTime = 2f; // リセットまでの時間
    private float timer = 0f;
    public static int MAXConbo=0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSize = playerCollider.size; // 初期サイズを保存
        defaultScale = transform.localScale;
    }


    void Update()
    {
        if (currentCombo > 0)
        {
            timer += Time.deltaTime;
            if (timer >= comboResetTime)
            {
                ResetCombo();
            }
        }
        Game_Set -= Time.deltaTime;
        Time_text.text= Game_Set.ToString("F0");
        if(Game_Set<=0)
        {
            Game_Set_Panel.SetActive(true);
        }
        // --- Aキーのダブルクリック判定 ---
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTimeA < doubleTapTime)
            {
                StartDodge(Vector3.left); // 左に回避
            }
            lastTapTimeA = Time.time;
        }

        // --- Dキーのダブルクリック判定 ---
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTimeD < doubleTapTime)
            {
                StartDodge(Vector3.right); // 右に回避
            }
            lastTapTimeD = Time.time;
        }

        // --- 回避中の移動 ---
        if (isDodging)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                dodgeTarget,
                dodgeSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, dodgeTarget) < 0.05f)
            {
                isDodging = false; // 到着したら終了
            }
        }
        // 入力を取得 (-1 ～ 1) A/D or ←/→キー
        float moveX = Input.GetAxisRaw("Horizontal");

        // velocityを直接セット（フレーム依存なし）
        if(!Bonus)
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(moveX * 1.3f*speed, rb.velocity.y);
        }
        speedSlider.value -= 0.01f;
        Calorie_consumption += 0.01f;
        Full_stomach_gage();
        Kg_text.text= "体重："+Kg.ToString("F1")+"Kg";

    }
    public void Full_stomach_gage()
    {
        if (speedSlider.value >= 100.0f && !Bonus)
        {
            Bonus = true;
            EnlargeCollider();
            transform.DOScale(defaultScale * 1.3f, 0.3f)
            .SetEase(Ease.OutBack);
        }
            if(Bonus)
            {
            speedSlider.value -= Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime+Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime + Time.deltaTime;
                Bonus_time += Time.deltaTime;
                Player.sprite = Player_Change[2];
                if(Bonus_time>5)
                {
                    Player.sprite = Player_Change[0];
                    Bonus_time = 0;
                    ResetCollider();
                transform.DOScale(defaultScale, 0.3f)
            .SetEase(Ease.OutBack);
                Bonus =false;
                }
            }
    }

    public void IncreaseGauge(float amount,float Full_stomach_gage)
    {
        Caloric_intake += amount;
        Kg += amount / 7200;
        if(!Bonus)
        speedSlider.value += Full_stomach_gage;

        AddCombo();
    }
    void EnlargeCollider()
    {
        playerCollider.size *= 1.2f;//２倍
    }

    // 元に戻す処理
    void ResetCollider()
    {
        playerCollider.size = defaultSize;  // 元の大きさに戻す
    }

    void StartDodge(Vector3 direction)
    {
        if (isDodging) return; // 回避中なら無効
        isDodging = true;
        dodgeTarget = transform.position + direction * dodgeDistance;
    }

    public void ResultPanelOn()
    {
        Game_Set_Panel.SetActive(false);
    }

    public void Reset()
    {
        Game_Set_Panel.SetActive(false);
    }

    public void AddCombo()
    {
        currentCombo++;

        if (currentCombo > MAXConbo)
            MAXConbo = currentCombo;
        comboText.text = currentCombo + " Combo!";
        // タイマーをリセット
        timer = 0f;
        // 既存アニメーションを止める（連打しても破綻しないように）
        if (scaleTween != null && scaleTween.IsActive())
        {
            scaleTween.Kill();
        }

        comboText.transform.localScale = Vector3.one;

        // ポンッと拡大して戻る
        scaleTween = comboText.transform.DOScale(1.3f, 0.2f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                comboText.transform.DOScale(1f, 0.2f).SetEase(Ease.InOutQuad);
            });
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        comboText.text = "";
        timer = 0f;
    }
}