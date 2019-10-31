using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterPlane : MonoBehaviour
{

    [SerializeField]
    private BulletManager bulletManager;

    [SerializeField]
    private BulletManager.BulletType bulletType = BulletManager.BulletType.Default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyInputUpdate();
    }

    private void KeyInputUpdate()
    {
        BulletManager.BulletType currentBulletType = bulletType;

        if (Input.GetKey(KeyCode.LeftArrow)) bulletType = BulletManager.BulletType.Default;
        else if (Input.GetKey(KeyCode.RightArrow)) bulletType = BulletManager.BulletType.Rapid;
        else if (Input.GetKey(KeyCode.UpArrow)) bulletType = BulletManager.BulletType.Grendate;
        else if (Input.GetKey(KeyCode.DownArrow)) bulletType = BulletManager.BulletType.WideA;
        else if (Input.GetKey(KeyCode.Space)) bulletType = BulletManager.BulletType.WideB;

        if (currentBulletType != bulletType)
        {
            bulletManager.SetBulletType(bulletType);
        }
    }
}
