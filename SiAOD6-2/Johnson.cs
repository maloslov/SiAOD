using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiAOD6_2
{
    class Johnson
    {

        private readonly int SOURCE_NODE;
        private readonly int numberOfNodes;
        private readonly int[,] augmentedMatrix;
        private int[] potential;
        private BellmanFord bellmanFord;
        private Dijkstra dijkstra;
        private int[,] allShortest;


        private Johnson(int numberOfNodes)
        {
            this.numberOfNodes = numberOfNodes;
            augmentedMatrix = new int[numberOfNodes, numberOfNodes];
            SOURCE_NODE = 0;// numberOfNodes;
            potential = new int[numberOfNodes + 1];
            bellmanFord = new BellmanFord(numberOfNodes);
            dijkstra = new Dijkstra(numberOfNodes);
            allShortest = new int[numberOfNodes, numberOfNodes];
        }

        private void computeAugmentedGraph(int[,] adjacencyMatrix)
        {
            for (int source = 0; source < numberOfNodes; source++)
            {
                for (int destination = 0; destination < numberOfNodes; destination++)
                {
                    augmentedMatrix[source, destination] =
                adjacencyMatrix[source, destination];
                }
            }
            for (int destination = 0; destination < numberOfNodes; destination++)
            {
                augmentedMatrix[SOURCE_NODE, destination] = 0;
            }
        }

        private int[,] reweightGraph(int[,] adjacencyMatrix)
        {
            int[,] result = new int[numberOfNodes, numberOfNodes];
            for (int source = 0; source < numberOfNodes; source++)
            {
                for (int destination = 0; destination < numberOfNodes; destination++)
                {
                    result[source, destination] = adjacencyMatrix[source, destination]
                            + potential[source] - potential[destination];
                }
            }
            return result;
        }

        public static int[,] johnsonStart(int[,] adjacencyMatrix)
        {
            Johnson j = new Johnson(adjacencyMatrix.GetLength(0));
            j.computeAugmentedGraph(adjacencyMatrix);

            bool flag = j.bellmanFord.BellmanFordEvaluation(j.SOURCE_NODE,
            j.augmentedMatrix);
            if (!flag)
                return null;
            j.potential = j.bellmanFord.distances;

            int[,] reweightedGraph = j.reweightGraph(adjacencyMatrix);

            for (int source = 0; source < j.numberOfNodes; source++)
            {
                //reweightedGraph in params
                j.dijkstra.dijkstraShortestPath(source, reweightedGraph);
                int[] result = j.dijkstra.Distances;
                for (int destination = 0; destination < j.numberOfNodes; destination++)
                {
                    j.allShortest[source, destination] = result[destination]
                            + j.potential[destination] - j.potential[source];
                }
            }

            return j.allShortest;
        }


        class BellmanFord
        {
            public int[] distances;
            private int numberofverticles;

            public BellmanFord(int numberofvertices)
            {
                this.numberofverticles = numberofvertices;
                distances = new int[numberofvertices + 1];
            }
            public bool BellmanFordEval(int src, int[,] graph)
            {
                // Initialize distance of all vertices as infinite.
                int E=0;
                foreach (var i in graph)
                {
                    if (i != 0) E++;
                }
                int verts = graph.GetLength(0);
                distances = new int[verts];
                for (int i = 0; i < verts; i++)
                    distances[i] = int.MaxValue;

                // initialize distance of source as 0
                distances[src] = 0;

                // Relax all edges |V| - 1 times. A simple
                // shortest path from src to any other
                // vertex can have at-most |V| - 1 edges
                for (int i = 0; i < verts - 1; i++)
                {
                    for (int j = 0; j < verts; j++)
                    {
                        if (distances[graph[j, 0]] != int.MaxValue 
                            && (distances[graph[j, 0]] + graph[j, 2]) < distances[graph[j, 1]])
                            distances[graph[j, 1]] =
                            distances[graph[j, 0]] + graph[j, 2];
                    }
                }

                // check for negative-weight cycles.
                // The above step guarantees shortest
                // distances if graph doesn't contain
                // negative weight cycle. If we get a
                // shorter path, then there is a cycle.
                for (int i = 0; i < verts; i++)
                {
                    int x = graph[i, 0];
                    int y = graph[i, 1];
                    int weight = graph[i, 2];
                    if (distances[x] != int.MaxValue &&
                            distances[x] + weight < distances[y])
                        throw new Exception("Negative weight cycle!");
                }
                return true;
            }
            public bool BellmanFordEvaluation(int source, int[,] adjacencymatrix)
            {
                for (int node = 0; node < numberofverticles; node++)
                {
                    distances[node] = int.MaxValue;
                }

                distances[source] = 0;

                for (int node = 0; node < numberofverticles - 1; node++)
                {
                    for (int sourcenode = 0; sourcenode < numberofverticles;
                sourcenode++)
                    {
                        for (int destinationnode = 1;
                    destinationnode < numberofverticles; destinationnode++)
                        {
                            if (adjacencymatrix[sourcenode,
                    destinationnode] != int.MaxValue)
                            {
                                if (distances[destinationnode] > distances[sourcenode]
                                        + adjacencymatrix[sourcenode, destinationnode])
                                {
                                    distances[destinationnode] = distances[sourcenode]
                                            + adjacencymatrix[sourcenode,
                            destinationnode];
                                }
                            }
                        }
                    }
                }

                for (int sourcenode = 0; sourcenode < numberofverticles; sourcenode++)
                {
                    for (int destinationnode = 0; destinationnode < numberofverticles;
                destinationnode++)
                    {
                        if (adjacencymatrix[sourcenode, destinationnode] != int.MaxValue)
                        {
                            if (distances[destinationnode] > distances[sourcenode]
                                    + adjacencymatrix[sourcenode, destinationnode])
                            {
                                MessageBox.Show("The Graph contains negative egde cycle!", 
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        class Dijkstra
        {
            private bool[] settled;
            private bool[] unsettled;
            public int[] Distances { get; set; }
            private int[,] adjacencymatrix;
            private readonly int numberofvertices;


            public Dijkstra(int numberofvertices)
            {
                this.numberofvertices = numberofvertices;
            }

            public void dijkstraShortestPath(int source, int[,] adjacencymatrix)
            {
                this.settled = new bool[numberofvertices + 1];
                this.unsettled = new bool[numberofvertices + 1];
                this.Distances = new int[numberofvertices + 1];
                this.adjacencymatrix = new int[numberofvertices + 1,
            numberofvertices + 1];

                int evaluationnode;
                for (int vertex = 0; vertex < numberofvertices; vertex++)
                {
                    Distances[vertex] = int.MaxValue;
                }

                for (int sourcevertex = 0; sourcevertex < numberofvertices;
                sourcevertex++)
                {
                    for (int destinationvertex = 0; destinationvertex < numberofvertices;
                    destinationvertex++)
                    {
                        this.adjacencymatrix[sourcevertex, destinationvertex]
                                = adjacencymatrix[sourcevertex, destinationvertex];
                    }
                }

                unsettled[source] = true;
                Distances[source] = 0;
                while (getUnsettledCount(unsettled) != 0)
                {
                    evaluationnode = getNodeWithMinimumDistanceFromUnsettled(unsettled);
                    unsettled[evaluationnode] = false;
                    settled[evaluationnode] = true;
                    evaluateNeighbours(evaluationnode);
                }
            }

            public int getUnsettledCount(bool[] unsettled)
            {
                int count = 0;
                for (int vertex = 0; vertex < numberofvertices; vertex++)
                {
                    if (unsettled[vertex] == true)
                    {
                        count++;
                    }
                }
                return count;
            }

            public int getNodeWithMinimumDistanceFromUnsettled(bool[] unsettled)
            {
                int min = int.MaxValue;
                int node = 0;
                for (int vertex = 0; vertex < numberofvertices; vertex++)
                {
                    if (unsettled[vertex] == true && Distances[vertex] < min)
                    {
                        node = vertex;
                        min = Distances[vertex];
                    }
                }
                return node;
            }

            public void evaluateNeighbours(int evaluationNode)
            {
                int edgeDistance = -1;
                int newDistance = -1;

                for (int destinationNode = 0; destinationNode < numberofvertices;
                destinationNode++)
                {
                    if (settled[destinationNode] == false)
                    {
                        if (adjacencymatrix[evaluationNode,
                    destinationNode] != int.MaxValue)
                        {
                            edgeDistance = adjacencymatrix[evaluationNode,
                    destinationNode];
                            newDistance = Distances[evaluationNode] + edgeDistance;
                            if (newDistance < Distances[destinationNode])
                            {
                                Distances[destinationNode] = newDistance;
                            }
                            unsettled[destinationNode] = true;
                        }
                    }
                }
            }
        }

    }
}
