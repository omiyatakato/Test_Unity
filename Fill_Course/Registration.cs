using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Registration : MonoBehaviour
{
    [SerializeField] private FoodList foodList;
    [SerializeField] private GameObject Fill_Couse;
    [SerializeField] private GameObject Bar;
    [SerializeField] private GameObject Scroll;
    [SerializeField] private GameObject List_Food;
    public Image image;
    private Image slotImage;
    public static Registration Instance; // シングルトンで呼べるようにする
    [SerializeField] private Button confirmButton;
    private Image confirmImage;
   
    private void Awake()
    {
        Instance = this;
        confirmImage = confirmButton.GetComponent<Image>();
    }

    public void OnSlotSelected(Slot slot,bool ISBool)
    {
        if (ISBool)
        {
            Debug.Log("Yes1111");
            // 登録ボタンを点滅させる
            DOTween.Kill(confirmImage); // 既存のTweenを止める
            confirmImage.color = new Color(0.5f, 1f, 0.5f); ; // リセット
            confirmImage.DOFade(0.3f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            Debug.Log("No11");
            DOTween.Kill(confirmImage); // 点滅停止
            confirmImage.color = Color.white; // 元に戻す
        }
    }

    public void OnConfirm()
    {
        DOTween.Kill(confirmImage); // 点滅停止
        confirmImage.color = Color.white; // 元に戻す
        Debug.Log("登録確定！");
    }

   public void Rearrangement(int index)
    {
        // データを反映
      image.sprite = foodList.foods[index].icon;
    }
    public void Appetizer_List()
    {
        Fill_Couse.SetActive(false);
        Bar.SetActive(false);
        Scroll.SetActive(false);
        List_Food.SetActive(true);
    }
    public void Appetizer_ListOf()
    {
        Fill_Couse.SetActive(true);
        Bar.SetActive(true);
        Scroll.SetActive(true);
        List_Food.SetActive(false);
    }
}