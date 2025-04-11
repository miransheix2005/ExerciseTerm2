using System;
using System.Collections.Generic;

class Program
{
    public static void Hanoi(int n, char source, char auxiliary, char destination)
    {
        if (n == 1)
        {
            Console.WriteLine($"Move disk 1 from {source} to {destination}");
            return;
        }

        Hanoi(n - 1, source, destination, auxiliary);
        Console.WriteLine($"Move disk {n} from {source} to {destination}");
        Hanoi(n - 1, auxiliary, source, destination);
    }

    static void Exercise1()
    {
        Console.Clear();
        Console.WriteLine("Enter the number of disks: ");
        int numDisks = int.Parse(Console.ReadLine());
        Console.WriteLine("The sequence of moves are:");
        Hanoi(numDisks, 'A', 'B', 'C');
    }
    static void Exercise2()
    {

        int size = GetMagicSquareSize();
        if (size % 2 == 0)
        {
            Console.WriteLine("The size must be an odd number.");
            return;
        }


        int[,] magicSquare = GenerateMagicSquare(size);


        PrintMagicSquare(magicSquare, size);
    }
    static void Exercise3()
    {
        int n = GetMatrixSize();
        int[,] matrix = GetMatrix(n);
        CheckMatrixType(matrix, n);
    }
    static void Exercise4()
    {

        Console.WriteLine("Enter the number of altitudes:");
        int n = int.Parse(Console.ReadLine());

        int[] altitudes = new int[n];

        Console.WriteLine("Enter the altitudes:");
        for (int i = 0; i < n; i++)
        {
            altitudes[i] = int.Parse(Console.ReadLine());
        }

        int trappedWater = CalculateTrappedWater(altitudes, n);

        Console.WriteLine($"The total amount of trapped water is: {trappedWater} units.");

        VisualizeWaterTrapping(altitudes, n);
    }
    static void Exercise5()
    {
        int leader;
        Console.WriteLine("Enter the number of elements in the array:");
        int n = int.Parse(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements of the array:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = int.Parse(Console.ReadLine());
        }

       leader = FindAndPrintLeaders(arr);
       Console.WriteLine($"the leader is {leader}");
    }
    static void Exercise6()
    {
        Console.WriteLine("Enter the number of rows:");
        int m = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the number of columns:");
        int n = int.Parse(Console.ReadLine());

        int[,] matrix = new int[m, n];
        Console.WriteLine("Enter the matrix weights:");
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Enter the start coordinates (row, column):");
        int startX = int.Parse(Console.ReadLine());
        int startY = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the end coordinates (row, column):");
        int endX = int.Parse(Console.ReadLine());
        int endY = int.Parse(Console.ReadLine());

        bool[,] visited = new bool[m, n];

        List<string> paths = new List<string>();
        FindPaths(matrix, startX, startY, endX, endY, visited, "", 0, paths);

        Console.WriteLine("\nPossible valid paths and their weights:");
        foreach (var path in paths)
        {
            Console.WriteLine(path);
        }
    }

    static void FindPaths(int[,] matrix, int x, int y, int endX, int endY, bool[,] visited,
                          string currentPath, int currentWeight, List<string> paths)
    {
        if (x < 0 || y < 0 || x >= matrix.GetLength(0) || y >= matrix.GetLength(1) || visited[x, y])
            return;

        visited[x, y] = true;
        currentPath += $"({x},{y}) -> ";
        currentWeight += matrix[x, y];

        if (x == endX && y == endY)
        {
            paths.Add(currentPath.TrimEnd(" -> ".ToCharArray()) + ", Weight: " + currentWeight);
        }
        else
        {

            FindPaths(matrix, x + 1, y, endX, endY, visited, currentPath, currentWeight, paths);

            FindPaths(matrix, x, y + 1, endX, endY, visited, currentPath, currentWeight, paths);
        }

        visited[x, y] = false;
    }

    static int FindAndPrintLeaders(int[] arr)
    {
        int maxFromRight = arr[arr.Length - 1];


        for (int i = arr.Length - 2; i >= 0; i--)
        {
            if (arr[i] >= maxFromRight)
            {
                maxFromRight = arr[i];
                Console.WriteLine(maxFromRight);
            }
        }
        return maxFromRight;
    }

