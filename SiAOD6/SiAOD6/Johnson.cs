using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiAOD6
{
    class Johnson
    {
        public const int MAX_VALUE = 999;

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
            augmentedMatrix = new int[numberOfNodes + 1,numberOfNodes + 1];
            SOURCE_NODE = numberOfNodes;
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
                    augmentedMatrix[source, destination] = adjacencyMatrix[source, destination];
                }
            }
            for (int destination = 0; destination < numberOfNodes; destination++)
            {
                augmentedMatrix[SOURCE_NODE, destination] = 0;
            }
        }

        private int[,] reweightGraph(int[,] adjacencyMatrix)
        {
            int[,] result = new int[numberOfNodes + 1, numberOfNodes + 1];
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
            Johnson j = new Johnson((int)Math.Sqrt(adjacencyMatrix.Length));
            j.computeAugmentedGraph(adjacencyMatrix);

            bool flag = j.bellmanFord.BellmanFordEvaluation(j.SOURCE_NODE, j.augmentedMatrix);
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
                    j.allShortest[source,destination] = result[destination]
                            + j.potential[destination] - j.potential[source];
                }
            }

            return j.allShortest;
        }


        class BellmanFord
        {
            public int[] distances { get; }
            private int numberofverticles;

            public BellmanFord(int numberofvertices)
            {
                this.numberofverticles = numberofvertices;
                distances = new int[numberofvertices + 1];
            }

            public bool BellmanFordEvaluation(int source, int[,] adjacencymatrix)
            {
                for (int node = 0; node < numberofverticles; node++)
                {
                    distances[node] = MAX_VALUE;
                }

                distances[source] = 0;

                for (int node = 0; node < numberofverticles-1; node++)
                {
                    for (int sourcenode = 0; sourcenode < numberofverticles; sourcenode++)
                    {
                        for (int destinationnode = 1; destinationnode < numberofverticles; destinationnode++)
                        {
                            if (adjacencymatrix[sourcenode, destinationnode] != MAX_VALUE)
                            {
                                if (distances[destinationnode] > distances[sourcenode]
                                        + adjacencymatrix[sourcenode, destinationnode])
                                {
                                    distances[destinationnode] = distances[sourcenode]
                                            + adjacencymatrix[sourcenode, destinationnode];
                                }
                            }
                        }
                    }
                }

                for (int sourcenode = 0; sourcenode < numberofverticles; sourcenode++)
                {
                    for (int destinationnode = 0; destinationnode < numberofverticles; destinationnode++)
                    {
                        if (adjacencymatrix[sourcenode, destinationnode] != MAX_VALUE)
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
                this.adjacencymatrix = new int[numberofvertices + 1, numberofvertices + 1];

                int evaluationnode;
                for (int vertex = 0; vertex < numberofvertices; vertex++)
                {
                    Distances[vertex] = MAX_VALUE;
                }

                for (int sourcevertex = 0; sourcevertex < numberofvertices; sourcevertex++)
                {
                    for (int destinationvertex = 0; destinationvertex < numberofvertices; destinationvertex++)
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
                int min = MAX_VALUE;
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

                for (int destinationNode = 0; destinationNode < numberofvertices; destinationNode++)
                {
                    if (settled[destinationNode] == false)
                    {
                        if (adjacencymatrix[evaluationNode, destinationNode] != MAX_VALUE)
                        {
                            edgeDistance = adjacencymatrix[evaluationNode, destinationNode];
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
