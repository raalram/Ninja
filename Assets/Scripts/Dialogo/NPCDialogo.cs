using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteraccionExtraNPC
{
    Quests,
    Tienda,
    Crafting
}
[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("info")]
    public string Nombre; // nombre del NPC
    public Sprite Icono; // icono del NPC
    public bool ContieneInteraccionExtra; // variable para ver si tiene interacci�n extra
    public InteraccionExtraNPC InteraccionExtra;//si tiene interacci�n extra hay que ver qu� tipo de interacci�n es

    [Header("Saludo")]
    [TextArea] public string Saludo;

    [Header("Chat")]
    public DialogoTexto[] Conversacion;


    [Header("Despedida")]
    [TextArea] public string Despedida;

    [Serializable]
    public class DialogoTexto
    {
        [TextArea] public string Oracion;
    }
}
