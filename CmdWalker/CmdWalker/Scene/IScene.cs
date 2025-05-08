namespace CmdWalker;

internal interface IScene
{
    bool IsActive { get; set; }
    void Enter();
    void Update();

    void Exit();
}