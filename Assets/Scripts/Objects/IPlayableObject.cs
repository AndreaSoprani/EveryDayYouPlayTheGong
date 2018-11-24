/// <summary>
/// Interface used for objects that can be played.
/// </summary>
public interface IPlayableObject
{


	/// <summary>
	/// Used to check if the object can be played.
	/// </summary>
	/// <returns>true if the object can be played, false otherwise</returns>
	bool IsPlayable();

	/// <summary>
	/// Used to play the object.
	/// </summary>
	void Play();
}

	
