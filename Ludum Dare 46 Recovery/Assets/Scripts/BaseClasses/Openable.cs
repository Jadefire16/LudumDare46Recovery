using UnityEngine;

public class Openable : MonoBehaviour, IOpenable
{

    protected bool isOpen;

    public virtual void OnClose() { isOpen = false; }
    public virtual void OnOpen() { isOpen = true; }

}
