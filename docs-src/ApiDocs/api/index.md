# Basics

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

## NuGet Package Installation

Package and installation instructions at: https://www.nuget.org/packages/OpenCollar.Extensions/