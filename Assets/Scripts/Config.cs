using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {
    public GameObject botComSom;
    public GameObject botSemSom;

    public void ClicouSom(){
        if(botComSom.activeSelf)
        {
            botComSom.SetActive(false);
            botSemSom.SetActive(true);
            Debug.Log("Som desativado");
        }
        else
        {
            botComSom.SetActive(true);
            botSemSom.SetActive(false);
            Debug.Log("Som ativado");
        }
    }
}
