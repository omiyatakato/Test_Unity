using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodListUI : MonoBehaviour
{
    [SerializeField] private Transform contentParent; // ScrollViewのContent
    [SerializeField] private GameObject foodSlotPrefab; // 1個分のUIプレハブ
    [SerializeField] private List<foodEntity> foods; // 登録済みの食材たち
    public List<foodEntity> Foods => foods;
    void Start()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var food in foods)
        {
            if (food.Discovery)
            {
                
                // スロット生成
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // 子オブジェクトを探す
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // データを反映
                icon.sprite = food.icon;
                name.text = food.foodName;
            }
            else
            {
                // スロット生成
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // 子オブジェクトを探す
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // データを反映
                icon.sprite = food.icon;
                icon.color = new Color(0f, 0f, 0f, 0.5f);
                name.text = "???";
            }
        }
    }
    public void AllRearrangement()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var food in foods)
        {
            if(food.Discovery)
            {
               // スロット生成
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // 子オブジェクトを探す
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // データを反映
                icon.sprite = food.icon;
                name.text = food.foodName;
            }
            else
            {
                // スロット生成
                var slot = Instantiate(foodSlotPrefab, contentParent);

                // 子オブジェクトを探す
                var icon = slot.transform.Find("Icon").GetComponent<Image>();
                var name = slot.transform.Find("Name").GetComponent<Text>();

                // データを反映
                icon.sprite = food.icon;
                icon.color = new Color(0f, 0f, 0f, 0.5f);
                name.text = "???";
            }
        }
    }
    public void Rearrangement(int couse)
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < foods.Count; i++)
        {
            var food = foods[i];

            if (food.Corse != couse) continue;

            // スロット生成
            var slotObj = Instantiate(foodSlotPrefab, contentParent);
            var slot = slotObj.GetComponent<Slot>();

            // Slot に food と index を渡す
            slot.Initialize(food, i);

            // 子オブジェクトを探す
            var icon = slotObj.transform.Find("Icon").GetComponent<Image>();
            var name = slotObj.transform.Find("Name").GetComponent<Text>();

            icon.sprite = food.icon;

            if (food.Discovery)
            {
                name.text = food.foodName;
                icon.color = Color.white;
            }
            else
            {
                name.text = "???";
                icon.color = new Color(0f, 0f, 0f, 0.5f);
            }
        }
    }
}
