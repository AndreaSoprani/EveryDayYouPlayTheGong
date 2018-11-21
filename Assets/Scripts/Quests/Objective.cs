namespace Quests
{
    /// <summary>
    /// The Objective class contains the information for a single objective inside a quest.
    /// </summary>
    [System.Serializable]
    public abstract class Objective
    {
        public string ObjectiveID; // Unique ID of the Objective.
        public string Description; // Text description of the objective.
        public bool Completed; 
    }
}
