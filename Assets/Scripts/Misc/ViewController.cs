using UnityEngine;

public class ViewController: MonoBehaviour
{
    public GameObject unitView;
    public GameObject selectUnit;
    public void OpenUnitView(int formationIndex = -1, int inventoryIndex = -1, int selectHeroIndex = -1) {
        unitView.GetComponent<UnitView>().formationIndex = formationIndex;
        unitView.GetComponent<UnitView>().inventoryIndex = inventoryIndex;
        unitView.GetComponent<UnitView>().selectHeroIndex = selectHeroIndex;
        unitView.SetActive(true);
    }
    public void OpenSelectUnit(int formationIndex = -1, int inventoryIndex = -1) {
        selectUnit.GetComponent<SelectUnit>().formationIndex = formationIndex;
        selectUnit.GetComponent<SelectUnit>().inventoryIndex = inventoryIndex;
        selectUnit.SetActive(true);
    }
}
