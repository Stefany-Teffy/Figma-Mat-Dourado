// Junta os quadrados, verifica o resultado, mostra os valores e o resultado de vencedor na tela
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour, IHasChanged {

	// Declaracao de variaveis estaticas para slots e textos
	[SerializeField] static Transform slots;
	[SerializeField] static Text inventoryText;
	[SerializeField] static Text opText;
	[SerializeField] static Text numText;

	// Declaracao de variaveis de resposta, contadores e outros
	public static int resposta;
	public int x, nb10 = 0, nb100 = 0, nb1=0,  i, y;
	public string nome;
	public Transform slotfinal;
	public GameObject cubo1;
	public GameObject cubo10;
	public GameObject cubo100;
	public int sub;

	
	void Start () {
		
		// Encontra e associa objetos de texto e slots
		opText = GameObject.Find("textValOp").GetComponent<Text> ();
		numText = GameObject.Find("textValSel").GetComponent<Text> ();
		inventoryText = GameObject.Find ("textTotal").GetComponent<Text> ();
		slots = GameObject.Find ("caixaPecas").transform;

		// Obtem as respostas de outras classes
		resposta = GetOp2.valorOp;

		// teste 
		Debug.Log("resposta: " + resposta);
		Debug.Log("resposta2: " + numMax.num);

		 // Verifica a cena anterior e define a visibilidade de textos com base nela
		if (MudaCena.nomeAnt.ToString () == "Opcoes") {
			opText.enabled=false;
			resposta = -1;
		}			
		else if (MudaCena.nomeAnt == "Selecao") {
			opText.enabled=false;
			numText.enabled=true;
		}
		else if (MudaCena.nomeAnt == "Operacao") {
			if (GetOp2.opcao == 2) {
				slotfinal = GameObject.Find ("SlotFinal").transform;
				if (GetOp2.botaoVal1 > GetOp2.botaoVal2)
					sub = GetOp2.botaoVal1;
				else
					sub = GetOp2.botaoVal2;
				// Divide "sub" em centenas, dezenas e unidades e cria objetos com base nesses valores
				int subc, subd, subu;
				if (sub >= 100) {
					subc = (int)sub / 100;
					sub = sub - subc * 100;
					for (x = 0; x < subc; x++)
						Instantiate (cubo100, slotfinal);
				}
				if (sub >= 10) {
					subd = (int)sub / 10;
					sub = sub - subd * 10;
					for (x = 0; x < subd; x++)
						Instantiate (cubo10, slotfinal);
				}
				if (sub >= 1) {
					subu = sub;
					sub = sub - subu;
					for (x = 0; x < subu; x++)
						Instantiate (cubo1, slotfinal);
				}

			}
		}
		HasChanged ();
	}

	#region IHasChanged implementation
	public void HasChanged ()
	{
		 // Atualiza o texto "OPERACAO" com base nas configuracoes
		if (MudaCena.nomeAnt == "Operacao")
		{
			opText.text = "OPERAÇÃO: " + GetOp2.operacao.ToString();
			if (GetOp2.opcao == 2)
			{
				slotfinal = GameObject.Find("SlotFinal").transform;
				nb1 = 0;
				nb10 = 0;
				nb100 = 0;
				foreach (Transform bloquinho in slotfinal)
				{
					if (bloquinho.tag == "10")
					{
						nb10++;
						i = slotfinal.GetSiblingIndex();
					}
					else if (bloquinho.tag == "100")
					{
						nb100++;
						y = slotfinal.GetSiblingIndex();
					}
					else if (bloquinho.tag == "1")
						nb1++;
				}
				if (nb100 >= 1 && nb10 == 0)
				{
					Transform bDestruir100 = slotfinal.Find("cubo100(Clone)");
					DestroyImmediate(bDestruir100.gameObject);
					for (x = 0; x < 10; x++)
						Instantiate(cubo10, slotfinal);
				}
				if (nb10 >= 1 && nb1 == 0)
				{
					Transform bDestruir10 = slotfinal.Find("cubo10(Clone)");
					DestroyImmediate(bDestruir10.gameObject);
					for (x = 0; x < 10; x++)
						Instantiate(cubo1, slotfinal);
				}

			}
		}
		else if (MudaCena.nomeAnt == "Selecao")
		{
			 // Atualiza o texto "VALOR SELECIONADO" com base no valor de "numMax.num"
			numText.text = "VALOR SELECIONADO: " + numMax.num.ToString();
			

			// teste 
			Debug.Log("resposta: " + resposta);
			Debug.Log("resposta2: " + numMax.num);
		} // até aqui certo
            AtualizarValor();
		#endregion
	}

	// Método estático para atualizar o valor total e verificar se é uma resposta correta
	public static void AtualizarValor(){
		int valortotal = 0;
		foreach (Transform slotTransform in slots) {
			for (int i = 0; i < slotTransform.childCount; i++) 
				valortotal += int.Parse(slotTransform.GetChild(i).gameObject.tag);
		}
 		// Exibe o valor total no console e na tela.
		Debug.Log ("Valor no slot: " + valortotal);
		inventoryText.text = "VALOR TOTAL: " + valortotal.ToString();
		// Verifica se o valor total corresponde a alguma das respostas e ativa um modal se corresponder
		if (MudaCena.nomeAnt == "Operacao") {
			if (resposta == valortotal)
			{
				inventoryText.color = Color.green;
				Ativar.ativarModal();
			}
			else
			{
				Debug.Log("Valor incorreto");
				inventoryText.color = Color.white;
			}
		}
        if (MudaCena.nomeAnt == "Selecao")
        {
            if (numMax.num == valortotal)
            {
                inventoryText.color = Color.green;
                Ativar.ativarModal(); 
            }
            else
            {
                Debug.Log("Valor incorreto");
                inventoryText.color = Color.white;
            }
        }

    }
}
// Interface para notificar mudanças
namespace UnityEngine.EventSystems{
	public interface IHasChanged: IEventSystemHandler{
		void HasChanged ();
	}
}