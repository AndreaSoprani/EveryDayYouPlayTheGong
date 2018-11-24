/// <summary>
/// Interface used for objects that can be interacted with.
/// </summary>
public interface IInteractiveObject
{


	/// <summary>
	/// Used to check if the object can be interacted with.
	/// </summary>
	/// <returns>true if the object can be interacted with, false otherwise</returns>
	bool IsInteractable();
	
	/// <summary>
	/// Used to interact with the object.
	/// </summary>
	void Interact(Player player);

	

}
