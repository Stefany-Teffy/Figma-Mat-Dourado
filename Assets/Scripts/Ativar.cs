//ativa o painel escondido da vitória
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Ativar : MonoBehaviour {
	public GameObject mod;
	public GameObject mat;
	public static GameObject modal;
	public static GameObject jogoMat;

	// Use this for initialization
	void Start () {
		modal = mod;
		jogoMat = mat;
		Debug.Log ("Modal: " + modal.transform.name);
	}

	public void desativar(){
		jogoMat.SetActive (true);
		modal.SetActive (false);
		Debug.Log ("Modal desativado");
	}

	public static void ativarModal(){
		modal.SetActive (true);
		jogoMat.SetActive (false);
		Debug.Log ("Modal ativado");
	}
	
	 // Método para jogar novamente


	public void PlayAgain()
	{
    // Verifica a cena anterior e carrega a cena correspondente
    	if (MudaCena.nomeAnt == "Selecao")
    	{
        	SceneManager.LoadScene("Selecao");
    	}
    	else if (MudaCena.nomeAnt == "Operacao")
    	{
       	 SceneManager.LoadScene("Operacao");
    	}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
