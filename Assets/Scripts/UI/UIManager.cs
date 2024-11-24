using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the UI to display the current number of visitors.
/// </summary>
public class UIManager : MonoBehaviour
{
    // --- State ---
    private int nbVisitors = 0; // Tracks the total number of visitors
    
    // --- References ---
    [SerializeField] TMP_Text text = null; // Text element to display the visitor count
    
    /// <summary>
    /// Adds visitors and update the displayed count.
    /// </summary>
    /// <param name="nb">The number of visitors to add.</param>
    public void addVisitor(int nb)
    {
        nbVisitors += nb;
        text.text = "" + nbVisitors;
    }
    
    /// <summary>
    /// Removes visitors and update the displayed count.
    /// </summary>
    /// <param name="nb">The number of visitors to remove.</param>
    public void removeVisitor(int nb)
    {
        nbVisitors -= nb;
        text.text = "" + nbVisitors;
    }
}
