// realiza conta, com base nos valores e operacoes passa ou nao para a proxima cena

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

// faz o op2 funcionar
public class GetOp2 : MonoBehaviour
{
	public ToggleGroup simOp;
	public Toggle simMais, simMenos;
	public Button ok;
	public static int botaoVal1 = -1;
	public static int botaoVal2 = -1;
	public int tipo;
	public string valor1;
	public string valor2;
	public static int opcao = 0;
	public static int valorOp;
	public static string operacao;
	public Transform erroVal1, erroVal2, erroOp;
	public Text textValor1, textValor2;


	// Inicialização
	void Start()
	{
		botaoVal1 = -1;
		botaoVal2 = -1;
		opcao = 0;
	}

	// Obtém o valor do primeiro operando
	public void GetOp1(string v1)
	{
		if (v1 == "")
			valor1 = "-1";
		else
			valor1 = v1;
		Debug.Log(v1);
		botaoVal1 = int.Parse(valor1);
	}

	 // Obtém o valor do segundo operando
	public void GetOpe2(string v2)
	{
		if (v2 == "")
			valor2 = "-1";
		else
			valor2 = v2;
		Debug.Log(v2);
		botaoVal2 = int.Parse(valor2);
	}

	// Obtém a opção selecionada (adicao ou subtracao)
	public void GetOption(int ope)
	{
		opcao = ope; 
		
	}

	 // Lógica executada ao clicar no botão "Ok"
	public void botaoOk()
	{
		// Tenta converter os valores dos campos de texto em inteiros

		bool converte = int.TryParse(textValor1.text, out botaoVal1);
		if (!converte)
		{
			botaoVal1 = -1;
		}

		converte = int.TryParse(textValor2.text, out botaoVal2);
		if (!converte)
		{
			botaoVal2 = -1;
		}

		// Realiza a operação com base na opção selecionada
		if (opcao == 1)
		{
			valorOp = botaoVal1 + botaoVal2;
			operacao = botaoVal1 + "+" + botaoVal2;
		}
		else if (opcao == 2)
		{
			
			// Verifica se o segundo operando é maior que o primeiro
			if (botaoVal2 > botaoVal1)
			{
				erroVal2.gameObject.SetActive(true);
				erroVal1.gameObject.SetActive(true);
				//interrompe aqui a funcao
				return;
			}
			else
				valorOp = botaoVal1 - botaoVal2;
			//Debug.Log(valorOp);
			operacao = botaoVal1 + "-" + botaoVal2;
		}
		 // se tiver algo errado surge mesg de erro caso contrario ela fica escondida
		if (opcao == 0)
			erroOp.gameObject.SetActive(true);
		else
			erroOp.gameObject.SetActive(false);
		if (botaoVal1 < 0)
			erroVal1.gameObject.SetActive(true);
		else
			erroVal1.gameObject.SetActive(false);
		if (botaoVal2 < 0)
			erroVal2.gameObject.SetActive(true);
		else
			erroVal2.gameObject.SetActive(false);

		// se der tudo certo, vai para a proxima cena
		if (botaoVal1 >= 0 && botaoVal2 >= 0 && opcao != 0)
			MudaCena.proxCena("MatDourado");
	}

}