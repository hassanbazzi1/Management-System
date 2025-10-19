namespace Common.AI.Models;

public class Embedding
{
    public Embedding(float[] floats)
    {
        Floats = floats;
    }

    public float[] Floats { get; set; }
}