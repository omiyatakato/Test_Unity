using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Slot : MonoBehaviour
{
    private foodEntity food;
    private int index;
    private bool isSelected = false;

    [SerializeField] private Button button;
    private Image slotImage;
    private Color slotImage_Normal;

    public void Initialize(foodEntity f, int i)
    {
        food = f;
        index = i;

        button.onClick.AddListener(OnClick);
    }

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    private void Start()
    {
        slotImage_Normal = slotImage.color;
    }

    private void OnClick()
    {
        if (!food.Discovery) return; // 所持していない場合は無効

        // 選択状態を反転
        isSelected = !isSelected;

        // 状態に応じて色とスケールを変更
        slotImage.color = isSelected ? Color.green : slotImage_Normal;
        transform.localScale = isSelected ? Vector3.one * 1.2f : Vector3.one;

        Debug.Log(isSelected ? food.foodName + " 選択" : food.foodName + " 非選択");

        // Registration 側に通知（登録ボタン点滅など）
        Registration.Instance.OnSlotSelected(this, isSelected);
    }
}