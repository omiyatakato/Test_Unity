using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]

public class foodEntity : ScriptableObject
{
    public string foodName;    // �H�ނ̖��O
    public Sprite icon;        // �H�ނ̃A�C�R��
    [TextArea(2, 3)]
    public string description; // ����
    public int Corse;
    // 0---�O��
    // 1---�X�[�v 
    // 2---��
    // 3---��
    // 4---���C��
    // 5---�T���_
    // 6---�f�U�[�g
    // 7---�h�����Npublic int Number;
    public bool Discovery=false;
   //public int Number;
    public int Amount;
}