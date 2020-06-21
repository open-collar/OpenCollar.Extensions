# OpenCollar.Extensions

This library provides extensions and base classes for low-level .NET framework
types.  In particular this includes methods extendending the Delegate and Type
classes.  These include:

 * [BadImplementationException](/api/OpenCollar.Extensions.BadImplementationException.html) - An
    exception thrown when an implementation of an interface or delegate (for example an overridden abstract method or a
    factory class) breaks the contract either explicitly or implicitly expected.
 * [Compare](/api/OpenCollar.Extensions.Compare.html) - Utility methods supporting comparisons
    between objects and values.
 * [DelegateExtensions](/api/OpenCollar.Extensions.DelegateExtensions.html) - Extensions to the
    [Delegate](https://docs.microsoft.com/en-us/dotnet/api/system.delegate) type.  In particular a "safe" method for 
    raising events that may have no subscribers or for which the subscribers might throw exceptions.
 * [Disposable](/api/OpenCollar.Extensions.Disposable.html) - A base class implementing the 
    [Dispose Pattern](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose#implement-the-dispose-pattern)
    in a thread-safe way.
 * [ExceptionManager](/api/OpenCollar.Extensions.ExceptionManager.html) - Provides a very simple
    way to route all unhandled exceptions through a single point. 
 * [TypeExtensions](/api/OpenCollar.Extensions.TypeExtensions.html) - Methods extending the
    [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type) type.

## NuGet Package

Package and installation instructions at: https://www.nuget.org/packages/OpenCollar.Extensions/

## Project
<table style="border-style: none; width: 100%;">
    <tr style="border-style: none;">
        <td style="width: 20%; border-style: none;">Latest Build:</td>
        <td style="width: 20%; border-style: none;"><a href="https://github.com/open-collar/OpenCollar.Extensions/actions"><img src="https://img.shields.io/github/workflow/status/open-collar/OpenCollar.Extensions/Build and Deploy"/></a></td>
        <td style="width: 20%; border-style: none;"><a href="https://coveralls.io/github/open-collar/OpenCollar.Extensions?branch=master"><img src="https://coveralls.io/repos/github/open-collar/OpenCollar.Extensions/badge.svg?branch=master"/></a></td>
        <td style="width: 20%; border-style: none;"><a href="https://www.nuget.org/packages/OpenCollar.Extensions/"><img src="https://img.shields.io/nuget/vpre/OpenCollar.Extensions?color=green"/></a></td>
        <td style="width: 20%; border-style: none;"><a href="https://www.nuget.org/packages/OpenCollar.Extensions/"><img src="https://img.shields.io/nuget/dt/OpenCollar.Extensions?color=green"/></a></td>
    </tr>
</table>

 * [Source Code on GitHub](https://github.com/open-collar/OpenCollar.Extensions)
 * [Issue Tracking on GitHub](https://github.com/open-collar/OpenCollar.Extensions/issues)
 * [Documentation on GitHub Pages](https://open-collar.github.io/OpenCollar.Extensions/)

# Usage


This library provides extensions and base classes for low-level .NET framework
types.  In particular this includes methods extendending the Delegate and Type
classes.


# Related Projects

* [OpenCollar.Extensions](https://github.com/open-collar/OpenCollar.Extensions)
* [OpenCollar.Extensions.Collections](https://github.com/open-collar/OpenCollar.Extensions.Collections)
* [OpenCollar.Extensions.Configuraton](https://github.com/open-collar/OpenCollar.Extensions.Configuraton)
* [OpenCollar.Extensions.IO](https://github.com/open-collar/OpenCollar.Extensions.IO)
* [OpenCollar.Extensions.Security](https://github.com/open-collar/OpenCollar.Extensions.Security)
* [OpenCollar.Extensions.Threading](https://github.com/open-collar/OpenCollar.Extensions.Threading)
* [OpenCollar.Extensions.Validation](https://github.com/open-collar/OpenCollar.Extensions.Validation)