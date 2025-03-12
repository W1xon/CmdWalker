﻿namespace CmdWalker
{
    internal class Door
    {
        private Random _rand = new Random();
        private Room _room;
        public Door(Room room)
        {
            _room = room;
        }
        public void Create(Vector vector)
        {
            if (_room.Size.X < 3 || _room.Size.Y < 3) return;

            bool isHorizontal = vector == Vector.up || vector == Vector.down;
            int maxSize = isHorizontal ? _room.Size.X : _room.Size.Y;
            int sizeDoor = _rand.Next(1, maxSize - 2);
            int start = _rand.Next(1, maxSize - sizeDoor - 1);
            int fixedCoord = (vector == Vector.up || vector == Vector.left) ? 0 : (isHorizontal ? _room.Size.Y - 1 : _room.Size.X - 1);

            for (int i = start; i < start + sizeDoor; i++)
            {
                if (isHorizontal)
                    _room.Plane[fixedCoord][i] = Blocks.GetGlyph(Block.Floor);
                else
                    _room.Plane[i][fixedCoord] = Blocks.GetGlyph(Block.Floor);
            }
        }
    }
}
