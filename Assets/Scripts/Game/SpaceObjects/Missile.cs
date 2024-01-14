using UnityEngine;

public class Missile : SpaceObject
{
    private Vector3 _originPos;
    private float speed = 0.2f;
    private float maxDis = 10f;
    private void Start()
    {
        _originPos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();
        if (spaceObject != null)
        {
            MakeDamage(spaceObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();
        if (spaceObject != null)
        {
            MakeDamage(spaceObject);
        }
    }

    private void MakeDamage(SpaceObject spaceObject)
    {
        spaceObject.TakeDamage(1);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + transform.up * speed;
        if (Vector3.Distance(transform.position, _originPos) > maxDis)
        {
            Destroy(gameObject);
        }
    }
}