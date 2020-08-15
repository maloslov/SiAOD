using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SiAOD7.Form1;

namespace SiAOD7
{
    
    public class Waypoint
    {
        public Point Loc { get; set; }
        public Waypoint PrevWaypoint { get; set; }
        public float PrevCost { get; set; }
        public float RemainingCost { get; set; }

        public Waypoint(Point loc, Waypoint prevWay)
        {
            Loc = loc;
            PrevWaypoint = prevWay;
        }
        public float getTotalCost()
        {
            return PrevCost + RemainingCost;
        }
        public void Reverse()
        {
            Waypoint res = new Waypoint(Loc, null);

            while(PrevWaypoint != null)
            {
                Loc = PrevWaypoint.Loc;
                res = new Waypoint(Loc, res);
                PrevWaypoint = PrevWaypoint.PrevWaypoint;
            }
            Loc = res.Loc;
            PrevWaypoint = res.PrevWaypoint;
        }
    }

    public class Map2D
    {
        public int width { get; }
        public int height { get; }
        public int[,] cells { get; set; }
        public Point start { get; set; }
        public Point end { get; set; }

        public Map2D(int w, int h)
        {
            width = w;
            height = h;
            cells = new int[width,height];
           // start = new Point(0, 0);
          //  end = start;
        }

        public void changeCost(MyCell mc, int cost)
        {
            cells[mc.Loc.X, mc.Loc.Y] = cost;
        }

        public bool contains(int x, int y)
        {
            return (x >= 0 && x < width && y >= 0 && y < height);
        }

        
        public void changeStart(MyCell mc, bool flag)
        {
            if (flag)
            {
                start = new Point(0,0);
            }
            else
            {
                start = mc.Loc;

            }
            changeCost(mc, 0);
        }

        public void changeEnd(MyCell mc, bool flag)
        {
            if (flag)
            {
                end = new Point(0, 0);
            }
            else
            {
                end = mc.Loc;

            }
            changeCost(mc, 0);
        }

    }
    
    public class AstarState
    {
        public Map2D Map { get; }
        public Dictionary<Point,Waypoint> openCells { get; }
        public Dictionary<Point,Waypoint> closedCells { get; }

        public AstarState(Map2D map)
        {
            Map = map;
            openCells = new Dictionary<Point, Waypoint>();
            closedCells = new Dictionary<Point, Waypoint>();
        }

        public Waypoint getMinOpen()
        {
            if(openCells.Count > 0)
            {
                List<Waypoint> w = 
                    new List<Waypoint>(openCells.Values);
                Waypoint minCost = w[0];
                for (int i = 0; i < w.Count; i++)
                {
                    if (minCost.getTotalCost() >
                        w[i].getTotalCost())
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
            if (closedCells.ContainsKey(loc))
                return true;
            return false;
        }

        public void closeWaypoint(Point loc)
        {
            closedCells.Add(loc, openCells[loc]);
            openCells.Remove(loc);
        }
    }
    
    public static class AstarPathfind
    {
        public const float LIMIT = 1e6f;

        //Waypoint
        public static Dictionary<Point,Waypoint> computePath(Map2D map)
        {
            AstarState state = new AstarState(map);
            Point end = map.end;
            Waypoint start = new Waypoint(map.start, null);
            start.PrevCost = 0;
            start.RemainingCost = estimateTravelCost(start.Loc, end);
            state.addOpen(start);

            // Waypoint finalWaypoint = null;
            bool foundPath = false;

            while (!foundPath && state.openCells.Count > 0)
            {
                Waypoint best = state.getMinOpen();
                if (best.Loc == (end)) 
                {
                   // finalWaypoint = best;
                    foundPath = true;
                }

                takeNextStep(best, state);

                state.closeWaypoint(best.Loc);
            }
            return state.closedCells;
                //finalWaypoint;
        }

        private static void takeNextStep(Waypoint currWP, AstarState state)
        {
            Point loc = currWP.Loc;
            Map2D map = state.Map;

            for(int y = (loc.Y - 1); y <= (loc.Y + 1); y++)
            {
                for(int x = (loc.X - 1); x <= (loc.X + 1); x++)
                {
                    Point next = new Point(x, y);

                    if (!map.contains(next.X, next.Y))
                        continue;
                    if (next == loc)
                        continue;
                    if (state.isClosed(next))
                        continue;

                    Waypoint nextWP = new Waypoint(next, currWP);
                    float prevCost = currWP.PrevCost +
                        estimateTravelCost(currWP.Loc, nextWP.Loc);

                    prevCost += map.cells[next.X, next.Y];

                    if (prevCost >= LIMIT)
                        continue;

                    nextWP.PrevCost = prevCost;
                    nextWP.RemainingCost = estimateTravelCost(next, map.end);

                    state.addOpen(nextWP);
                }
            }
        }

        private static float estimateTravelCost(Point currLoc, Point destLoc)
        {
            int dx = destLoc.X - currLoc.X;
            int dy = destLoc.Y - currLoc.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

    }

}
