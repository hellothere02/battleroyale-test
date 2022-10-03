using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private Vector2 planeSaze;
    [SerializeField] private Material planeMaterialat;
    public Vector2 PlaneSize { get => planeSaze; }

    private void Start()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = Vector3.zero;
        plane.transform.localScale = new Vector3(planeSaze.x, 1, planeSaze.y);
        plane.GetComponent<MeshRenderer>().material = planeMaterialat;
    }
}
