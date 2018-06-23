using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesTester : MonoBehaviour
{
    [System.Serializable]
    public class Objective
    {
        [System.Serializable]
        public class Update
        {
            public string text = "Be the guy";
            public float startTime = 0.0f;

            internal bool hasStarted = false;
        }

        public List<Update> updates;
        public float endTime = 5.0f;

        internal bool hasEnded = false;
        internal SingleObjectiveContainer container = null;
    }

    [SerializeField]
    private ObjectivesContainer objectivesContainer;

    [SerializeField]
    private List<Objective> objectivesList;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        foreach (Objective objective in objectivesList)
        {
            if (!objective.hasEnded && timer >= objective.endTime && objective.container != null)
            {
                objectivesContainer.RemoveObjective(objective.container);
                objective.hasEnded = true;
            }

            foreach (Objective.Update update in objective.updates)
            {
                if (!update.hasStarted && timer >= update.startTime)
                {
                    if (objective.container == null)
                    {
                        objective.container = objectivesContainer.AddObjective();
                    }

                    objective.container.SetText(update.text);
                    update.hasStarted = true;
                }
            }
        }
    }
}
