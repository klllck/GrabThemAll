using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private LineRenderer line;

    private List<Waypoint> path;
    private Vector2 lastTouchedPosition;
    private bool isMoving;
    private int totalCoins = 0;

    private void Start()
    {
        path = new List<Waypoint>();
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UIController.Instance.TotalCoinsTxt.text = $"{totalCoins}";

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            var touch = Input.GetTouch(0);
            lastTouchedPosition = Camera.main.ScreenToWorldPoint(touch.position);
            var newWaypoint = Instantiate(waypoint, lastTouchedPosition, Quaternion.identity);
            path.Add(newWaypoint);
            isMoving = true;
        }

        if (isMoving && path.Count > 0)
        {
            SetPlayerMovePath();

            var nextWaypoint = path[0];
            transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == nextWaypoint.transform.position)
            {
                path.Remove(nextWaypoint);
                Destroy(nextWaypoint.gameObject);
            }
        }
        else
        {
            isMoving = false;
        }
    }

    private void SetPlayerMovePath()
    {
        var linePoints = path.Select(wPoint => wPoint.transform).ToList();
        linePoints.Insert(0, transform);

        line.positionCount = linePoints.Count;
        for (int i = 0; i < linePoints.Count; i++)
        {
            line.SetPosition(i, linePoints[i].position);
        }
    }

    private void OnTriggerEnter2D(Collider2D targetCollider)
    {
        if (targetCollider.CompareTag(TagManager.coin))
        {
            totalCoins++;
            Destroy(targetCollider.gameObject);
            if (totalCoins == Spawner.Instance.CoinsAmount)
            {
                Kill();
                UIController.Instance.Win();
            }
        }

        if (targetCollider.CompareTag(TagManager.spike))
        {
            Kill();
            UIController.Instance.GameOver();
        }
    }

    public void Kill()
    {
        path.Clear();
        foreach (var item in FindObjectsOfType<Waypoint>())
            Destroy(item.gameObject);
        Destroy(gameObject);
    }
}
