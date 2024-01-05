using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundShaker : MonoBehaviour
{
    // Deprem ne kadar güçlü?
    public float magnitude = 3f; // Gerçek depremlerdeki kullanılan büyüklük ile aynı değil
    public float slowDownFactor = 0.01f;

    private Vector3 originalPosition;
    private DateTime shakeTimer = DateTime.Now.AddSeconds(10);
    private DateTime shakeCount = DateTime.Now;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeTimer.Second > shakeCount.Second)
        {
            // Rastgele değerlerle zemini salla
            Vector2 randomPos = Random.insideUnitCircle * magnitude;

            float randomY = Random.Range(-1f, 1f) * magnitude;

            // Daha gerçekçi bir deprem elde etmek için - aksi takdirde zemin sallanacak ve titreyecektir
            float randomX = Mathf.Lerp(transform.position.x, randomPos.x, Time.time * slowDownFactor);
            float randomZ = Mathf.Lerp(transform.position.z, randomPos.y, Time.time * slowDownFactor);

            randomY = Mathf.Lerp(transform.position.y, randomY, Time.time * slowDownFactor * 0.1f);

            Vector3 moveVec = new Vector3(randomX, randomY, randomZ);

            transform.position = originalPosition + moveVec;

            shakeCount = DateTime.Now;
        }
        else
        {
            // Sallama süresi bittiğinde pozisyonu sıfırla
            transform.position = originalPosition;
        }
    }
}
