

public interface IInteractable
{
    public void Interact(Player player)
    {
    }

    public virtual string GetInteractText(Player actor)
    {
        return "Interact";
    }
}
