using UnityEngine;

[RequireComponent(typeof(NavMeshAgentController))]
public class BaseUnit : MonoBehaviour
{
    [SerializeField] private Color[] availableColors;


    private NavMeshAgentController navMeshAgentController;
    private new Renderer renderer;
    private Color color;

    public Color Color {get { return color; } }

    private void Awake()
    {
        navMeshAgentController = GetComponent<NavMeshAgentController>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        Initiliaze();
    }

    private void Initiliaze()
    {
        color = availableColors[Random.Range(0, availableColors.Length)];
        renderer.material.color = color;


        Vector3 initialPoition = new Vector3(Random.Range(-25.0f, 25.0f), 0, Random.Range(-25.0f, 25.0f));
        navMeshAgentController.WarpPosition(initialPoition);

        SeekDestination();
    }

    private void OnEnable()
    {
        navMeshAgentController.OnReachDestination += SeekDestination;
    }

    private void OnDisable()
    {
        navMeshAgentController.OnReachDestination -= SeekDestination;
    }

    private void SeekDestination()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-25.0f,25.0f), 0, Random.Range(-25.0f, 25.0f));        
        navMeshAgentController.SetDestination(randomPosition);
    }
}
