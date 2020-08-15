using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAOD7
{

    public class DoubleBeamState
    {
        public Map2D Map { get; }
        public Dictionary<Point,Waypoint> openCells { get; }
        public Dictionary<Point, Waypoint> closedCells { get; }
        public Dictionary<Point,Waypoint> closedStart { get; }
        public Dictionary<Point,Waypoint> closedEnd { get; }

        public bool foundPath { get; set; }
        public Waypoint[] ways { get; set; }
        public Point[] wayDirection { get; set; }

        public DoubleBeamState(Map2D map)
        {
            Map = map;
            openCells = new Dictionary<Point, Waypoint>();
            closedCells = new Dictionary<Point, Waypoint>();
            closedStart = new Dictionary<Point, Waypoint>();
            closedEnd = new Dictionary<Point, Waypoint>();
            wayDirection = new Point[4];
            ways = new Waypoint[5];
            // from start through x
            ways[0] = new Waypoint(map.start,null);
            // from start through y
            ways[1] = ways[0];
            // from end through x
            ways[2] = new Waypoint(map.end, null);
            // from end through y
            ways[3] = ways[2];
            ways[4] = null;

            addOpen(ways[0]);
            addOpen(ways[1]);
            addOpen(ways[2]);
            addOpen(ways[3]);

            setDirections();
        }

        private void setDirections()
        {
            //TODO: направление задается в связи с положением
            // точек и добавляет след точку в, короче, куча условий будет.
            int a, b;
            if (Map.start.X - Map.end.X >= 0)
                a = 1;
            else a = 0;
            if (Map.start.Y - Map.end.Y >= 0)
                b = 1;
            else b = 0;

            if (a == 0 && b == 0)
            {
                wayDirection[0] = new Point(0,1);
                wayDirection[1] = new Point(1,0);
                wayDirection[2] = new Point(-1,0);
                wayDirection[3] = new Point(0,-1);
            }
            else if(a == 0 & b == 1)
            {
                wayDirection[0] = new Point(0,-1);
                wayDirection[1] = new Point(1,0);
                wayDirection[2] = new Point(-1,0);
                wayDirection[3] = new Point(0,1);
            }
            else if(a == 1 && b == 0)
            {
                wayDirection[0] = new Point(0,1);
                wayDirection[1] = new Point(-1,0);
                wayDirection[2] = new Point(1,0);
                wayDirection[3] = new Point(0,-1);
            }
            else
            {
                wayDirection[0] = new Point(0,-1);
                wayDirection[1] = new Point(-1,0);
                wayDirection[2] = new Point(1,0);
                wayDirection[3] = new Point(0,1);
            }
        }

        public Waypoint getMinOpen()
        {
            if(openCells.Count > 0)
            {
                List<Waypoint> w = new List<Waypoint>(openCells.Values);
                Waypoint minCost = w[0];
                for(int i = 0; i< w.Count; i++)
                {
                    if (minCost.getTotalCost() > w[i].getTotalCost())
                        minCost = w[i];
                }
                return minCost;
            }
            return null;
        }
        public void addOpen(Waypoint wp)
        {
            Waypoint buf;
            if (!openCells.TryGetValue(wp.Loc, out buf) && buf != wp)
            {
                if (!openCells.ContainsKey(wp.Loc) ||
                    openCells[wp.Loc].PrevCost > wp.PrevCost)
                {
                    openCells.Add(wp.Loc, wp);
                }
            }
        }

        public bool isClosed(Point loc)
        {
            bool res = false;
            if (closedCells.ContainsKey(loc))
                res = true;
            return res;
        }

        public void closeStart(Point loc)
        {
            if (openCells.ContainsKey(loc))
            {
                closedStart.Add(loc, openCells[loc]);
                closedCells.Add(loc, openCells[loc]);
                openCells.Remove(loc);
            }
        }

        public void closeEnd(Point loc)
        {
            if (openCells.ContainsKey(loc))
            {
                closedEnd.Add(loc, openCells[loc]);
                closedCells.Add(loc, openCells[loc]);
                openCells.Remove(loc);
            }
        }

        public void setEndWay(Waypoint wp, Point loc)
        {
            ways[4] = Waypoint.GetListIn(ways[0], loc, wp);
            if (ways[4] == null)
                ways[4] = Waypoint.GetListIn(ways[1], loc, wp);
            if (ways[4] == null)
                ways[4] = Waypoint.GetListIn(ways[2], loc, wp);
            if (ways[4] == null)
                ways[4] = Waypoint.GetListIn(ways[3], loc, wp);
            foundPath = true;
        }


        public Waypoint[] /*Dictionary<Point,Waypoint>*/ computePath()
        {
            while(!foundPath && openCells.Count > 0)
            {
                takeNextStep();
            }
            
            for(int i=0; i < 4; i++)
                ways[i].Reverse();
            
            return ways;
            //return closedCells;
        }

        private void takeNextStep()
        {
            Point loc, dir;

            for(int i = 0; i < 4; i++)
            {
                loc = ways[i].Loc;
                dir = wayDirection[i];
                Point next = new Point((loc.X + dir.X), (loc.Y + dir.Y));

                if (Map.contains(next.X,next.Y))
                {
                    if(Map.cells[next.X,next.Y] == 0) {

                        if (isClosed(next))
                        {
                            setEndWay(ways[i], next);
                            break;
                        }
                        else
                        {
                            ways[i] = new Waypoint(next, ways[i]);
                            addOpen(ways[i]);
                        }
                    }

                }
                                if (i < 3) closeStart(loc);
                                else closeEnd(loc);
            }
        }
    }
}
