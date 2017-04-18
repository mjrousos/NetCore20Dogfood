.NET Core 2.0 and .NET Standard 2.0
===================================
*Updated April 18th, 2017*

Introduction and Disclaimer
---------------------------

.NET Standard 2.0 provides a big jump in the API surface area supported by .NET Standard (and .NET Core). Although .NET Standard 2.0 and .NET Core 2.0 are not yet ready for widespread consumption, nightly builds are available to enable early testing.

Because the 2.0 products are not yet released, these nightly builds should only be used for testing and exploration purposes and *should not* be used in production. They are useful for exploring future porting work, though, or for testing the product and providing early feedback. As detailed in the [.NET Standard Roadmap](https://github.com/dotnet/core/blob/master/roadmap.md#ship-dates), an official .NET Standard 2.0 preview is planned for later in Q2, and a fully-supported release is planned for Q3.

Trying .NET Core / .NET Standard 2.0
------------------------------------

Steps for trying out .NET Core 2.0 are available in the [official CoreFX repository](https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/dogfooding.md). This repository includes a sample .NET Core 2.0 app, .NET Standard 2.0 library, and this readme which summarizes the steps for dogfooding NetCore/NetStandard 2.0.

The sample included in this repository showcases how to setup csproj and NuGet.config files for early .NET Standard 2.0 testing. This solution works with .NET Core SDK 2.0 CLI tools but does not work as is in Visual Studio (due to the bug mentioned at the end of this doc). To use the solution in Visual Studio, either remove the NetCore2-App -> NetStandard2-Library dependency or temporarily make the library target netcoreapp2.0 instead of netstandard2.0.

### With the Command Line

Currently, the .NET Core CLI is the best way to try out new 2.0 features. Visual Studio updates are in development but still have a few more active issues than the command line interface.

Testing .NET Core 2.0 from the CLI is simple:

* Install the [.NET Core SDK 2.0](https://github.com/dotnet/corefx/blob/master/Documentation/project-docs/dogfooding.md#install-prerequisites)
    * It is recommended to install the 2.0 SDK into a new folder to avoid interfering with the released .NET Core SDK. Just make sure when testing 2.0 to use the new `dotnet.exe`. If there is any question of which SDK is being used, `dotnet.exe --info` will print its version to the console.
* Optionally, install [even newer CoreFX builds](https://github.com/dotnet/core-setup#daily-builds) to get the latest changes.
  * If you installed the .NET Core SDK 2.0 into a non-standard location, make sure that the upated CoreFX build is placed in the shared\Microsoft.NETCore.App folder relative to your 2.0 SDK install.
* Create new projects with the usual CLI commands (`dotnet new console` or `dotnet new classlib`, for example). 

The pre-release SDK should automatically add a NuGet.config file to newly-created projects which points package restore at pre-release feeds. Just in case, though, make sure that a NuGet.config similar to this is present in your project:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="dotnet-core" value="https://dotnet.myget.org/F/dotnet-core/api/v3/index.json" />
  </packageSources>
</configuration>
```

Also check the csproj files to make sure that the correct `TargetFramework` attributes are set (`netcoreapp2.0` or `netstandard2.0`).

All usual CLI commands (`dotnet add`, `dotnet restore`, `dotnet run`, etc.) should work.

#### Referencing .NET Framework Libraries
.NET Core and .NET Standard 2.0 projects have the ability to depend on .NET Framework 4.x libraries despite them not targeting .NET Standard. To enable that, currently, for a NetCore/NetStandard project, it's necessary to add an explicit PackageTargetFallback element like this:

```xml
<PackageTargetFallback>$(PackageTargetFallback);net45</PackageTargetFallback>
```

Be aware that while many .NET Framework libraries will work in this way, some which use APIs unavailable to NetCore/NetStandard or which explicitly check the Framework/platform using them will not work. Be sure to thoroughly test any NetCore/NetStandard -> NetFX depenencies!

### With Visual Studio 2017
As mentioned above, Visual Studio 2017 offers some limited options for testing NetCore/NetStandard 2.0.

First, new projects should be created either by using the CLI or by using the .NET Core/Standard new project templates in VS and changing the `TargetFramework` attributes manually (also be sure to add a NuGet.config file, as detailed above).

There are a couple extra steps necessary to get things working in Visual Studio, though:

* .NET Core 2.0 projects in Visual Studio need to explicitly reference the version of Microsoft.NetCore.App they use by including this element in their csproj: `<RuntimeFrameworkVersion>2.0.0-preview1-002028-00</RuntimeFrameworkVersion>`
  * The version, of course, will change as new nightly builds of the Framework are released. Current recent versions can be found [on MyGet](https://dotnet.myget.org/feed/dotnet-core/package/nuget/Microsoft.NetCore.App)
* .NET Standard 2.0 projects, similarly, need to specify which NetStandard.Library version they are using. Update the csproj to include this element: `<NETStandardImplicitPackageVersion>2.0.0-preview1-25218-01</NETStandardImplicitPackageVersion>`
  * As before, up-to-date versions can be found [on MyGet](https://dotnet.myget.org/feed/dotnet-core/package/nuget/NETStandard.Library)

With those changes done, it should be possible to build, run, and debug simple .NET Core or .NET Standard projects in Visual Studio.

#### Known Issue in Visual Studio
Be aware that it is currently not possible to build a .NET Core 2.0 project in Visual Studio with a .NET Standard 2.0 dependency. 

This, of course, should work, but Visual Studio needs an update to its NuGet extension to enable the scenario. This scenario works from the CLI but is not yet enabled in Visual Studio. 

NetCore 2.0 apps in VS can depend on <= NetStandard 1.6 libraries or on NetFX libraries (with the appropriate PackageTargetFallback set). A VS update is in the works to enable NetCore 2.0 -> NetStandard 2.0 dependencies soon.