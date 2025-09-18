using System;
using UnityEngine;

public class CinematiqueScript : MonoBehaviour
{
    
    
    public GameObject[] listeImage;
    public int count;

    private void Start()
    {
        count = listeImage.Length;
    }


    public void LaunchCinematique()
    {
        foreach (var image in listeImage)
        {
            
        }
        
        
    }

}
