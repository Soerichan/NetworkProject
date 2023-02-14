using UnityEngine;
using UnityEngine.Events;

namespace Jake
{

public class MasterGroundChecker : MonoBehaviour
{
  public UnityEvent<Collider> OnGroundTrigger;

  private void OnTriggerEnter(Collider other)
  {                   
      if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
      {
        OnGroundTrigger?.Invoke(other);               
      }
  }
}
}
