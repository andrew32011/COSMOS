using UnityEngine;

public class SpawnOnCircle : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    private Collider2D circleCollider;
    private Vector3 spawnPosition;
    private bool isObjectSelected = false;
    private int ObjToSpw;
    void Start()
    {
        circleCollider = GetComponent<Collider2D>();
    }

    public void _Pressed (int _NumOfObj)
    {
        isObjectSelected = true;
        ObjToSpw = _NumOfObj;
    }
    void Update()
    {
        if (isObjectSelected)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject(ObjToSpw);
            }
            else
            {
                MoveObject();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SelectObject();
            }
        }
    }

    void SelectObject()
    {
        Vector3 inputPosition = Input.mousePosition;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }

        inputPosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        Vector3 circleCenter = circleCollider.bounds.center;
        float radius = circleCollider.bounds.extents.x; // ��������������, ��� ��������� ����� ����� ������ ������� �� X � Y

        spawnPosition = circleCenter + (worldPosition - circleCenter).normalized * radius;
        isObjectSelected = true;
    }

    void MoveObject()
    {
        Vector3 inputPosition = Input.mousePosition;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }

        inputPosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        Vector3 circleCenter = circleCollider.bounds.center;
        float radius = circleCollider.bounds.extents.x; // ��������������, ��� ��������� ����� ����� ������ ������� �� X � Y

        Vector3 direction = worldPosition - circleCenter;
        direction.z = 0f;
        spawnPosition = circleCenter + direction.normalized * radius;
        //Collider2D[] colliders = Physics2D.OverlapBoxAll(spawnPosition, new Vector2(radius, radius), 0f); // ��������� ��� ���������� � ������� ������

        //foreach (Collider2D collider in colliders)
        //{
        //    if (collider.GetComponent<SpwnObj>() != null)
        //    {
        //        // ���� ��������� ��������� ������� SpwnObj, �� ��������� ������� ������
        //        return;
        //    }
        //}
    }

    void PlaceObject(int Obj)
    {
        
        if (objectsToSpawn[Obj].GetComponent<SpwnObj>().Cost <= PointsCounter.Points)
        {
            FindFirstObjectByType<PointsCounter>().AddPoints(-objectsToSpawn[Obj].GetComponent<SpwnObj>().Cost);
            Vector3 _circleCenter = circleCollider.bounds.center;
            Vector3 _direction = spawnPosition - _circleCenter;
            float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg; // ��������� ���� ����� ������������ � ���� X
            Collider2D[] colliders = Physics2D.OverlapBoxAll(spawnPosition, new Vector2(objectsToSpawn[Obj].GetComponent<BoxCollider2D>().bounds.size.x, objectsToSpawn[Obj].GetComponent<BoxCollider2D>().bounds.size.y), _angle);
            foreach (Collider2D collider in colliders)
            {
                if (collider.GetComponent<SpwnObj>() != null)
                {
                    // ���� ��������� ��������� ������� SpwnObj, �� ������� ����� ������
                    return;
                }
            }
            GameObject newObject = Instantiate(objectsToSpawn[Obj].gameObject, spawnPosition, Quaternion.identity);

            Vector3 circleCenter = circleCollider.bounds.center;
            Vector3 direction = spawnPosition - circleCenter;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // ��������� ���� ����� ������������ � ���� X

            newObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // ������������ ������ � ������� ����������

            isObjectSelected = false;
        }
        
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(circleCollider.bounds.center, circleCollider.bounds.extents.x);

    //    if (isObjectSelected)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawWireSphere(spawnPosition, 0.5f);
    //    }
    //}
}