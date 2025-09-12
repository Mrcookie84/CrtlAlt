using System;
using TMPro;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    
    [SerializeField]private GameObject player;
    public TMP_Text text;


    private void Start()
    {
        text.text = "height : " + player.transform.localPosition.y;
    }

    private void Update()
    {
        text.text = "height : " + player.transform.localPosition.y;
    }
}
