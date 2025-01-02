using System.Reflection;

namespace TotalOne.Application;

public static class ApplicationAssembly
{
    public static Assembly Get() => typeof(ApplicationAssembly).Assembly;
}
