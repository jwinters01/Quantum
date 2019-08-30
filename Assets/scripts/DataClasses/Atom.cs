using UnityEngine;

class Atom
{
    private Tile position;
    private Color color;
    private GameObject atomObject;

    public Atom(Tile position, Color color, GameObject atomObject)
    {
        this.position = position;
        this.color = color;
        this.atomObject = atomObject;
    }

    public GameObject GetGameObject()
    {
        return this.atomObject;
    }
}
