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

    public GameObject getGameObject()
    {
        return this.tileObject;
    }

    public bool isEmpty()
    {
        return atom == null;
    }

    public Atom removeAtom()
    {
        Atom result = this.atom;
        this.atom = null;
        return result;
    }

    internal void acceptAtom(Atom selectedAtom)
    {
        this.atom = selectedAtom;
        selectedAtom.GetGameObject().transform.SetParent(this.tileObject.transform, false);
    }

    internal void setNeighbors(Dictionary<Direction, Tile> neighbors)
    {
        this.neighbors = neighbors;
    }
}
