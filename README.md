# SharpBar

Simple progress bar for your C# console application.

This is companion repository for nuget package publish guide available [here](https://blog.kbegiedza.eu/how-to-publish-your-project-as-nuget-package).

- [SharpBar](#sharpbar)
  - [Usage](#usage)

## Usage

```c#
// include namespace
using SharpBar;

// use WithProgress() extension
foreach (var i in collection.WithProgress())
{
    // do stuff ...
}

```

for more detailed examples please visit: https://sharpbar.kbegiedza.eu / [docs](./docs/index.md)