    static int CalculateTrappedWater(int[] heights, int n)
    {

        if (n < 3)
            return 0;
        int[] left_max = new int[n];
        int[] right_max = new int[n];

        left_max[0] = heights[0];
        for (int i = 1; i < n; i++)
        {
            left_max[i] = Math.Max(left_max[i - 1], heights[i]);
        }

        right_max[n - 1] = heights[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            right_max[i] = Math.Max(right_max[i + 1], heights[i]);
        }

        int totalWater = 0;
        for (int i = 0; i < n; i++)
        {

            totalWater += Math.Max(0, Math.Min(left_max[i], right_max[i]) - heights[i]);
        }

        return totalWater;
    }
    static int GetMagicSquareSize()
    {
        Console.Write("Enter the size of the magic square (odd number): ");
        return int.Parse(Console.ReadLine());
    }

    static int[,] GenerateMagicSquare(int n)
    {
        int[,] magicSquare = new int[n, n];

        int row = 0;
        int col = n / 2;

        for (int num = 1; num <= n * n; num++)
        {
            magicSquare[row, col] = num;

            int newRow = (row - 1 + n) % n;
            int newCol = (col + 1) % n;

            if (magicSquare[newRow, newCol] != 0)
            {
                row = (row + 1) % n;
            }
            else
            {
                row = newRow;
                col = newCol;
            }
        }

        return magicSquare;
    }

    static void PrintMagicSquare(int[,] magicSquare, int n)
    {
        Console.WriteLine("\nMagic Square:");

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(magicSquare[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static int GetMatrixSize()
    {
        Console.Write("Enter the size of the matrix (n x n): ");
        return int.Parse(Console.ReadLine());
    }

    static int[,] GetMatrix(int n)
    {
        int[,] matrix = new int[n, n];

        Console.WriteLine("Enter the elements of the matrix:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"Enter element at position ({i},{j}): ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }
        return matrix;
    }

    static void CheckMatrixType(int[,] matrix, int n)
    {
        bool isSymmetrical = true;
        bool isTopTriangle = true;
        bool isBottomTriangle = true;
        bool isDiagonal = true;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] != matrix[j, i])
                    isSymmetrical = false;

                if (i > j && matrix[i, j] != 0)
                    isTopTriangle = false;

                if (i < j && matrix[i, j] != 0)
                    isBottomTriangle = false;

                if (i != j && matrix[i, j] != 0)
                    isDiagonal = false;
            }
        }

        Console.WriteLine("\nMatrix Type Check:");
        if (isSymmetrical)
            Console.WriteLine("The matrix is Symmetrical.");
        else
            Console.WriteLine("The matrix is not Symmetrical.");

        if (isTopTriangle)
            Console.WriteLine("The matrix is a Top Triangular Matrix.");
        else
            Console.WriteLine("The matrix is not a Top Triangular Matrix.");

        if (isBottomTriangle)
            Console.WriteLine("The matrix is a Bottom Triangular Matrix.");
        else
            Console.WriteLine("The matrix is not a Bottom Triangular Matrix.");

        if (isDiagonal)
            Console.WriteLine("The matrix is a Diagonal Matrix.");
        else
            Console.WriteLine("The matrix is not a Diagonal Matrix.");
    }

    static void VisualizeWaterTrapping(int[] heights, int n)
    {

        int maxHeight = 0;
        foreach (var height in heights)
        {
            maxHeight = Math.Max(maxHeight, height);
        }

        Console.WriteLine("\nVisual Representation of the Water Trapping:");

        for (int level = maxHeight; level >= 0; level--)
        {
            for (int i = 0; i < n; i++)
            {

                if (heights[i] >= level)
                {
                    Console.Write(" |");
                }
                else
                {

                    Console.Write("  ");
                }
            }
            Console.WriteLine();
        }

        for (int i = 0; i < n; i++)
        {
            Console.Write($"-{heights[i]}-");
        }
        Console.WriteLine();
    }

    public static void Main()
    {
        Console.WriteLine("Enter the target exercise(1-7): ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Exercise1();
                break;
            case 2:
                Exercise2();
                break;
            case 3:
                Exercise3();
                break;
            case 4:
                Exercise4();
                break;
            case 5:
                Exercise5();
                break;
            case 6:
                Exercise6();
                break;

        }


    }


}