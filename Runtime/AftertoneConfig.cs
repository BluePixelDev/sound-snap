using UnityEngine;

namespace BP.Aftertone
{
    /// <summary>
    /// Configuration asset for <see cref="Aftertone"/>.
    /// </summary>
    public class AftertoneConfig : ScriptableObject
    {
        [SerializeField, Min(0)] private int initialPoolSize = 10;
        [SerializeField, Min(1)] private int poolExpansionSize = 10;
        [SerializeField, Min(2)] private int maxPoolSize = 100;

        [Tooltip("Enables debug GUI")]
        [SerializeField] private bool useDebug = false;

        private void OnValidate()
        {
            initialPoolSize = Mathf.Max(0, initialPoolSize);
            poolExpansionSize = Mathf.Max(1, poolExpansionSize);
            maxPoolSize = Mathf.Max(2, maxPoolSize);
        }

        public int InitialPoolSize => Mathf.Max(0, initialPoolSize);
        public int PoolExpansionSize => Mathf.Max(1, poolExpansionSize);
        public int MaxPoolSize => Mathf.Max(2, maxPoolSize);
        public bool Debug => useDebug;
    }
}
