using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Player_Move : MonoBehaviour
{
    public float speed = 5f; // �ړ��X�s�[�h
    private Rigidbody2D rb;
    private float lastAPressTime = 0f;
    private float lastDPressTime = 0f;
    public float dodgeDistance = 3f;   // �������
    public float dodgeSpeed = 10f;     // ��𑬓x
    public float doubleTapTime = 0.3f; // �_�u���N���b�N����̎���
    public Slider speedSlider;
    private float lastTapTimeA = -1f;
    private float lastTapTimeD = -1f;
    private bool isDodging = false;
    private Vector3 dodgeTarget;
    private float Kg=50;
    public static float Caloric_intake;//�ێ�
    public static float Calorie_consumption;//����
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
    [SerializeField] private float comboResetTime = 2f; // ���Z�b�g�܂ł̎���
    private float timer = 0f;
    public static int MAXConbo=0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultSize = playerCollider.size; // �����T�C�Y��ۑ�
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
        // --- A�L�[�̃_�u���N���b�N���� ---
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTimeA < doubleTapTime)
            {
                StartDodge(Vector3.left); // ���ɉ��
            }
            lastTapTimeA = Time.time;
        }

        // --- D�L�[�̃_�u���N���b�N���� ---
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTimeD < doubleTapTime)
            {
                StartDodge(Vector3.right); // �E�ɉ��
            }
            lastTapTimeD = Time.time;
        }

        // --- ��𒆂̈ړ� ---
        if (isDodging)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                dodgeTarget,
                dodgeSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, dodgeTarget) < 0.05f)
            {
                isDodging = false; // ����������I��
            }
        }
        // ���͂��擾 (-1 �` 1) A/D or ��/���L�[
        float moveX = Input.GetAxisRaw("Horizontal");

        // velocity�𒼐ڃZ�b�g�i�t���[���ˑ��Ȃ��j
        if(!Bonus)
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(moveX * 1.3f*speed, rb.velocity.y);
        }
        speedSlider.value -= 0.01f;
        Calorie_consumption += 0.01f;
        Full_stomach_gage();
        Kg_text.text= "�̏d�F"+Kg.ToString("F1")+"Kg";

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
        playerCollider.size *= 1.2f;//�Q�{
    }

    // ���ɖ߂�����
    void ResetCollider()
    {
        playerCollider.size = defaultSize;  // ���̑傫���ɖ߂�
    }

    void StartDodge(Vector3 direction)
    {
        if (isDodging) return; // ��𒆂Ȃ疳��
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
        // �^�C�}�[�����Z�b�g
        timer = 0f;
        // �����A�j���[�V�������~�߂�i�A�ł��Ă��j�]���Ȃ��悤�Ɂj
        if (scaleTween != null && scaleTween.IsActive())
        {
            scaleTween.Kill();
        }

        comboText.transform.localScale = Vector3.one;

        // �|���b�Ɗg�債�Ė߂�
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