using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFiller : MonoBehaviour
{

    [SerializeField] InputField PlayerNameText;
    [SerializeField] Text PlayerScoreText;

    void Start()
    {
        var data = UIDataManager.Instance.GetStoredPlayerData();
        if (data == null)
        {
            return;
        }

        PlayerNameText.text = data.Name;

        PlayerScoreText.gameObject.SetActive(true);
        PlayerScoreText.text = $"Player {data.Name} best scores: {data.Score.ToString()}";
    }
    
}

