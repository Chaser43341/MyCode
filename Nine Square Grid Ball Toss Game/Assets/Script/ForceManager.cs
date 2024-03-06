using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ForceState
{
    Increase,
    Decrease,
    Stop,
}
public class ForceManager : MonoBehaviour
{
    public Image Force;
    public ForceState state;
    public float speed = 0.1f;
    public float fillRate;
    public GameObject bulletPrefab;
    public Camera mainCamera; // 引用主摄像机
    private Transform spawnPoint;

    private float maxForce = 20f; // 你可以根据需要调整最大力度
    private float currentForce;

    // Start 方法在第一帧更新前调用
    void Start()
    {
        Force.fillAmount = 0;
        state = ForceState.Stop;

        // 获取主摄像机的位置并设置 spawnPoint
        mainCamera = Camera.main;
        spawnPoint = new GameObject("SpawnPoint").transform;
        
    }
    // Update 方法在每一帧都被调用
    void Update()
    {
        HandleInput();
        switchForceState();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
        if(Force.fillAmount==0)
            {
                state = ForceState.Increase;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            state = ForceState.Stop;
            LaunchBullet();
            Force.fillAmount = 0;

        }
    }

    private void LaunchBullet()
    {
        // 在 spawnPoint 处生成子弹
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // 根据当前力度百分比计算力的大小
        float forceMultiplier = Force.fillAmount;
        currentForce = forceMultiplier * maxForce;

        // 在向上方45度的角度施加力
        Vector3 launchDirection = mainCamera.transform.forward;
        bulletRb.AddForce(launchDirection * currentForce, ForceMode.Impulse);
    }

    public void switchForceState()
    {
        switch (state)
        {
            case ForceState.Increase:
                StartCoroutine(OnIncrease());
                break;
            case ForceState.Decrease:
                StartCoroutine(OnDecrease());
                break;
            case ForceState.Stop:
                OnStop();
                break;
        }
    }

    private IEnumerator OnIncrease()
    {
        Force.fillAmount += speed * Time.deltaTime;
        fillRate = Force.fillAmount;
        yield return new WaitForSeconds(0.1f);
        if (Force.fillAmount >= 1.0f)
        {
            Force.fillAmount = 1.0f;
            fillRate = Force.fillAmount;
            state = ForceState.Decrease;
        }
    }
    private IEnumerator OnDecrease()
    {
        Force.fillAmount -= speed * Time.deltaTime;
        fillRate = Force.fillAmount;
        yield return new WaitForSeconds(0.1f);
        if (Force.fillAmount <= 0)
        {
            Force.fillAmount = 0;
            fillRate = Force.fillAmount;
            state = ForceState.Increase;
        }
    }

    private void OnStop()
    {
        // 停止增加力度
        StopAllCoroutines();
    }
    public void SetSpawnPoint(Vector3 position)
    {
        // 设置 spawnPoint 的位置
        spawnPoint.position = position;
    }
}