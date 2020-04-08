using BT;

//TODO: interface for AI with InitActions() and StartAI()
namespace BaseAI
{
    public interface IBaseAI
    {
        void InitActions();
        int StartAI();
        void Start();
        void InitObjects();
        AiStates GetNextAction(States state);
    }
}