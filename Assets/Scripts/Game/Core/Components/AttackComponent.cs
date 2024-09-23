using Fusion;
using UnityEngine;

namespace Game.Core.Components
{
    public class AttackComponent : NetworkBehaviour
    {
        [SerializeField] private Material _fieldMaterial;

        public void SetColor(Color color)
        {
            if (_fieldMaterial != null)
            {
                _fieldMaterial.SetColor("_MainColor", color);
            }
        }

        public void UpdateRange(float range)
        {
            transform.localScale = new Vector3(range, range, range);
        }
    }
}