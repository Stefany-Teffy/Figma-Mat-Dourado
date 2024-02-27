// recebe um numero, analisa ele e com base nisso passa para a proxima cena ou mostra mensagem de erro
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class numMax : MonoBehaviour
{

    public static int num = -1;
    public string valor;
    public Transform erroVal;
	public Text textValSel;

    void Start()
    {
     num=-1;   
    }

	// verifica e converte um valor de string em um numero inteiro
    public void verificaNum (string v)
	{
		if (v == "")
			valor = "-1";
		else
			valor = v;
		Debug.Log(v);
		num = int.Parse(valor); // Converte a string para um numero inteiro e armazena em "num"
	}

	// método chamado quando um botao e pressionado
    public void botok()
	{
		bool converte = int.TryParse(textValSel.text, out num);
		if (!converte)
		{
			num = -1; // Se a conversao falhar, define "num" como -1.
		}
		// Ativa ou desativa um texto de aviso com base no valor de "num"
        if (num < 0)
			erroVal.gameObject.SetActive(true);
		else
			erroVal.gameObject.SetActive(false);

        if (num > 0)
			MudaCena.proxCena("MatDourado");
	}
}
