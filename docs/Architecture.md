# Dali application architecture

This application is built with MVVM architecture with cross platform in mind. Here is short description for each module:
* RedSharp.Dali - entry point of application. Has reference to RedSharp.Dali.View and RedSharp.Dali.ViewModel reference. This a single point where this two library are connected. It creates DI container and pass it to View and ViewModel module. They should register all required services. It's interfaces defined in RedSharp.Dali.Common. 
* RedSharp.Dali.View - contains WPF application, all UI markup and classes required to transform data from generic to WPF specific and vise versa. Knows nothing about ViewModel types and works with it only through bindings and interface types.
* RedSharp.Dali.Controls - contains WPF reusable UI code (controls, themes and so on).
* RedSharp.Dali.Common - defines interfaces and enumeration that will be used all across the application. Should define interfaces only with plain dotnet types without direct references to any 3rd party library.
* RedSharp.Dali.Common.Interop - defines platform specific code, currently WinAPI calls, to support windows transparency.
* RedSharp.Dali.ViewModels - contains platform independent ViewModels. Hides every concrete type and 3rd party types with explicit interface implementation. That means MainWindowViewModels, for example might have public member of ConcreteCommand type, but it is hidden in IMainViewModel.ICommand member. So you might think about that ones like internal. Should not depend on any platform specific library. 