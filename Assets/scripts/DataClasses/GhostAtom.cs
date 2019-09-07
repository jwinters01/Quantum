using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GhostAtom : Atom
{
    public GhostAtom(Tile position, Color color, GameObject atomObject):
        base(position, color, atomObject){}
}
