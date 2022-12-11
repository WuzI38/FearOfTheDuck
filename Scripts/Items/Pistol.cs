using UnityEngine;

public class Pistol : Gun
{
    public Pistol(MonoBehaviour player, GunScriptable gunParams) : base(player, gunParams){
        // Set all the parameters (using scriptable object)
        type = Constants.itemType.Pistol;
    }
}
