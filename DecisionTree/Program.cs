using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DecisionTree
{
    enum Value
    {
        c, d
    }

    enum Class
    {
        Y, G
    }

    struct Parameter
    {
        public Value Value;
        public string Name;

        public Parameter(Value value, string name)
        {
            Value = value;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name}={Value}";
        }
    }

    struct Instance
    {
        public string Name;
        public Dictionary<string, Parameter> Parameters;
        public Class Class;

        public Instance(string name, IEnumerable<Parameter> parameters, Class cls)
        {
            Name = name;
            Parameters = parameters.ToDictionary(p => p.Name);
            Class = cls;
        }

        public Instance Without(string parameterName)
        {
            return new Instance(Name, Parameters.Values.Where(p => p.Name != parameterName), Class);
        }

        public override string ToString()
        {
            return Name + ": " + String.Join(" ", Parameters.Values.OrderBy(p => p.Name)) + " - " + Class;
        }
    }

    class InstanceSet : List<Instance>
    {
        public InstanceSet(IEnumerable<Instance> collection) : base(collection) { }

        public override string ToString()
        {
            return String.Join(Environment.NewLine, this);
        }
    }

    static class Extensions
    {
        public static InstanceSet ToInstanceSet(this IEnumerable<Instance> collection)
        {
            return new InstanceSet(collection);
        }
    }

    static class Enums
    {
        public static T[] Items<T>()
        {
            return (T[]) Enum.GetValues(typeof(T));
        }
    }

    class Node
    {
        public InstanceSet TrainingInstances;
        public Parameter? Parameter;
        public List<Node> ChildNodes { get; } = new List<Node>();

        public Node(InstanceSet trainingInstances, Parameter? parameter = null)
        {
            TrainingInstances = trainingInstances;
            Parameter = parameter;
        }

        public Class? Class()
        {
            return Enums.Items<Class>().Cast<Class?>().FirstOrDefault(cls => !ChildNodes.Any() && TrainingInstances.All(inst => inst.Class == cls));
        }

        public override string ToString()
        {
            return $"Parameter: {Parameter}, Class: {Class()}";
        }
    }

    public class Program
    {
        static InstanceSet Parse(string data, int startInd = 1)
        {
            var lines = Regex.Replace(data.Trim(), @" +", "\t").Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            return lines.Select((line, ind) => new Instance($"e{ind+startInd}",
                    line.Split('\t').Take(line.Count(c => c == '\t')).Select((v, i) => new Parameter((Value)Enum.Parse(typeof(Value), v), $"x{i+1}")),
                    (Class) Enum.Parse(typeof(Class), line.Split('\t').Last())))
                .ToInstanceSet();
        }

        static double P(Class cls, InstanceSet instances)
        {
            return !instances.Any() ? 0 : (double) instances.Count(inst => inst.Class == cls) / instances.Count;
        }

        static double P(string parameterName, Value value, InstanceSet instances)
        {
            return (double) instances.Count(inst => inst.Parameters[parameterName].Value == value) / instances.Count;
        }

        static double Gini(InstanceSet instances)
        {
            return 1 - Enums.Items<Class>().Select(cls => Math.Pow(P(cls, instances), 2)).Sum();
        }

        static double Gini(InstanceSet instances, string parameterName)
        {
            return Enums.Items<Value>().Select(v => P(parameterName, v, instances) * Gini(instances.Where(inst => inst.Parameters[parameterName].Value == v).ToInstanceSet())).Sum();
        }

        static double GiniGain(InstanceSet instances, string parameterName)
        {
            Console.WriteLine($"    Gini(E, {parameterName}) = {Math.Round(Gini(instances, parameterName), 2)}");
            Console.WriteLine("    " + String.Join("; ", Enums.Items<Value>().Select(v => $"P({parameterName}={v}) = " + P(parameterName, v, instances) + $" Gini(E{parameterName}={v}) = " +
                                                                        Math.Round(Gini(instances.Where(inst => inst.Parameters[parameterName].Value == v).ToInstanceSet()), 2))));

            return Gini(instances) - Gini(instances, parameterName);
        }

        static string SelectParameter(InstanceSet instances)
        {
            Console.WriteLine();
            Console.WriteLine($"Selecting parameter for\r\n{instances}");

            Console.WriteLine($"Gini(E) = {Math.Round(Gini(instances), 2)}");

            var parameters = instances.First().Parameters.Values.OrderBy(p => p.Name);
            string result = null;
            double max = Double.MinValue;
            foreach (var parameter in parameters)
            {
                var gain = GiniGain(instances, parameter.Name);
                Console.WriteLine($"GiniGain({parameter.Name}) = {Math.Round(gain, 2)} ");
                if (gain > max)
                {
                    max = gain;
                    result = parameter.Name;
                }
            }
            Console.WriteLine($"Selected {result}");

            return result;
        }

        static Node BuildTree(Node root)
        {
            if (root.Class().HasValue)
            {
                Console.WriteLine();
                Console.WriteLine($"All have class {root.Class()}");
                Console.WriteLine(root.TrainingInstances);
                return root;
            }

            var paramName = SelectParameter(root.TrainingInstances);

            foreach (var val in Enums.Items<Value>())
            {
                Console.WriteLine();
                Console.WriteLine($"{paramName}={val}");

                root.ChildNodes.Add(BuildTree(new Node(root.TrainingInstances
                    .Where(inst => inst.Parameters[paramName].Value == val)
                    .Select(inst => inst.Without(paramName))
                    .ToInstanceSet(), new Parameter(val, paramName))));
            }

            return root;
        }

        static Class Classify(Node root, Instance instance)
        {
            if (root.Class().HasValue)
            {
                return root.Class().Value;
            }

            return Classify(root.ChildNodes.First(n => instance.Parameters[n.Parameter?.Name].Value == n.Parameter?.Value), instance);
        }

        public static void Main(string[] args)
        {
            var trainingSet = Parse(@"
c	c	d	d	d	Y
d	c	d	d	d	G
c	d	d	c	c	Y
c	c	d	c	d	Y
d	d	c	c	d	G
c	d	d	d	c	Y
d	d	c	d	d	Y
c	d	d	c	d	G
d	c	d	c	d	G
d	c	d	c	c	G");

            Console.WriteLine(trainingSet);

            var tree = BuildTree(new Node(trainingSet));

            var testSet = Parse(@"
c	d	c	c	c	Y
d	c	c	d	c	G", 19);
            Console.WriteLine();
            foreach (var instance in testSet)
            {
                Console.WriteLine($"{instance} == {Classify(tree, instance)}");
            }
        }
    }
}
