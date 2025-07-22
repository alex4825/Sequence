using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta
{
    [CreateAssetMenu(fileName = "CostsConfig", menuName = "Configs/CostsConfig")]
    public class CostsConfig : ScriptableObject
    {
        [field: SerializeField] public int WinCost { get; private set; }
        [field: SerializeField] public int DefeatCost { get; private set; }
        [field: SerializeField] public int GameResetCost { get; private set; }
    }
}
