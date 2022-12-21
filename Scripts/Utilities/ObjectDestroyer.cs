using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void Destroy() {
        DestroyImmediate(this.gameObject);
    }
}
