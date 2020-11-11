using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConsoleCoreApp
{
    class MyPrograms
    {
        public static string SloveMath(string line)
        {

            var answer = "";
            var equation = line.Split(' ');
            for (var i = 0; i < equation.Length; i++)
            {
                int index;
                if (equation[i].IndexOf('%') != -1)
                {
                    var badEquation = equation[i].ToCharArray();
                    index = equation[i].IndexOf('%');
                    var a = "";
                    var b = "";
                    var j = 1;
                    var k = 1;
                    try
                    {
                        while (badEquation[index - k] != '*'
                        && badEquation[index - k] != '/'
                        && badEquation[index - k] != '+'
                        && badEquation[index - k] != '-'
                        && badEquation[index - k] != ')'
                        && badEquation[index - k] != ' ')
                        {
                            a += badEquation[index - k];
                            k++;
                        }
                    }
                    catch
                    {
                        return "KEKS";
                    }
                    var newA = ReverseString(a);
                    try
                    {
                        while (badEquation[index + j] != '*'
                    && badEquation[index + j] != '/'
                    && badEquation[index + j] != '+'
                    && badEquation[index + j] != '-'
                    && badEquation[index + j] != '%'
                    && badEquation[index + j] != ')'
                    && badEquation[index + j] != '('
                    && badEquation[index + j] != ' ')
                        {
                            b += badEquation[index + j];
                            j++;
                        }
                    }
                    catch
                    { return "KEKS"; }

                    int x;
                    try
                    {
                        x = int.Parse(newA) % int.Parse(b);
                    }
                    catch
                    {
                        return "KEKS";
                    }

                    k--;
                    j -= 2;
                    var lastBadIndex = index + j;
                    var firstBadIndex = index - k;

                    string badPart;
                    try
                    { badPart = equation[i].Substring(firstBadIndex, lastBadIndex); }
                    catch
                    {
                        return "KEKS";
                    }
                    equation[i] = equation[i].Replace(badPart, x.ToString());
                    i--;
                }
                else
                    try { answer += " " + Calculate(equation[i]); }
                    catch
                    { return "KEKS"; }
            }
            return (answer.Remove(0, 1));
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static int Calculate(string expression)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(int), expression);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return (int)(loDataTable.Rows[0]["Eval"]);
        }

        public static string Determinant(string question)
        {
            double det = 1;
            const double EPS = 1E-9;
            var allStr = question.Split(new string[] { " ", "&", "\\\\" }, StringSplitOptions.RemoveEmptyEntries);

            int dimension = (int)System.Math.Sqrt(allStr.Length);

            double[][] b = new double[1][];
            b[0] = new double[dimension];

            var matrix = new double[dimension][];

            for (int i = 0; i < dimension; ++i)
            {
                matrix[i] = new double[dimension];
                for (int j = 0; j < dimension; ++j)
                {
                    matrix[i][j] = int.Parse(allStr[i * dimension + j]);
                }
            }

            for (int i = 0; i < dimension; ++i)
            {
                int k = i;
                for (int j = i + 1; j < dimension; ++j)
                    if (System.Math.Abs(matrix[j][i]) > System.Math.Abs(matrix[k][i]))
                        k = j;
                if (System.Math.Abs(matrix[k][i]) < EPS)
                {
                    det = 0;
                    break;
                }
                b[0] = matrix[i];
                matrix[i] = matrix[k];
                matrix[k] = b[0];
                if (i != k)
                    det = -det;
                det *= matrix[i][i];
                for (int j = i + 1; j < dimension; ++j)
                    matrix[i][j] /= matrix[i][i];
                for (int j = 0; j < dimension; ++j)
                    if ((j != i) && (System.Math.Abs(matrix[j][i]) > 0))
                        for (k = i + 1; k < dimension; ++k)
                            matrix[j][k] -= matrix[i][k] * matrix[j][i];

            }
            return Math.Round(det).ToString();
        }

        public static string Polynomial_root(string question)
        {

            return "";
        }

        public static string Moment(string question)
        {
            var time = question.Split()[0];
            time = time.Substring(0, 5);
            var date = question.Split()[1];
            date = date.Substring(1, 1) + " ноября ";
            return date + time;
        }

        public static string Shape(string question)
        {
            var coords = question.Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var dotAmount = coords.Length;
            var xCoords = new int[dotAmount];
            var yCoords = new int[dotAmount];

            for (var i = 0; i < dotAmount; ++i)
            {
                var tmpBuilderCoord = new StringBuilder(coords[i]);
                tmpBuilderCoord.Remove(0, 1);
                tmpBuilderCoord.Remove(tmpBuilderCoord.Length - 1, 1);

                var twoCoords = tmpBuilderCoord.ToString().Split(',');

                xCoords[i] = int.Parse(twoCoords[0]);
                yCoords[i] = int.Parse(twoCoords[1]);
            }

            var maxX = xCoords.Max();
            var minX = xCoords.Min();
            var maxY = yCoords.Max();
            var minY = yCoords.Min();

            if ((minY == maxY && maxX != minX) || (maxY != minY && minX == maxX))
                return "equilateraltriangle";

            if (maxX == minX || maxY == minY)
                return "circle";

            return "square";
        }

        public static string Statistics(string question)
        {
            var parts = question.Split('|');
            var task = parts[0];
            var numbers = parts[1].Split(' ');

            Array.Sort(numbers);

            if (task == "min")
            {
                return numbers[0].ToString();
            }
            else
            if (task == "max")
            {
                return numbers[numbers.Length - 1].ToString();
            }
            else
            //if(task=="sum")
            {
                return "0";
            }
            /*else
            if(task=="median")
            {

            }*/
        }
    }
}
