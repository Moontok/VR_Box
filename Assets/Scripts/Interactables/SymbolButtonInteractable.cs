using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolButtonInteractable : ButtonInteractable
{
    [SerializeField] private string symbol = "";

    public string Symbol => symbol;
}
