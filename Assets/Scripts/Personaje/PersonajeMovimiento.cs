
using UnityEngine;

public class PersonajeMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;

    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;//si la magnitud del vector es mayor de cero no estamos moviendo
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    private PersonajeVida personajeVida;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    private void Awake()
    {
        personajeVida = GetComponent<PersonajeVida>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (personajeVida.Derrotado)
        {
            _direccionMovimiento = Vector2.zero;
            return;
        }

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        //movimiento en X

        if(_input.x>0.1f)// nos estamos moviendo a la derecha
        {
            _direccionMovimiento.x = 1f;
        }
        else if(_input.x<0f)// nos estamos moviendo a la izquierda
        {
            _direccionMovimiento.x = -1f;
        }
        else //no nos estamos moviendo
        {
            _direccionMovimiento.x = 0f;
        }

        //movimiento en Y
        if (_input.y > 0.1f)// nos estamos moviendo hacia arriba
        {
            _direccionMovimiento.y = 1f;
        }
        else if (_input.y < 0f)// nos estamos moviendo hacia abajo
        {
            _direccionMovimiento.y = -1f;
        }
        else //no nos estamos moviendo
        {
            _direccionMovimiento.y = 0f;
        }

    }
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad*Time.fixedDeltaTime);
    }
}
