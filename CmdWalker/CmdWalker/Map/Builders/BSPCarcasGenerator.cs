﻿namespace CmdWalker;

internal class BSPCarcasGenerator : CarcassGenerator
{
    private List<Leaf> leafs = new List<Leaf>();
    private Leaf root;
    private int _maxLeafSize;

    public BSPCarcasGenerator(MapTemplate template) : base(template){}
    public override char[][] Generate()
    {
        _maxLeafSize = _template.MaxRoomSize;
        GenerateRoom();
        FillCarcass();
        return _field.DeepCopy();
    }
    private void GenerateRoom()
    {
        root = new Leaf(Vector.zero,  new Vector(_width, _height));
        leafs.Add(root); 
        bool didSplit = true;
        while (didSplit)
        {
            didSplit = false;
            for (int i = 0; i < leafs.Count; i++)
            {
                Leaf l = leafs[i];
                if (l.leftChild == null && l.rightChild == null)
                {
                    if (l.Width > _maxLeafSize * 2 || l.Height > _maxLeafSize || _rand.NextDouble() > 0.25)
                    {
                        if (l.Split())
                        {
                            leafs.Add(l.leftChild);
                            leafs.Add(l.rightChild);
                            didSplit = true;
                        }
                    }
                }
            }
        }
        root.CreateRooms();
    }

    private void FillCarcass()
    {
        foreach (var l in leafs)
        {
            if(l.room != null)
                AddShapeContour(l.room, Blocks.GetGlyph(Block.Wall));
                
        }
        foreach (var l in leafs)
        {
            if(l.halls != null)
            {
                foreach (var hall in l.halls)
                {
                    AddRectangle(hall, Blocks.GetGlyph(Block.Floor));
                }
            }
        }
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _field[y].Length; x++)
            {
                if (y == 0 || x == 0 || y == _field.Length - 1 || x == _field[y].Length - 1)
                    _field[y][x] = Blocks.GetGlyph(Block.Wall);
                else if(_field[y][x] == ' ') _field[y][x] = Blocks.GetGlyph(Block.Floor);
            }
        }
    }

    public void AddRectangle(Rectangle rectangle, char glyph)
    {
        /*AddShapeContur(shape, glyph);
        var vertices = shape.GetVertices();
        int x = 0, y = 0;
        foreach (var vertex in vertices)
        {
            x += vertex.X;
            y += vertex.Y;
        }
        var center = new Vector(x / vertices.Length, y / vertices.Length);
        
        FillNeighbour(center, glyph);*/

        for (int y = 0; y < rectangle.Height; y++)
        {
            for (int x = 0; x < rectangle.Width; x++)
            {
                int fieldY = rectangle.Y + y;
                int fieldX = rectangle.X + x;
                if (fieldY >= 0 && fieldY < _height && fieldX >= 0 && fieldX < _width)
                    _field[fieldY][fieldX] = Blocks.GetGlyph(Block.Floor);
            }
        }
    }

    private void FillNeighbour(Vector point, char glyph)
    {
        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                var newX = point.X + x;
                var newY = point.Y + y;
                if ((x == 0 && y == 0) || 
                    newX < 0 || newY < 0 || newX >= _field[0].Length || newY >= _field.Length) continue;
                if(_field[newY][newX] == glyph) continue;
                _field[newY][newX] = glyph;
                FillNeighbour(new Vector(newX,newY), glyph);
            }
        }
    }

}