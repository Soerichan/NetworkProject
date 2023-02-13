using UnityEngine;
using UnityEngine.Events;

namespace Jake
{
    public class MasterGroundChecker : MonoBehaviour
    {
        public UnityEvent<Collider> OnGroundTrigger;

        private void OnTriggerEnter(Collider other)
        {
            
            Debug.Log("온트리거엔터");
            // 나중에 이벤트로 땅에 닿았으면 색칠 해주는걸로 변경 예정 
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                OnGroundTrigger?.Invoke(other);
                Debug.Log("이프문");
            }
        }
    }
}
