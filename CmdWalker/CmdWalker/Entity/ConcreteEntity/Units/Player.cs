using System;

namespace CmdWalker;

public class Player : Unit
{
    public readonly Inventory Inventory;
    private readonly PlayerController _controller;
    public Player(Vector pos) : base(pos)
    {
        _health = new Health(100);
        Visual = new Glyph(
            RenderPalette.GetString(TileType.Player),
            ConsoleColor.Green
        );

        Inventory = new Inventory(this);

        _controller = new PlayerController(this);
    }
    
    public override void Move(Vector direction)
    {
        if (direction == Vector.Zero)
            return;

        if (!CanMoveDir(direction))
            return;

        ClearPreviousPosition();
        Transform.Position += direction;
    }


    public override bool CanMoveDir(Vector dir)
    {
        return Collider.CanMoveTo(dir);
    }
    public override void Update()
    {
        Render();
    }
    private void Render()
    {
        _map.SetCells(Transform.Position, Visual);
    }

    public override void Destroy()
    {
        _controller.Dispose();

        ClearPreviousPosition();
        _map.EntityManager.DeleteEntity(this);
        Inventory.DropAll();

        Debug.Info = "Игрока грохнули";
        SceneManager.SwitchTo(new DeathMenu(new Vector(90, 30)));
    }
}