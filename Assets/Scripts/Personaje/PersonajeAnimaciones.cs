using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;
    [SerializeField] private string layerAtacar;

    private Animator _animator;
    private PersonajeMovimiento _personajeMovimiento;
    private PersonajeAtaque _personajeAtaque;

    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int derrotado = Animator.StringToHash("Derrotado");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _personajeMovimiento=GetComponent<PersonajeMovimiento>();
        _personajeAtaque=GetComponent<PersonajeAtaque>();
    }

    void Update()
    {
        ActualizarLayers();

        if (_personajeMovimiento.EnMovimiento == false) //si el personaje no se está moviendo
        {
            return; //regresamos, el código se queda aquí, no sigue
        }

        _animator.SetFloat(direccionX, _personajeMovimiento.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _personajeMovimiento.DireccionMovimiento.y);

    }

    private void ActivarLayer(string nombreLayer)
    {
        for(int i=0;i<_animator.layerCount;i++)
        {
            _animator.SetLayerWeight(i,0);// pone un peso a los layer (0 desactivado o 1 activado)
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    private void ActualizarLayers()
    {
        if (_personajeAtaque.Atacando)
        {
            ActivarLayer(layerAtacar);
        }
        else if(_personajeMovimiento.EnMovimiento) //si el personaje se está moviendo
        {
            ActivarLayer(layerCaminar);//activa el layer caminar
        }
        else //si no
        {
            ActivarLayer(layerIdle);// activa el layer del Idle
        }
    }

    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado,false);
    }

    private void PersonajeDerrotadoRespuesta()
    {
        if(_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle))==1)
        {
            _animator.SetBool(derrotado, true);
        }
    }
    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }
    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;

    }
}
