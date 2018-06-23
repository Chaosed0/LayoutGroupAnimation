using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ObjectivesContainer : MonoBehaviour
{
    [SerializeField]
    private RectTransform header;

    [SerializeField]
    private SingleObjectiveContainer singleObjectiveContainerPrefab;

    private RectTransform rectTransform;
    private float smoothDampVel;

    private void Awake()
    {
        rectTransform = this.transform as RectTransform;
    }

    public SingleObjectiveContainer AddObjective()
    {
        SingleObjectiveContainer objectiveContainer = Instantiate<SingleObjectiveContainer>(singleObjectiveContainerPrefab, this.transform, false);
        return objectiveContainer;
    }

    public void RemoveObjective(SingleObjectiveContainer objectiveContainer)
    {
        objectiveContainer.Hide(() =>
        {
            Destroy(objectiveContainer.gameObject);
        });
    }
}
