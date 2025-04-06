using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private List<Light> lightSources;
    private bool isOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOn) return;
        if (other.gameObject.TryGetComponent(out Player player))
        {
            TurnOn();
        }
    }

    public void TurnOn()
    {
        isOn = true;
        foreach (var lightSource in lightSources)
        {
            lightSource.enabled = true;
        }
    }

    public void TurnOff()
    {
        foreach (var lightSource in lightSources)
        {
            lightSource.enabled = false;
        }
    }
}
