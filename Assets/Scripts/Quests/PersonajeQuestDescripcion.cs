using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        if(QuestporCompletar.QuestCompletadoCheck)
        {
            return;
        }
        tareaObjetivo.text = $"¨{QuestporCompletar.CantidadActual}/{QuestporCompletar.CantidadObjetivo}";

    }


    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        recompensaOro.text=quest.RecompensaOro.ToString();
        recompensaExp.text=quest.RecompensaExp.ToString();
        tareaObjetivo.text = $"¨{quest.CantidadActual}/{quest.CantidadObjetivo}";

        recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono;
        recompensaItemCantidad.text=quest.RecompensaItem.Cantidad.ToString();
            
    }

    private void QuestCompletadoRespuesta(Quest questCompletado)
    {
        if(questCompletado.ID==QuestporCompletar.ID)
        {
            tareaObjetivo.text = $"¨{QuestporCompletar.CantidadActual}/{QuestporCompletar.CantidadObjetivo}";
            gameObject.SetActive(false);

        }
    }

    private void OnEnable()
    {
        if(QuestporCompletar.QuestCompletadoCheck)
        {
            gameObject.SetActive(false);
        }
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }
    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;

    }
}
