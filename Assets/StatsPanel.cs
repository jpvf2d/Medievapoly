using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] public TextMeshPro money, location;
    [SerializeField] private Player playerStats;
    public Transform[] boardSpaces;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
    }

    void SetStats()
    {
        money.text = "$" + playerStats.money.ToString();
        location.text = playerStats.boardSpaceIndex.ToString(); // Change to names once cards are in game
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();
    }
}
