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
        public int foundOrDeadend { get; set; }
        public Waypoint[] ways { get; set; }
        public Point[] wayDirection { get; set; }

        public DoubleBeamState(Map2D map)
        {
            Map = map;
            openCells = new Dictionary<Point, Waypoint>();
            closedCells = new Dictionary<Point, Waypoint>();
            closedStart = new Dictionary<Point, Waypoint>();
            closedEnd = new Dictionary<Point, Waypoint>();
            foundOrDeadend = 0;
            wayDirection = new Point[4];
            ways = new Waypoint[4];
            // from start through x
            ways[0] = new Waypoint(map.start,null);
            // from start through y
            ways[1] = ways[0];
            // from end through x
            ways[2] = new Waypoint(map.end, null);
            // from end through y
            ways[3] = ways[2];

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

        public int isClosed(Point loc)
        {
            if (closedStart.ContainsKey(loc))
                return 1;
            else if (closedEnd.ContainsKey(loc))
                return 2;
            return 0;
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


        public Waypoint[] /*Dictionary<Point,Waypoint> */ computePath()
        {
            
           // Waypoint finalWaypoint = null;
            //bool foundPath = false;

            while(!foundPath && openCells.Count > 0)
            {
                //Waypoint best = state.getMinOpen();
                takeNextStep();


            }
            
            for(int i=0; i < 4; i++)
                ways[i].Reverse();
            

            return ways;
            //return state.closedCells;
        }

        private void takeNextStep()
        {
            Point loc, dir;

            for(int i = 0; i < 4; i++)
            {
                //Waypoint wp = state.ways[i];
                loc = ways[i].Loc;
                dir = wayDirection[i];
                Point next = new Point((loc.X + dir.X), (loc.Y + dir.Y));

                if (Map.contains(next.X,next.Y))
                {
                    int k = isClosed(next);
                    switch(k)
                    {
                        case 1:
                            {
                             //   if (i > 1) state.setEndWay();
                                break;
                            }
                        case 2:
                            {
                             //   if (i < 3) state.setEndWay();
                                break;
                            }
                        case 0:
                            {
                                ways[i] = new Waypoint(next, ways[i]);
                                addOpen(ways[i]);
                                break;
                            }
                    }

                }
                                if (i < 3) closeStart(loc);
                                else closeEnd(loc);
            }
        }
    }
}
