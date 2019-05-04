using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PerspectiveSwitcher))]
public class MatrixesInverter : GameCommandHandler
{
    public override void PerformInteraction()
    {
        PerspectiveSwitcher ps = GetComponent<PerspectiveSwitcher>();
        var tmp = ps.start;
        ps.start = ps.end;
        ps.end = tmp;
        ps.toOrtho = !ps.toOrtho;
    }
}
