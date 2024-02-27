using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ararstar e soltar
public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged; // objeto sendo arrastado
	public static int valor; // onde é usado?
	public Vector3 startPosition; // posicao inicial do objeto arrastado
	public static Transform startParent; // pai original do objeto sendo arrastado
	public GameObject cubo1;
	public GameObject cubo10;
	public GameObject cubo100;

	#region IBeginDragHandler implementation

	// começa a arrastar
	public void OnBeginDrag (PointerEventData eventData)
	{
		Debug.Log("Início do arrasto - Objeto sendo arrastado: " + gameObject.name);
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		//GetComponent<CanvasGroup> ().blocksRaycasts = false;

	}

	#endregion

	#region IDragHandler implementation

	
	public void OnDrag (PointerEventData eventData) // erro movimento
	{
		//Debug.Log("movimentando");
		transform.position = Input.mousePosition; 
	}

	#endregion

	#region IEndDragHandler implementation

	// termina de arrastar
	public void OnEndDrag (PointerEventData eventData)
	{
		Debug.Log("terminou de arrastar");
		// Debug.Log (transform.parent.tag); = certo

		if (transform.parent.tag == "lixo") {
			
			if (startParent.tag == "1" || startParent.tag == "10" || startParent.tag == "100") {

				Debug.Log("Instanciando novo cubo"); // erro é aqui? instantiate não funciona e startParent

				Instantiate (transform, startParent);
				transform.name = transform.name.Replace("(Clone)", "");
				transform.name.Trim();
			}
			Destroy (itemBeingDragged);
		}
		if (startParent.tag == "1" && startParent.childCount == 0) {
			Debug.Log("Instanciando novo cubo 1");
			Transform blocoNovo = Instantiate(cubo1, startParent).transform;
			// blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
			Debug.Log("Clone de cubo1 instanciado com sucesso!");
		} else if (startParent.tag == "10" && startParent.childCount == 0) {
			Debug.Log("Instanciando novo cubo 10");
			Transform blocoNovo = Instantiate(cubo10, startParent).transform;
			blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
			Debug.Log("Clone de cubo10 instanciado com sucesso!");
		} else if (startParent.tag == "100" && startParent.childCount == 0) {
			Debug.Log("Instanciando novo cubo 100");
			Transform blocoNovo = Instantiate(cubo100, startParent).transform;
			blocoNovo.name = blocoNovo.name.Replace("(Clone)", "");
			blocoNovo.name.Trim();
			Debug.Log("Clone de cubo100 instanciado com sucesso!");
		}
			Debug.Log("Fim do arrasto - Objeto arrastado: " + itemBeingDragged.name);
			itemBeingDragged = null;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == startParent && transform.parent.tag!="lixo") {
				Debug.Log("Restaurando posição inicial");
				transform.position = startPosition;
			}

		Debug.Log("Executando HasChang"); // evento de mudança, leva p/ o inventory
		ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());

	}

	#endregion
}