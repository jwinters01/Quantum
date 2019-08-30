using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Tile
{
    private GameObject tileObject;
    private Atom atom;
    private Dictionary<Direction, Tile> neighbors;
    public Tile(ref GameObject tileObject)
    {
        this.tileObject = tileObject;
    }

    public void setAtom(ref Atom a)
    {
        this.atom = a;
    }

    public bool isEmpty()
    {
        return atom == null;
    }
}
