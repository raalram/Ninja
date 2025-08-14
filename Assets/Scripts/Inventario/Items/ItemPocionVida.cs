
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Pocion Vida")]
public class ItemPocionVida : InventarioItem
{
    [Header("Pocion info")]
    public float HPrestauracion;//cuanto podremos restaurar con esta poción de vida

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPrestauracion);
            return true;
        }
        return false;
    }
}
