using System;
using System.Reflection;


public class Triangle
{
    public double side1;
    public double side2; 
    public double side3;
    private string color;
    internal bool isEquilateral;
    internal bool isIsosceles;
    internal bool isScalene;
    protected double area; 
    public Triangle(double side1, double side2, double side3, string clr)
    {
        this.side1 = side1;
        this.side2 = side2;
        this.side3 = side3;
        color = clr;
      
            CalculateArea();
            CheckType();
       
    }

    private void CalculateArea()
    {
        double s = (side1 + side2 + side3) / 2;
        area = Math.Sqrt(s * (s - side1) * (s - side2) * (s - side3));
    }
    public double CalculatePerimeter()
    {
        return side1 + side2 + side3;
    }
    private void CheckType()
    {
        if (side1 == side2 && side2 == side3)
            isEquilateral = true;
        else if (side1 == side2 || side1 == side3 || side2 == side3)
            isIsosceles = true; 
        else
            isScalene = true;
    }
    public void DisplayInfo()
    {
        Console.WriteLine($"Triangle with sides {side1}, {side2}, {side3}, color {color}, area: {area}, perimeter {CalculatePerimeter()}");
        Console.WriteLine($"Type of triangle: {(isEquilateral ? "Equilateral" : (isIsosceles ? "Isosceles" : "Polylateral"))}");
    }
    public void SetField(string fieldName, object value)
    {
        //task 4
        Type type = typeof(Triangle);
        FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);//рефлексія

        if (field != null)
        {
            field.SetValue(this, value);//рефлесія
            Console.WriteLine($"Set {fieldName} to {value}");
            CalculateArea();
        }
        else
        {
            Console.WriteLine($"Field {fieldName} not found.");
        }
    }
}

class Program
{
    static void Main()
    {
        // task1
        Triangle triangle = new Triangle(3.0, 4.0, 5.0, "black");
        triangle.DisplayInfo();
        //task2
        Type triangleType = typeof(Triangle);
        Console.WriteLine("............ Type.............");
        Console.WriteLine($"Full class name: {triangleType.FullName}");
        Console.WriteLine($"Number of properties: {triangleType.GetFields().Length}");
        Console.WriteLine($"Number of methods: {triangleType.GetMethods()}");
        TypeInfo triangleTypeInfo = triangleType.GetTypeInfo();
        Console.WriteLine("............ TypeInfo.............");
        Console.WriteLine($"The class is abstract: {triangleTypeInfo.IsAbstract}");
        Console.WriteLine($"The class is sealed: {triangleTypeInfo.IsSealed}");
        Console.WriteLine($"The class is public: {triangleTypeInfo.IsPublic}");
        //task3
        Console.WriteLine("............ MemberInfo.............");
        MemberInfo[] members = triangleType.GetMembers();
        Console.WriteLine("Class members Triangle:");
        foreach (MemberInfo member in members)
        {
            Console.WriteLine($"Name: {member.Name}, Type: {member.MemberType}");
        }
        //task4 
        Console.WriteLine("............ FieldInfo.............");
        triangle.SetField("side1", 30);
        triangle.SetField("side2", 30);
        triangle.SetField("side3", 10);
        triangle.DisplayInfo();
        //task5
        MethodInfo methodInfo = triangleType.GetMethod("DisplayInfo");
        MethodInfo[] privateMethods = triangleType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (MethodInfo method in privateMethods)
        {
            Console.WriteLine($"Private method name: {method.Name}");
        }
        methodInfo.Invoke(triangle, null);
        Console.ReadLine();

    }


}
