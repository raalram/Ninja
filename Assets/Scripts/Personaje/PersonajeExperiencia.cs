using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;
    
    private void Start()
    {
        stats.Nivel = 1;
        expRequeridaSiguienteNivel=expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            A�adirExperiencia(10f);
        }
    }

    public void A�adirExperiencia(float expObtenida)
    {
        if(expObtenida >0f) 
        {
            float expRestanteNuevoNivel=expRequeridaSiguienteNivel-expActualTemp;
            if(expObtenida>=expRestanteNuevoNivel)
            {
                expObtenida-=expRestanteNuevoNivel; 
                expActual += expObtenida;
                ActualizarNivel();
                A�adirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if(expActualTemp==expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }

        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if(stats.Nivel< nivelMax )
        {
            stats.Nivel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel += valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;

        }
    }
    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }
    private void RespuestaEnemigoDerrotado(float exp)
    {
        A�adirExperiencia(exp);
    }
    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
