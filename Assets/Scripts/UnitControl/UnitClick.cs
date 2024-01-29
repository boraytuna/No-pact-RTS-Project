using UnityEngine;
using UnityEngine.InputSystem.HID;

public class UnitClick : MonoBehaviour
{

    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        // Handling left-click selection
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // shift click
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else 
                {
                    //normal click
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelections.Instance.DeselectAll();
                }
            }
        }

        // Handling right-click commands
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("EnemyBuilding"))
                {
                    EnemyBuilding enemyBuilding = hit.collider.GetComponent<EnemyBuilding>();
                    if (enemyBuilding != null)
                    {
                        CommandUnitsToAttack(enemyBuilding);
                    }
                }
                else
                {
                    // Command units to move to the clicked position
                    CommandUnitsToMove(hit.point);
                }
            }
        }
    }

    private void CommandUnitsToAttack(EnemyBuilding enemyBuilding)
    {
        foreach (var unit in UnitSelections.Instance.unitsSelected)
        {
            IUnit unitScript = unit.GetComponent<IUnit>();
            if (unitScript != null)
            {
                unitScript.SetTarget(enemyBuilding);
            }
        }
    }

    private void CommandUnitsToMove(Vector3 position)
    {
        foreach (var unit in UnitSelections.Instance.unitsSelected)
        {
            UnitMovement unitMovement = unit.GetComponent<UnitMovement>();
            if (unitMovement != null)
            {
                unitMovement.SetDestination(position);
            }
        }
    }

    private void AttackEnemyBuilding(EnemyBuilding enemyBuilding)
    {
        var selectedUnits = UnitSelections.Instance.unitsSelected;
        foreach (var unit in selectedUnits)
        {
            IUnit unitScript = unit.GetComponent<IUnit>();
            if (unitScript != null)
            {
                unitScript.SetTarget(enemyBuilding);
            }
        }
    }

    //private void HandleUnitSelection(GameObject clickedObject)
    //{
    //    if (clickedObject.CompareTag("ControllableUnit"))
    //    {
    //        if (Input.GetKey(KeyCode.LeftShift))
    //        {
    //            // Shift clicked for multi-selection
    //            UnitSelections.Instance.ShiftClickSelect(clickedObject);
    //        }
    //        else
    //        {
    //            // Single selection
    //            UnitSelections.Instance.ClickSelect(clickedObject);
    //        }

    //        // Stop actions of previously selected units
    //        DeselectOtherUnits(clickedObject);
    //    }
    //}

    //private void DeselectOtherUnits(GameObject newlySelectedUnit)
    //{
    //    var selectedUnits = UnitSelections.Instance.unitsSelected;
    //    foreach (var unit in selectedUnits)
    //    {
    //        if (unit != newlySelectedUnit)
    //        {
    //            IUnit unitScript = unit.GetComponent<IUnit>();
    //            if (unitScript != null)
    //            {
    //                unitScript.StopAction(); // Stop the actions of previously selected units
    //            }
    //        }
    //    }
    //}

    //private void HandleDeselection()
    //{
    //    if (!Input.GetKey(KeyCode.LeftShift))
    //    {
    //        UnitSelections.Instance.DeselectAll();

    //        // Stop actions of deselected units
    //        var selectedUnits = UnitSelections.Instance.unitsSelected;
    //        foreach (var unit in selectedUnits)
    //        {
    //            IUnit unitScript = unit.GetComponent<IUnit>();
    //            if (unitScript != null)
    //            {
    //                unitScript.StopAction();
    //            }
    //        }
    //    }
    //}

}

