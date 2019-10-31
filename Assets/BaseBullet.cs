using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : PoolableObject
{
    private const float DefBulletSpeed = 10.0f;
    public enum MoveDir
    {
        Left,
        Right,
        Up,
        Down
    };

    private Vector3 localPos;
    private float bulletSpeed = DefBulletSpeed;
    private MoveDir bulletMoveDir = MoveDir.Up;

    public void InitializeBullet(Vector3 _pos, float _bulletSpeed, MoveDir _moveDir = MoveDir.Up)
    {
        transform.position = _pos;
        bulletSpeed = _bulletSpeed;
        bulletMoveDir = _moveDir;
    }

    public void InitializeBullet(float _bulletSpeed, MoveDir _moveDir = MoveDir.Up)
    {
        InitializeBullet(Vector3.zero, _bulletSpeed, _moveDir);
    }

    public void InitializeBullet(MoveDir _moveDir = MoveDir.Up)
    {
        InitializeBullet(Vector3.zero, DefBulletSpeed, _moveDir);
    }

    public void SetBulletPos(Vector3 _pos)
    {
        transform.position = _pos;
    }

    /// <summary>
    /// Called when this object moves outside of all cameras.
    /// </summary>
    public void OnBecameInvisible()
    {
        Return2Pool();
    }

    // Update is called once per frame
    protected void Update()
    {
        localPos = transform.localPosition;
        switch (bulletMoveDir)
        {
            case MoveDir.Up:
                localPos.y += bulletSpeed * Time.deltaTime;
                break;

            case MoveDir.Down:
                localPos.y -= bulletSpeed * Time.deltaTime;
                break;

            case MoveDir.Right:
                localPos.y += bulletSpeed * Time.deltaTime;
                break;

            case MoveDir.Left:
                localPos.y -= bulletSpeed * Time.deltaTime;
                break;
        }
        transform.localPosition = localPos;
    }
}
