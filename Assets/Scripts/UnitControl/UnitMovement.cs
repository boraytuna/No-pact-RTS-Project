using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{

    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask terrain;

    private Vector3 destination; // Store the destination point

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        destination = transform.position; // Initialize destination to current position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrain))
            {
                destination = hit.point; // Update the destination point
            }
        }

        // Move towards the destination point
        myAgent.SetDestination(destination);
    }

    public void SetDestination(Vector3 position)
    {
        myAgent.SetDestination(position);
    }

}
