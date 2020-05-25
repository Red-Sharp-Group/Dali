using System;

namespace RedSharp.Dali.Common.Interfaces
{
    public interface IApplicationBuilder
    {
        IApplicationBuilder Configure (string[] args);

        IApplicationBuilder WithDIContainer<T>(T container);

        int Run(string[] args);
    }
}