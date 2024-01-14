using UnityEngine;

public class ResourceCollecter : SpaceObject
{
    private Vector3 _originPos;
    private float speed = 0.1f;
    private bool _back = false;
    private float _maxDis = 10f;
    private GameObject _ship;
    // Start is called before the first frame update
    void Start()
    {
        _originPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_back)
        {

            Vector3 directionToTarget = (_ship.transform.position - transform.position).normalized; // 计算从当前对象指向目标对象的单位化向量
            float angleToTarget = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f; // 计算与X轴正方向的夹角
            Quaternion rotationToTarget = Quaternion.Euler(0, 0, angleToTarget); // 创建一个新的四元数表示旋转
            transform.rotation = rotationToTarget; // 更新当前对象的旋转
            if (Vector3.Distance(_ship.transform.position, transform.position) < 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, _originPos) >= _maxDis)
            {
                _back = true;
            }
        }
        transform.position = transform.position + transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_back)
        {
            return;
        }
        SpaceResource mine = collision.GetComponent<SpaceResource>();
        if (mine != null)
        {
            mine.Collect();
            _back = true;
        }
    }

    public void SetShip(GameObject ship)
    {
        _ship = ship;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _back = true;
    }
}