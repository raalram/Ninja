using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class InspectorQuestDescripcion : QuestDescripcion
{
    [SerializeField]private TextMeshProUGUI questRecompensa;
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        questRecompensa.text=$"-{quest.RecompensaOro} oro"+
                            $"\n-{quest.RecompensaExp} exp" +
                            $"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}";

    }

    public void AceptarQuest()
    {
        if(QuestporCompletar == null)
        {
            return;
        }

        QuestporCompletar.QuestAceptado = true;
        QuestManager.Instance.A�adirQuest(QuestporCompletar);
        gameObject.SetActive(false);
    }
}
