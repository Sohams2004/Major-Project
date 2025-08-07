using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkScriptableObject", menuName = "ScriptableObjects/RandomWalkScriptableObject")]
public class RandomWalkScriptableObject : ScriptableObject
{
    public int iterations = 10, walkLength = 5;
    public bool startIterations = true;
}
