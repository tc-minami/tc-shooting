using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private BaseBullet bulletPrefab;

    private float bulletSpawnPeriod = 0.1f;
    private Timer bulletSpawnTimer;
    private List<BaseBullet> bulletList;

    private GenericPool bulletPool;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Initialize()
    {
        bulletPool = GenericPool.CreateNewPoolWithPrefab(bulletPrefab, this.transform, "BulletPool", "Bullet");
        bulletSpawnTimer = Timer.InstantiateTimer(this.transform, "BulletTimer");
        bulletSpawnTimer.SetOnCompleteCallback(ShootBullet);
        bulletSpawnTimer.RunTimer(bulletSpawnPeriod, true);
    }

    private void ShootBullet()
    {
        BaseBullet bullet = bulletPool.GetOrCreate<BaseBullet>();
        bullet.InitializeBullet();
        bullet.SetBulletPos(this.transform.position);
    }
}
