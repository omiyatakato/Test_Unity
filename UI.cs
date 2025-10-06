using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI : MonoBehaviour
{
    private Player_Move Player_Move;
    public Text Caloric_intake_text;//摂取
    [SerializeField]  RectTransform[] popUIs;
    [SerializeField] RectTransform[] Reslut;
    public Text Calorie_consumption_text;//消費
    [SerializeField] AudioSource se;
    [SerializeField] GameObject Panel;
    // Start is called before the first frame update
    public Player_Move Player_value;
    public Text[] Result_Text;
    public Text Total_calories;
    public Text Total_calories_result;
    public Text Total_Kg;
    public Text MAXCONBO;
    void Start()
    {
        ShowResult();
        float Total = Player_Move.Caloric_intake - Player_Move.Calorie_consumption;
        Result_Text[0].text = Player_Move.Caloric_intake.ToString("F1");
        Result_Text[1].text = Player_Move.Calorie_consumption.ToString("F1");
        Total_calories.text= (Player_Move.Caloric_intake - Player_Move.Calorie_consumption).ToString("F1");
        Total_calories_result.text = "体重=50kg+" +  Total + "/ 7200";
        Total_Kg.text = (50 + Total / 7200).ToString("F1");
        MAXCONBO.text = Player_Move.MAXConbo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void ShowResult()
    {
        float delay = 0.1f;

        foreach (RectTransform ui in popUIs)
        {
            ui.localScale = Vector3.zero; // 最初は非表示（縮小）

            ui.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .OnComplete(() => Debug.Log(ui.name + " 出た！"));

            delay += 0.5f; // 0.5秒ずつずらす
        }
    }
    public void Result()
    {
        Panel.SetActive(false);
        float delay = 0.1f;

        foreach (RectTransform ui in Reslut)
        {
            ui.localScale = Vector3.zero; // 最初は非表示（縮小）

            ui.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack)
            .SetDelay(delay)
            .OnComplete(() => Debug.Log(ui.name + " 出た！"));

            delay += 0.5f; // 0.5秒ずつずらす
        }
    }
}
