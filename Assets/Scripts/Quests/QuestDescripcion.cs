using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{

    [SerializeField]private TextMeshProUGUI questNombre;
    [SerializeField]private TextMeshProUGUI questDescripcion;

    public Quest QuestporCompletar { get; set; }

    public virtual void ConfigurarQuestUI(Quest quest)
    {
        QuestporCompletar = quest;
        questNombre.text=quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }

}
