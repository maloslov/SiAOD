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

        private int SOURCE_NODE;
        private int numberOfNodes;
        private int[,] augmentedMatrix;
        private int[] potential;
        private BellmanFord bellmanFord;
        private Dijkstra dijkstra;
        private int[,] allPairShortestPath;


        private Johnson(int numberOfNodes)
        {
            this.numberOfNodes = numberOfNodes;
            augmentedMatrix = new int[numberOfNodes + 2,numberOfNodes + 2];
            SOURCE_NODE = numberOfNodes + 1;
            potential = new int[numberOfNodes + 2];
            bellmanFord = new BellmanFord(numberOfNodes + 1);
            dijkstra = new Dijkstra(numberOfNodes);
            allPairShortestPath = new int[numberOfNodes + 1, numberOfNodes + 1];
        }

        private void computeAugmentedGraph(int[,] adjacencyMatrix)
        {
            for (int source = 1; source <= numberOfNodes; source++)
            {
                for (int destination = 1; destination <= numberOfNodes; destination++)
                {
                    augmentedMatrix[source, destination] = adjacencyMatrix[source, destination];
                }
            }
            for (int destination = 1; destination <= numberOfNodes; destination++)
            {
                augmentedMatrix[SOURCE_NODE, destination] = 0;
            }
        }

        private int[,] reweightGraph(int[,] adjacencyMatrix)
        {
            int[,] result = new int[numberOfNodes + 1, numberOfNodes + 1];
            for (int source = 1; source <= numberOfNodes; source++)
            {
                for (int destination = 1; destination <= numberOfNodes; destination++)
                {
                    result[source, destination] = adjacencyMatrix[source, destination]
                            + potential[source] - potential[destination];
                }
            }
            return result;
        }

        public static int[,] johnsonsAlgorithms(int[,] adjacencyMatrix)
        {
            Johnson j = new Johnson((int)Math.Sqrt(adjacencyMatrix.Length)-1);
            j.computeAugmentedGraph(adjacencyMatrix);

            j.bellmanFord.BellmanFordEvaluation(j.SOURCE_NODE, j.augmentedMatrix);
            j.potential = j.bellmanFord.distances;

            int[,] reweightedGraph = j.reweightGraph(adjacencyMatrix);

            for (int source = 1; source <= j.numberOfNodes; source++)
            {
                //reweightedGraph in params
                j.dijkstra.dijkstraShortestPath(source, reweightedGraph);
                int[] result = j.dijkstra.distances;
                for (int destination = 1; destination <= j.numberOfNodes; destination++)
                {
                    j.allPairShortestPath[source,destination] = result[destination]
                            + j.potential[destination] - j.potential[source];
                }
            }

            return j.allPairShortestPath;
        }






        class BellmanFord
        {
            public int[] distances { get; }
            private int numberofvertices;

            public BellmanFord(int numberofvertices)
            {
                this.numberofvertices = numberofvertices;
                distances = new int[numberofvertices + 1];
            }

            public void BellmanFordEvaluation(int source, int[,] adjacencymatrix)
            {
                for (int node = 1; node <= numberofvertices; node++)
                {
                    distances[node] = MAX_VALUE;
                }

                distances[source] = 0;

                for (int node = 1; node <= numberofvertices - 1; node++)
                {
                    for (int sourcenode = 1; sourcenode <= numberofvertices; sourcenode++)
                    {
                        for (int destinationnode = 1; destinationnode <= numberofvertices; destinationnode++)
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

                for (int sourcenode = 1; sourcenode <= numberofvertices; sourcenode++)
                {
                    for (int destinationnode = 1; destinationnode <= numberofvertices; destinationnode++)
                    {
                        if (adjacencymatrix[sourcenode, destinationnode] != MAX_VALUE)
                        {
                            if (distances[destinationnode] > distances[sourcenode]
                                    + adjacencymatrix[sourcenode, destinationnode])
                                MessageBox.Show("The Graph contains negative egde cycle!");
                        }
                    }
                }
            }
        }

        class Dijkstra
        {
            private bool[] settled;
            private bool[] unsettled;
            public int[] distances { get; set; }
            private int[,] adjacencymatrix;
            private int numberofvertices;


            public Dijkstra(int numberofvertices)
            {
                this.numberofvertices = numberofvertices;
            }

            public void dijkstraShortestPath(int source, int[,] adjacencymatrix)
            {
                this.settled = new bool[numberofvertices + 1];
                this.unsettled = new bool[numberofvertices + 1];
                this.distances = new int[numberofvertices + 1];
                this.adjacencymatrix = new int[numberofvertices + 1, numberofvertices + 1];

                int evaluationnode;
                for (int vertex = 1; vertex <= numberofvertices; vertex++)
                {
                    distances[vertex] = MAX_VALUE;
                }

                for (int sourcevertex = 1; sourcevertex <= numberofvertices; sourcevertex++)
                {
                    for (int destinationvertex = 1; destinationvertex <= numberofvertices; destinationvertex++)
                    {
                        this.adjacencymatrix[sourcevertex, destinationvertex]
                                = adjacencymatrix[sourcevertex, destinationvertex];
                    }
                }

                unsettled[source] = true;
                distances[source] = 0;
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
                for (int vertex = 1; vertex <= numberofvertices; vertex++)
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
                for (int vertex = 1; vertex <= numberofvertices; vertex++)
                {
                    if (unsettled[vertex] == true && distances[vertex] < min)
                    {
                        node = vertex;
                        min = distances[vertex];
                    }
                }
                return node;
            }

            public void evaluateNeighbours(int evaluationNode)
            {
                int edgeDistance = -1;
                int newDistance = -1;

                for (int destinationNode = 1; destinationNode <= numberofvertices; destinationNode++)
                {
                    if (settled[destinationNode] == false)
                    {
                        if (adjacencymatrix[evaluationNode, destinationNode] != MAX_VALUE)
                        {
                            edgeDistance = adjacencymatrix[evaluationNode, destinationNode];
                            newDistance = distances[evaluationNode] + edgeDistance;
                            if (newDistance < distances[destinationNode])
                            {
                                distances[destinationNode] = newDistance;
                            }
                            unsettled[destinationNode] = true;
                        }
                    }
                }
            }
        }

    }


}
