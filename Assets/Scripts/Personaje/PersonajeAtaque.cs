using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float, EnemigoVida> EventoEnemigoDa�ado;

    [Header("Stats")]
    [SerializeField]private PersonajeStats stats;

    [Header("Pooler")]
    [SerializeField]private ObjectPooler pooler;

    [Header("Ataque")]
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private Transform[] posicionesDisparo;
    public Arma ArmaEquipada { get; private set; }
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    public bool Atacando { get; set; }


    private PersonajeMana _personajeMana;
    private int indexDireccionDisparo;
    private float tiempoParaSiguienteAtaque;

    private void Awake()
    {
        _personajeMana=GetComponent<PersonajeMana>();
    }
    private void Update()
    {
        ObtenerDireccionDisparo();

        if(Time.time>tiempoParaSiguienteAtaque)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(ArmaEquipada==null|| EnemigoObjetivo==null)
                {
                    return;
                }

                UsarArma();
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
                StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }

    private void UsarArma()
    {
        if(ArmaEquipada.Tipo==TipoArma.Magia)
        {
            if(_personajeMana.ManaActual< ArmaEquipada.ManaRequerida)
            {
                return;
            }

            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;

            Proyectil proyectil= nuevoProyectil.GetComponent<Proyectil>();
            proyectil.InicializarProyectil(this);

            nuevoProyectil.SetActive(true);
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida);
        }
       else
        {

            float da�o = ObtenerDa�o();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDa�o(da�o);
            EventoEnemigoDa�ado?.Invoke(da�o, enemigoVida);
        }
    }
    public float ObtenerDa�o()
    {
        float cantidad = stats.Da�o;
        if (Random.value < stats.PorcentajeCritico / 100)
        {
            cantidad *= 2;
        }

        return cantidad;
    }

    private IEnumerator IEEstablecerCondicionAtaque()
    {
        Atacando = true;
        yield return new WaitForSeconds(0.3f);
        Atacando = false;
    }

    public void EquiparArma(ItemArma armaPorEquipar)
    {
        ArmaEquipada=armaPorEquipar.Arma;
        if(ArmaEquipada.Tipo==TipoArma.Magia)
        {
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }

        stats.A�adirBonusPorArma(ArmaEquipada);
    }

    public void RemoverArma()
    {
        if (ArmaEquipada == null)
        {
            return;
        }
        if(ArmaEquipada.Tipo ==TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }

        stats.RemoverBonusPorArma(ArmaEquipada);
        ArmaEquipada = null;
    }

    private void ObtenerDireccionDisparo()
    {
        Vector2  input= new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(input.x>0.1f)// nos estamos moviendo a la derecha
        {
            indexDireccionDisparo = 1;//index de disparo dentro del array
        }
        else if (input.x<0f) //si vamos a la izquierda
        {
            indexDireccionDisparo = 3;
        }
        else if(input.y>0.1f)// nos movemos hacia arriba
        {
            indexDireccionDisparo=0;
        }
        else if(input.y<0f)//nos movemos hacia abajo
        {
            indexDireccionDisparo=2;
        }
    }

    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        if(ArmaEquipada==null) {return;}
        if(ArmaEquipada.Tipo!=TipoArma.Magia) {return;}
        if(EnemigoObjetivo==enemigoSeleccionado){return;}

        EnemigoObjetivo=enemigoSeleccionado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true,TipoDeteccion.Rango);
    }

    private void EnemigoNoSeleccionado()
    {
        if(EnemigoObjetivo==null) {return;}
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false,TipoDeteccion.Rango);
        EnemigoObjetivo=null;
    }

    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        if (ArmaEquipada == null) { return; }
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo=enemigoDetectado;
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);

    }
    private void EnemigoMeleePerdido()
    {
        if (ArmaEquipada == null) { return; }
        if (EnemigoObjetivo == null) { return; }
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;


    }
    private void OnEnable()
    {
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado+= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {
        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;



    }
}
