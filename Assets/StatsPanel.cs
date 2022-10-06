using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] public TextMeshPro money, location;
    [SerializeField] private Player playerStats;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    void SetStats()
    {
        money.text = "$" + playerStats.money.ToString();
        //location.text = playerStats.currentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();
    }
}
