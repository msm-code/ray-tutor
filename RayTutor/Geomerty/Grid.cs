using System.Collections.Generic;
using System.Linq;
using System;

namespace RayTutor.Geomerty
{
    class Grid : IGeometricObject
    {
        Aabb bounds;
        IGeometricObject[, ,] grid;
        Vector3 cells;
        Vector3 gridSize;
        Vector3 cellSize;

        public Grid(List<IGeometricObject> objects)
        {
            ComputeAabb(objects);
            InitGrid(gridSize, objects.Count);
            AddObjects(objects);
        }

        public double Intersection(Ray ray, ref IntersectionInfo info)
        {
            Vector3 intersectionPoint;
            double distance = 0;

            if (bounds.Contains(ray.Origin))
            { intersectionPoint = ray.Origin; }
            else if (Ray.IsValid(distance = bounds.Intersection(ray)))
            { intersectionPoint = ray.Follow(distance); }
            else
            { return Ray.Huge; }

            return distance + Traverse(intersectionPoint, ray.Direction, ref info);
        }

        double Traverse(Vector3 origin, Vector3 dir, ref IntersectionInfo info)
        {
            Ray ray = new Ray(origin, dir);

            Vector3 cell = GetCellForPoint(origin);

            Vector3 min = GetCellCorner(cell) - origin;
            Vector3 max = min + cellSize;

            Vector3 tNext = new Vector3(
                (dir.X < 0) ? min.X / dir.X : max.X / dir.X,
                (dir.Y < 0) ? min.Y / dir.Y : max.Y / dir.Y,
                (dir.Z < 0) ? min.Z / dir.Z : max.Z / dir.Z);

            Vector3 step = new Vector3(
                Math.Sign(dir.X),
                Math.Sign(dir.Y),
                Math.Sign(dir.Z));

            Vector3 delta = new Vector3(
                cellSize.X / Math.Abs(dir.X),
                cellSize.Y / Math.Abs(dir.Y),
                cellSize.Z / Math.Abs(dir.Z));

            while (cell.X >= 0 && cell.X < cells.X &&
                cell.Y >= 0 && cell.Y < cells.Y &&
                cell.Z >= 0 && cell.Z < cells.Z)
            {
                double intersection = CheckCell(ray, cell, ref info);
                if (intersection > Ray.Epsilon &&
                    intersection < tNext.X &&
                    intersection < tNext.Y &&
                    intersection < tNext.Z)
                { return intersection; }

                if (tNext.X < tNext.Y)
                    if (tNext.X < tNext.Z)
                    { cell.X += step.X; tNext.X += delta.X; }
                    else
                    { cell.Z += step.Z; tNext.Z += delta.Z; }
                else
                    if (tNext.Y < tNext.Z)
                    { cell.Y += step.Y; tNext.Y += delta.Y; }
                    else
                    { cell.Z += step.Z; tNext.Z += delta.Z; }
            }

            return Ray.Huge;
        }

        double CheckCell(Ray ray, Vector3 cell, ref IntersectionInfo info)
        {
            IGeometricObject obj = grid[(int)cell.X, (int)cell.Y, (int)cell.Z];

            if (obj != null)
            { return obj.Intersection(ray, ref info);  }
            
            return Ray.Huge;
        }

        public Aabb BoundingBox
        { get { return bounds; } }

        #region Init
        void ComputeAabb(List<IGeometricObject> objects)
        {
            const double Margin = 0.1;
            Vector3 margin = new Vector3(Margin, Margin, Margin);

            Aabb exactBounds = objects
                .Select((x) => x.BoundingBox)
                .Aggregate((x, y) => Aabb.Union(x, y));

            this.bounds = new Aabb(exactBounds.Min - margin, exactBounds.Max + margin);
            this.gridSize = this.bounds.Max - this.bounds.Min;
        }

        void InitGrid(Vector3 gridSize, int objCount)
        {
            const double Multipler = 2;
            double sizeFactor = Math.Pow(gridSize.X * gridSize.Y * gridSize.Z / objCount, 1 / 3f);

            if (sizeFactor == 0) // grid ma zerową szerokość w jakiejś osi - nie obsługujemy
            { throw new System.InvalidOperationException(); }

            this.cells = new Vector3((int)Math.Floor(Multipler * gridSize.X / sizeFactor) + 1,
                (int)Math.Floor(Multipler * gridSize.Y / sizeFactor) + 1,
                (int)Math.Floor(Multipler * gridSize.Z / sizeFactor) + 1);

            this.cellSize = new Vector3(gridSize.X / cells.X,
                gridSize.Y / cells.Y,
                gridSize.Z / cells.Z);

            this.grid = new IGeometricObject[(int)cells.X, (int)cells.Y, (int)cells.Z];
        }

        void AddObjects(List<IGeometricObject> objects)
        {
            foreach (var obj in objects)
            {
                Vector3 start = GetCellForPoint(obj.BoundingBox.Min);
                Vector3 end = GetCellForPoint(obj.BoundingBox.Max);

                for (int x = (int)start.X; x <= end.X; x++)
                    for (int y = (int)start.Y; y <= end.Y; y++)
                        for (int z = (int)start.Z; z <= end.Z; z++)
                        {
                            if (grid[x, y, z] == null) { grid[x, y, z] = new Composite(); }
                            (grid[x, y, z] as Composite).Add(obj);
                        }
            }

            for (int x = 0; x < cells.X; x++)
                for (int y = 0; y < cells.Y; y++)
                    for (int z = 0; z < cells.Z; z++)
                    {
                        if (grid[x, y, z] != null && (grid[x, y, z] as Composite).Objects.Count == 1)
                            grid[x, y, z] = (grid[x, y, z] as Composite).Objects[0];
                    }
        }
        #endregion

        #region Cells
        Vector3 GetCellForPoint(Vector3 point)
        {
            Vector3 relative = point - bounds.Min;

            return new Vector3(
                (int)(relative.X / gridSize.X * cells.X).ClampTo(0, cells.X - 1),
                (int)(relative.Y / gridSize.Y * cells.Y).ClampTo(0, cells.Y - 1),
                (int)(relative.Z / gridSize.Z * cells.Z).ClampTo(0, cells.Z - 1));
        }

        Vector3 GetCellCorner(Vector3 cell)
        {
            return bounds.Min + new Vector3(cell.X * cellSize.X, cell.Y * cellSize.Y, cell.Z * cellSize.Z);
        }
        #endregion
    }
}
