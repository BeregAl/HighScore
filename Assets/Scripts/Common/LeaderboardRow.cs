using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] private TMP_Text place;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text score;

    public void SetText(string place, string name, string score)
    {
        this.place.text = place;
        this.name.text = name;
        this.score.text = score;
    }
}
