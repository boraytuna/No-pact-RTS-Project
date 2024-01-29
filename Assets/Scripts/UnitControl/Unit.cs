using UnityEngine;

public class Unit : MonoBehaviour
{

    void Start()
    {
        UnitSelections.Instance.unitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        if (UnitSelections.Instance != null)
        {
            UnitSelections.Instance.unitList.Remove(this.gameObject);
        }
    }

}