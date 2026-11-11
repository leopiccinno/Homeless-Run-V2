using System;
using UnityEngine;

public class CoinPickup : BasePickup
{
    public static event Action CoinCollected;

    protected override void OnPickup()
    {
        CoinCollected?.Invoke();
    }
}