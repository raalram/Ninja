

using UnityEngine;

[CreateAssetMenu(menuName = "Items/Pocion Mana")]

public class ItemPocionMana : InventarioItem
{
    [Header("Mana info")]
    public float MPrestauracion;//cuanto podremos restaurar con esta poci�n de man�

    public override bool UsarItem()
    {
        if(Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPrestauracion);
            return true;
        }

        return false;
    }
}
