using System.Collections;
using System.Linq;
using Player;
using UnityEngine;

public class BossShockwave : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int pointsCount;
    public float maxRadius;
    public float speed;
    public float startWidth;

    [SerializeField]
    private float shockWaveDmg = 1f;
    

    private void Awake()
    {
        //needs 1 additional point because start and end are the same
        lineRenderer.positionCount = pointsCount + 1;
    }

    public void StartShockwave()
    {
        StartCoroutine(Blast());
    }

    private IEnumerator Blast()
    {
        float currentRadius = 0f;

        while (currentRadius < maxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            DrawCircle(currentRadius);
            Damage(currentRadius);
            yield return null;
        }
    }

    private void DrawCircle(float currentRadius)
    {
        //we need all points with an equal angle to get a nice circle
        float angleBetweenPoints = 360f / pointsCount;

        for (int i = 0; i <= pointsCount; i++)
        {
            float angle = i * angleBetweenPoints * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;
            
            lineRenderer.SetPosition(i, position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / maxRadius);
    }

    private void Damage(float currentRadius)
    {
        Collider [] outerCircle = Physics.OverlapSphere(transform.position, currentRadius, LayerMask.GetMask("Player"));
        Collider [] innerCircle = Physics.OverlapSphere(transform.position, currentRadius - 1f, LayerMask.GetMask("Player"));

        //check if someone hit
        if (outerCircle != null)
        {
            //if they are in inner circle dont get hit and be deletet of list
            for (int i = 0; i < outerCircle.Length; i++)
            {
                if (innerCircle.Contains(outerCircle[i]))
                {
                    outerCircle[i] = null;
                }
            }

            //for everyone left check if player jumped
            foreach (var player in outerCircle)
            {
                if (player != null)
                {
                    if (player.transform.position.y < 1.3f)
                    {
                        if (player.TryGetComponent(out PlayerHealth playerHealth))
                        {
                            playerHealth.DamagePlayer(shockWaveDmg);
                        }
                    }
                }
            }
        }
    }
}
