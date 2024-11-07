using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int nbVisitors = 0;
    
    [SerializeField] TMP_Text text = null;
    
    public void addVisitor(int nb)
    {
        nbVisitors += nb;
        text.text = "" + nbVisitors;
    }
    
    public void removeVisitor(int nb)
    {
        nbVisitors -= nb;
        text.text = "" + nbVisitors;
    }
}
