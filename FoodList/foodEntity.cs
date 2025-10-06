using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]

public class foodEntity : ScriptableObject
{
    public string foodName;    // 食材の名前
    public Sprite icon;        // 食材のアイコン
    [TextArea(2, 3)]
    public string description; // 説明
    public int Corse;
    // 0---前菜
    // 1---スープ 
    // 2---魚
    // 3---肉
    // 4---メイン
    // 5---サラダ
    // 6---デザート
    // 7---ドリンクpublic int Number;
    public bool Discovery=false;
   //public int Number;
    public int Amount;
}