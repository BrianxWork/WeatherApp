using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableImage : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		SearchSystem.Instance.ClearSearchResults();
		SearchSystem.Instance.ClearSearchBarText();
		SearchSystem.Instance.SetSearchWindow(false);

	}
}
