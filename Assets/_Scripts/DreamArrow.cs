using UnityEngine;

public class DreamArrow : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DreamTarget target))
        {
            target.TryToActivate();
        }
        Destroy(gameObject);
    }
}