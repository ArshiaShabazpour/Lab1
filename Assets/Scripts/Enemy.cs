using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector2 target;
    private bool alive = true;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            pointA = new GameObject("A").transform;
            pointB = new GameObject("B").transform;
            pointA.position = transform.position + Vector3.left * 2f;
            pointB.position = transform.position + Vector3.right * 2f;
        }
        target = pointB.position;
    }

    void Update()
    {
        if (!alive) return;

        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            if (Vector2.Distance(target, pointA.position) < 0.01f)
                target = pointB.position;
            else
                target = pointA.position;
        }
    }


    public void OnStomped()
    {
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        var rend = GetComponent<SpriteRenderer>();
        if (rend != null) rend.enabled = false;
        AudioManager.Instance.PlayEnemyStomp();
        Destroy(gameObject, 0.2f);
    }

    public void InitializePatrol(Vector2 a, Vector2 b)
    {
        if (pointA == null) pointA = new GameObject(name + "_A").transform;
        if (pointB == null) pointB = new GameObject(name + "_B").transform;

        pointA.position = a;
        pointB.position = b;
        target = pointB.position;
    }
}
