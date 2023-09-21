using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField] private CombinationLock comboLock = null;

    void Start()
    {
        comboLock.UnlockAction += OnUnlockAction;
    }

    private void OnUnlockAction()
    {
        this.GetComponent<Light>().enabled = true;
    }
}
