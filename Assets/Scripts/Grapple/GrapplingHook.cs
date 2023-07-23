using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform grapplingHook;
    [SerializeField] private Transform grapplingHookEndPoint;
    [SerializeField] private Transform handPos;
    [SerializeField] private Transform playerBody;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private float maxGrappleDist;
    [SerializeField] private float hookSpeed;
    [SerializeField] private Vector3 offset;

  
    bool isShooting, isGrappling;

    private Vector3 hookPoint;

    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        isGrappling = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (grapplingHook.parent == handPos)
        {
            grapplingHook.localPosition = Vector3.zero;
            grapplingHook.localRotation = Quaternion.Euler(new Vector3(0.027f,0f,0f));

        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            ShootHook();
        }

        if (isGrappling)
        {
            grapplingHook.position = Vector3.Lerp(grapplingHook.position, hookPoint, hookSpeed * Time.deltaTime);
            if (Vector3.Distance(grapplingHook.position, hookPoint) < .5f)
            {
                playerBody.position = Vector3.Lerp(playerBody.position, hookPoint - offset, hookSpeed * Time.deltaTime);
                
                if (Vector3.Distance(grapplingHook.position, hookPoint - offset) < .5f)
                {
                    isGrappling = false;
                    grapplingHook.SetParent(handPos);
                    lineRenderer.enabled = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        lineRenderer.SetPosition(0, grapplingHookEndPoint.position);
        lineRenderer.SetPosition(1, handPos.position);
    }

    void ShootHook()
    {
        if (isShooting || isGrappling) return;

        isShooting = true;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxGrappleDist, grappleLayer))
        {
            hookPoint = hit.point;
            isGrappling = true;
            grapplingHook.parent = null;
            grapplingHook.LookAt(hookPoint);
            lineRenderer.enabled = true;
            Debug.Log("HIT");
        }

        isShooting = false;
    }
}
