using System;
using System.Threading;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class GroundShaker : MonoBehaviour
{
    // Deprem ne kadar güçlü?
    public float magnitude = 3f; // Gerçek depremlerdeki kullanılan büyüklük ile aynı değil
    public float slowDownFactor = 0.01f;

    private Vector3 originalPosition;
    private Timer timer;
    private float zamanSayaci = 0f;
    private readonly float hedefZaman = 30f;
    private readonly float beklemeZaman = 10f;
    
    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        zamanSayaci += Time.deltaTime;
        if (zamanSayaci >= beklemeZaman && zamanSayaci <= hedefZaman)
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
        }
        else
        {
            // Sallama süresi bittiğinde pozisyonu sıfırla
            transform.position = originalPosition;
        }
    }
}
