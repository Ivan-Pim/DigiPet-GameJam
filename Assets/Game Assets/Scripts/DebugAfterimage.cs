using System.Collections.Generic;
using UnityEngine;

public class DebugAfterimage : MonoBehaviour
{
    public int length = 15;
    public Transform followObject;
    LineRenderer renderer;
    LinkedList<Vector2> rememberedPositions = new LinkedList<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
        renderer.positionCount = length;
    }

    // Update is called once per frame
    void Update()
    {
        rememberedPositions.AddLast(followObject.position);
        if (rememberedPositions.Count > length) {
            rememberedPositions.RemoveFirst();
        }

        var iPos = rememberedPositions.First;
        for (int i = 0; i < length; i++) {
            renderer.SetPosition(i, iPos.Value);
            iPos = iPos.Next;
        }
    }
}
