using _Project.Scripts.Player;

namespace _Project.Scripts.MinedResources
{
    public interface IInteractive
    {
        float TimeToInteract { get; }
        bool CanInteract(IPlayer player);
        void Interact(IPlayer player);
    }
}