using UnityEngine;

namespace Store
{
    [CreateAssetMenu(menuName = "Scriptable Objects/CheatMappingHolder")]
    public class CheatMappingHolder: ScriptableObject
    {
        public CheatMapping[] Mapping;
    }
}