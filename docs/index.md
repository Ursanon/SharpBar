# Welcome to SharpBar

Simple progress bar for your C# console application.

This is companion repository for nuget package publish guide available [here](https://blog.kbegiedza.eu/how-to-publish-your-project-as-nuget-package).

- [Welcome to SharpBar](#welcome-to-sharpbar)
  - [Usage](#usage)

## Usage

```c#
// include namespace
using SharBar;

// use WithProgress() extension
foreach (var i in collection.WithProgress())
{
    // do stuff ...
}

```