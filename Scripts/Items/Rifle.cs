using UnityEngine;

public class Rifle : Gun
{
    public Rifle(MonoBehaviour player, GunScriptable gunParams) : base(player, gunParams){
        // Set all the parameters (using scriptable object) - inherited from gun
        type = Constants.itemType.Rifle;
    }
}
